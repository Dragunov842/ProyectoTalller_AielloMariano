using Microsoft.Data.SqlClient;
using System;
using System.IO;

namespace CapaDatos
{
    public class CD_Backup
    {
        // Genera una copia de seguridad completa de la base de datos.
        // Recibe como parámetro la ruta donde se almacenará el archivo .bak.
        public void CrearBackup(string rutaDestino)
        {
            // Se comprueba que la ruta recibida no sea nula,
            // vacía o contenga únicamente espacios en blanco.
            if (string.IsNullOrWhiteSpace(rutaDestino))
                throw new ArgumentException(
                    "La ruta de destino no es válida.");

            // Se obtiene la ruta absoluta del archivo y se duplican
            // las comillas simples para evitar que interfieran
            // con la construcción de la consulta SQL.
            string rutaSegura = Path.GetFullPath(rutaDestino)
                .Replace("'", "''");

            // Se establece la conexión con SQL Server.
            using (SqlConnection conexion =
                   new SqlConnection(Conexion.cadena))
            {
                conexion.Open();

                // BACKUP DATABASE genera una copia de seguridad
                // de la base de datos DBSISTEMA_GIMNASIO.
                // TO DISK indica dónde se guardará el archivo.
                // WITH INIT sobrescribe el contenido existente en el destino.
                // STATS = 10 muestra el progreso de la operación
                // en intervalos del 10%.
                string consulta = $@"
                    BACKUP DATABASE [DBSISTEMA_GIMNASIO]
                    TO DISK = N'{rutaSegura}'
                    WITH INIT, STATS = 10;";

                using (SqlCommand comando =
                       new SqlCommand(consulta, conexion))
                {
                    // Se establece un tiempo de espera ilimitado,
                    // evitando que la operación falle por superar
                    // el tiempo de espera predeterminado.
                    comando.CommandTimeout = 0;

                    // ExecuteNonQuery ejecuta la instrucción SQL.
                    comando.ExecuteNonQuery();
                }
            }
        }



        // Comprueba que el archivo seleccionado sea una copia
        // de seguridad válida y que corresponda a la base de datos esperada.
        // Recibe una conexión abierta y la ruta segura del archivo.
        private void ValidarBackup(SqlConnection conexion, string rutaSegura)
        {
            // RESTORE HEADERONLY obtiene información de la cabecera
            // del archivo de backup, como el nombre de la base de datos
            // de origen y el tipo de copia de seguridad.
            string consultaCabecera = $@"
        RESTORE HEADERONLY
        FROM DISK = N'{rutaSegura}';";

            using (SqlCommand comando = new SqlCommand(
                consultaCabecera, conexion))
            {
                comando.CommandTimeout = 0;

                // ExecuteReader permite leer los datos devueltos
                // por RESTORE HEADERONLY.
                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    // Se comprueba que la consulta haya devuelto
                    // al menos un registro con información de cabecera.
                    if (!lector.Read())
                    {
                        throw new InvalidDataException(
                            "El archivo no contiene una cabecera de backup válida.");
                    }

                    // Comprobar que el backup pertenece a la base de datos esperada.
                    // Se obtiene el nombre de la base de datos desde la cabecera.
                    string nombreBase = lector["DatabaseName"].ToString();

                    // Se compara el nombre obtenido con el nombre esperado.
                    // OrdinalIgnoreCase permite ignorar diferencias entre
                    // mayúsculas y minúsculas durante la comparación.
                    if (!string.Equals(
                        nombreBase,
                        "DBSISTEMA_GIMNASIO",
                        StringComparison.OrdinalIgnoreCase))
                    {
                        throw new InvalidDataException(
                            "El backup seleccionado no pertenece a DBSISTEMA_GIMNASIO.");
                    }

                    // Comprobar que se trate de una copia completa de la base de datos.
                    // BackupType identifica el tipo de copia de seguridad.
                    // El valor 1 corresponde a una copia completa de base de datos.
                    int tipoBackup = Convert.ToInt32(lector["BackupType"]);

                    if (tipoBackup != 1)
                    {
                        throw new InvalidDataException(
                            "El archivo no contiene una copia completa de la base de datos.");
                    }
                }
            }



            // Verificar que SQL Server pueda leer y comprobar el backup.
            // RESTORE VERIFYONLY comprueba que el conjunto de backup
            // esté completo y pueda leerse correctamente.
            // No realiza una restauración real de la base de datos.
            string consultaVerificacion = $@"
        RESTORE VERIFYONLY
        FROM DISK = N'{rutaSegura}';";

