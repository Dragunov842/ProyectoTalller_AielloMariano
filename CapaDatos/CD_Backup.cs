
using Microsoft.Data.SqlClient;
using System;
using System.IO;

namespace CapaDatos
{
    public class CD_Backup
    {
        public void CrearBackup(string rutaDestino)
        {
            if (string.IsNullOrWhiteSpace(rutaDestino))
                throw new ArgumentException(
                    "La ruta de destino no es válida.");

            string rutaSegura = Path.GetFullPath(rutaDestino)
                .Replace("'", "''");

            using (SqlConnection conexion =
                   new SqlConnection(Conexion.cadena))
            {
                conexion.Open();

                string consulta = $@"
                    BACKUP DATABASE [DBSISTEMA_GIMNASIO]
                    TO DISK = N'{rutaSegura}'
                    WITH INIT, STATS = 10;";

                using (SqlCommand comando =
                       new SqlCommand(consulta, conexion))
                {
                    comando.CommandTimeout = 0;
                    comando.ExecuteNonQuery();
                }
            }
        }


        private void ValidarBackup(SqlConnection conexion, string rutaSegura)
        {
            string consultaCabecera = $@"
        RESTORE HEADERONLY
        FROM DISK = N'{rutaSegura}';";

            using (SqlCommand comando = new SqlCommand(
                consultaCabecera, conexion))
            {
                comando.CommandTimeout = 0;

                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    if (!lector.Read())
                    {
                        throw new InvalidDataException(
                            "El archivo no contiene una cabecera de backup válida.");
                    }
                }
            }

            string consultaVerificacion = $@"
        RESTORE VERIFYONLY
        FROM DISK = N'{rutaSegura}';";

            using (SqlCommand comando = new SqlCommand(
                consultaVerificacion, conexion))
            {
                comando.CommandTimeout = 0;
                comando.ExecuteNonQuery();
            }
        }


        public void RestaurarBackup(string rutaOrigen)
        {
            if (string.IsNullOrWhiteSpace(rutaOrigen))
                throw new ArgumentException(
                    "La ruta del archivo no es válida.");

            if (!File.Exists(rutaOrigen))
                throw new FileNotFoundException(
                    "No se encontró el archivo de copia de seguridad.",
                    rutaOrigen);

            string rutaSegura = Path.GetFullPath(rutaOrigen)
                .Replace("'", "''");

            SqlConnectionStringBuilder constructor =
                new SqlConnectionStringBuilder(Conexion.cadena);

            constructor.InitialCatalog = "master";

            using (SqlConnection conexion =
                   new SqlConnection(constructor.ConnectionString))
            {
                conexion.Open();
                ValidarBackup(conexion, rutaSegura);

                string ponerModoUnico = @"
                ALTER DATABASE [DBSISTEMA_GIMNASIO]
                SET SINGLE_USER WITH ROLLBACK IMMEDIATE;";

                using (SqlCommand comando =
                       new SqlCommand(ponerModoUnico, conexion))
                {
                    comando.CommandTimeout = 0;
                    comando.ExecuteNonQuery();
                }

                try
                {
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