            using (SqlCommand comando = new SqlCommand(
                consultaVerificacion, conexion))
            {
                comando.CommandTimeout = 0;

                // Se ejecuta la verificación del archivo.
                comando.ExecuteNonQuery();
            }
        }


        // Restaura una copia de seguridad previamente seleccionada.
        // Antes de realizar la restauración, comprueba que el archivo
        // exista y que corresponda a una copia válida de la base de datos.
        public void RestaurarBackup(string rutaOrigen)
        {
            // Se comprueba que la ruta recibida tenga contenido.
            if (string.IsNullOrWhiteSpace(rutaOrigen))
                throw new ArgumentException(
                    "La ruta del archivo no es válida.");

            // Se verifica que el archivo seleccionado exista físicamente.
            if (!File.Exists(rutaOrigen))
                throw new FileNotFoundException(
                    "No se encontró el archivo de copia de seguridad.",
                    rutaOrigen);

            // Se obtiene la ruta absoluta del archivo y se duplican
            // las comillas simples para utilizarla en las consultas SQL.
            string rutaSegura = Path.GetFullPath(rutaOrigen)
                .Replace("'", "''");

            // SqlConnectionStringBuilder permite modificar
            // de manera estructurada los parámetros de conexión.
            SqlConnectionStringBuilder constructor =
                new SqlConnectionStringBuilder(Conexion.cadena);

            // Se establece master como base de datos inicial.
            // Esto permite ejecutar las instrucciones necesarias
            // para restaurar DBSISTEMA_GIMNASIO.
            constructor.InitialCatalog = "master";

            using (SqlConnection conexion =
                   new SqlConnection(constructor.ConnectionString))
            {
                conexion.Open();

                // Primero verificamos que el archivo sea un backup válido.
                // Se comprueba su cabecera, su base de datos de origen,
                // su tipo y su integridad mediante RESTORE VERIFYONLY.
                ValidarBackup(conexion, rutaSegura);

                // Indica si debe intentarse recuperar el modo MULTI_USER
                // al finalizar el proceso, incluso si ocurre una excepción.
                bool restablecerModoMultiple = false;

                try
                {
                    // Se establece en true antes de intentar cambiar
                    // el modo de acceso de la base de datos.
                    restablecerModoMultiple = true;

                    // SINGLE_USER restringe el acceso a un único usuario.
                    // WITH ROLLBACK IMMEDIATE finaliza las conexiones existentes
                    // y revierte las transacciones que todavía no se confirmaron.
                    string ponerModoUnico = @"
                ALTER DATABASE [DBSISTEMA_GIMNASIO]
                SET SINGLE_USER WITH ROLLBACK IMMEDIATE;";

                    using (SqlCommand comando =
                           new SqlCommand(ponerModoUnico, conexion))
                    {
                        comando.CommandTimeout = 0;
                        comando.ExecuteNonQuery();
                    }

                    // Restauramos la copia de seguridad seleccionada.
                    // WITH REPLACE permite reemplazar la base de datos existente.
                    // RECOVERY deja la base de datos disponible al finalizar
                    // la restauración, sin esperar a aplicar otros backups.
                    string consultaRestaurar = $@"
                RESTORE DATABASE [DBSISTEMA_GIMNASIO]
                FROM DISK = N'{rutaSegura}'
                WITH REPLACE, RECOVERY;";

                    using (SqlCommand comando =
                           new SqlCommand(consultaRestaurar, conexion))
                    {
                        comando.CommandTimeout = 0;
                        comando.ExecuteNonQuery();
                    }
                }
                finally
                {
                    // Recuperamos el acceso normal a la base de datos.
                    // El bloque finally se ejecuta tanto si las operaciones
                    // anteriores finalizan correctamente como si ocurre un error.
                    if (restablecerModoMultiple)
                    {
                        // MULTI_USER permite nuevamente las conexiones
                        // normales de los distintos usuarios.
                        string volverModoMultiple = @"
                    ALTER DATABASE [DBSISTEMA_GIMNASIO]
                    SET MULTI_USER;";

                        using (SqlCommand comando =
                               new SqlCommand(volverModoMultiple, conexion))
                        {
                            comando.CommandTimeout = 0;
                            comando.ExecuteNonQuery();
                        }
                    }
                }
            }
        }
    }
}
