
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_Empleado
    {
        // Obtiene todos los empleados registrados en la base de datos.
        // También recupera los datos personales y el rol de cada empleado.
        public List<Empleado> Listar()
        {
            List<Empleado> lista = new List<Empleado>();

            // Se establece la conexión con la base de datos.
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    // Se consultan los empleados junto con sus datos personales y roles.
                    // INNER JOIN relaciona las tablas mediante sus claves correspondientes.
                    // Los alias permiten diferenciar columnas que tienen nombres
                    // similares en las distintas tablas.
                    string query = @"SELECT 
                        e.idEmpleado,
                        e.Fecha_Alta,
                        e.Usuario,
                        e.Contraseña,
                        e.Estado,

                        p.idPersona,
                        p.Nombre AS NombrePersona,
                        p.Apellido,
                        p.DNI,
                        p.Telefono,
                        p.Email,

                        r.idRol,
                        r.Nombre AS NombreRol
                    FROM Empleado e
                    INNER JOIN Persona p ON e.idPersona = p.idPersona
                    INNER JOIN Rol r ON e.idRol = r.idRol";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    // ExecuteReader permite recorrer los registros
                    // devueltos por la consulta.
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            // Se construye un objeto Empleado por cada registro.
                            // También se crean los objetos Persona y Rol relacionados,
                            // manteniendo la estructura de las clases de entidad.
                            lista.Add(new Empleado()
                            {
                                idEmpleado = Convert.ToInt32(dr["idEmpleado"]),
                                Fecha_Alta = Convert.ToDateTime(dr["Fecha_Alta"]),
                                Usuario = dr["Usuario"].ToString(),
                                Contraseña = dr["Contraseña"].ToString(),
                                Estado = Convert.ToBoolean(dr["Estado"]),

                                // Datos personales del empleado.
                                oPersona = new Persona()
                                {
                                    idPersona = Convert.ToInt32(dr["idPersona"]),
                                    Nombre = dr["NombrePersona"].ToString(),
                                    Apellido = dr["Apellido"].ToString(),
                                    DNI = dr["DNI"].ToString(),
                                    Telefono = dr["Telefono"].ToString(),
                                    Email = dr["Email"].ToString()
                                },

                                // Rol asignado al empleado.
                                oRol = new Rol()
                                {
                                    idRol = Convert.ToInt32(dr["idRol"]),
                                    Nombre = dr["NombreRol"].ToString()
                                }
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Se informa que ocurrió un error durante la consulta de empleados.
                    throw new Exception("Error al obtener empleados: " + ex.Message);
                }
            }

            return lista;
        }


        // Busca una persona a partir de su número de DNI.
        // Devuelve un objeto Persona si encuentra coincidencias
        // o null si no existe ningún registro con ese DNI.
        public Persona BuscarPersonaPorDNI(string dni)
        {
            Persona persona = null;

            using (SqlConnection oconexion =
                   new SqlConnection(Conexion.cadena))
            {
                // Se consultan los datos personales utilizando el DNI como criterio.
                string query = @"
            SELECT
                idPersona,
                Nombre,
                Apellido,
                DNI,
                Telefono,
                Email
            FROM Persona
            WHERE DNI = @DNI";

                using (SqlCommand cmd = new SqlCommand(query, oconexion))
                {
                    // Se agrega el parámetro indicando su tipo y longitud.
                    cmd.Parameters.Add("@DNI", SqlDbType.VarChar, 16)
                        .Value = dni;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        // Se comprueba si la consulta devolvió algún registro.
                        if (dr.Read())
                        {
                            // Se construye el objeto Persona con los datos obtenidos.
                            // Se verifica si Telefono y Email contienen DBNull,
                            // ya que la base de datos permite que esos campos sean nulos.
                            persona = new Persona()
                            {
                                idPersona = Convert.ToInt32(dr["idPersona"]),
                                Nombre = dr["Nombre"].ToString(),
                                Apellido = dr["Apellido"].ToString(),
                                DNI = dr["DNI"].ToString(),
                                Telefono = dr["Telefono"] == DBNull.Value
                                    ? ""
                                    : dr["Telefono"].ToString(),
                                Email = dr["Email"] == DBNull.Value
                                    ? ""
                                    : dr["Email"].ToString()
                            };
                        }
                    }
                }
            }

            return persona;
        }


        // Comprueba si ya existe un empleado asociado a una persona.
        // Recibe el identificador de Persona y devuelve true si encuentra
        // al menos un empleado relacionado con ese registro.
        public bool ExisteEmpleadoPorIdPersona(int idPersona)
        {
            using (SqlConnection oconexion =
                   new SqlConnection(Conexion.cadena))
            {
                // COUNT(1) cuenta los registros que cumplen la condición.
                string query = @"
            SELECT COUNT(1)
            FROM Empleado
            WHERE idPersona = @idPersona";

                using (SqlCommand cmd = new SqlCommand(query, oconexion))
                {
                    // Se establece el identificador de la persona que se desea consultar.
                    cmd.Parameters.Add("@idPersona", SqlDbType.Int)
                        .Value = idPersona;

                    oconexion.Open();

                    // Si el resultado es mayor que cero, significa que existe
                    // un empleado asociado a esa persona.
                    return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                }
            }
        }



        // Registra un nuevo empleado y sus datos personales.
        // Si la persona todavía no existe, primero la registra.
        // Ambas operaciones se realizan dentro de una misma transacción.
        // Devuelve el identificador generado para el empleado.
        public int Registrar(Empleado obj)
        {
            int idEmpleadoGenerado = 0;

            using (SqlConnection oconexion =
                   new SqlConnection(Conexion.cadena))
            {
                // La transacción permite que las operaciones de inserción
                // se confirmen o reviertan como una única unidad.
                SqlTransaction transaccion = null;

                try
                {
                    oconexion.Open();

                    // Se inicia la transacción sobre la conexión abierta.
                    transaccion = oconexion.BeginTransaction();

                    // Se obtiene el identificador de la persona asociada.
                    int idPersona = obj.oPersona.idPersona;

                    // Si la persona todavía no existe, la registramos.
                    // El valor cero indica que todavía no tiene un ID asignado.
                    if (idPersona == 0)
                    {
                        // Se inserta una nueva persona y se recupera
                        // el identificador generado automáticamente.
                        string queryPersona = @"
                    INSERT INTO Persona
                    (Nombre, Apellido, DNI, Telefono, Email)
                    VALUES
                    (@Nombre, @Apellido, @DNI, @Telefono, @Email);

                    SELECT SCOPE_IDENTITY();";

                        using (SqlCommand cmdPersona = new SqlCommand(
                            queryPersona, oconexion, transaccion))
                        {
                            // Se asignan los datos personales a los parámetros SQL.
                            cmdPersona.Parameters.AddWithValue(
                                "@Nombre", obj.oPersona.Nombre);

                            cmdPersona.Parameters.AddWithValue(
                                "@Apellido", obj.oPersona.Apellido);

                            cmdPersona.Parameters.AddWithValue(
                                "@DNI", obj.oPersona.DNI);

                            cmdPersona.Parameters.AddWithValue(
                                "@Telefono", obj.oPersona.Telefono);

                            cmdPersona.Parameters.AddWithValue(
                                "@Email", obj.oPersona.Email);

                            // Se recupera el ID de la persona recién insertada.
                            idPersona = Convert.ToInt32(
                                cmdPersona.ExecuteScalar());
                        }
                    }

                    // Registramos el empleado utilizando el idPersona
                    // nuevo o existente, según corresponda.
                    // Se inserta el empleado relacionándolo con su persona y su rol.
                    string queryEmpleado = @"
                INSERT INTO Empleado
                (idPersona, idRol, Usuario, Contraseña)
                VALUES
                (@idPersona, @idRol, @Usuario, @Contraseña);

                SELECT SCOPE_IDENTITY();";

                    using (SqlCommand cmdEmpleado = new SqlCommand(
                        queryEmpleado, oconexion, transaccion))
                    {
                        // Se asignan los datos correspondientes al empleado.
                        cmdEmpleado.Parameters.AddWithValue(
                            "@idPersona", idPersona);

                        cmdEmpleado.Parameters.AddWithValue(
                            "@idRol", obj.oRol.idRol);

                        cmdEmpleado.Parameters.AddWithValue(
                            "@Usuario", obj.Usuario);

                        cmdEmpleado.Parameters.AddWithValue(
                            "@Contraseña", obj.Contraseña);

                        // Se recupera el identificador generado para el empleado.
                        idEmpleadoGenerado = Convert.ToInt32(
                            cmdEmpleado.ExecuteScalar());
                    }

                    // Si ambas inserciones se realizaron correctamente,
                    // se confirman los cambios en la base de datos.
                    transaccion.Commit();
                }
                catch (Exception ex)
                {
                    // Si ocurre un error, se revierten los cambios realizados
                    // dentro de la transacción para evitar registros incompletos.
                    if (transaccion != null)
                        transaccion.Rollback();

                    throw new Exception(
                        "Error al registrar usuario: " + ex.Message);
                }
            }

            return idEmpleadoGenerado;
        }


        // Modifica los datos personales y laborales de un empleado.
        // También permite actualizar su contraseña de manera opcional.
        // Devuelve true si las operaciones se completan correctamente.
        public bool Editar(Empleado obj)
        {
            bool respuesta = false;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                // Se utiliza una transacción para garantizar que las modificaciones
                // de Persona y Empleado se realicen de forma conjunta.
                SqlTransaction transaccion = null;

                try
                {
                    oconexion.Open();

                    transaccion = oconexion.BeginTransaction();


                    // Se actualizan los datos personales de la persona asociada.
                    string queryPersona = @"
                UPDATE Persona
                SET
                    Nombre = @Nombre,
                    Apellido = @Apellido,
                    DNI = @DNI,
                    Telefono = @Telefono,
                    Email = @Email
                WHERE idPersona = @idPersona";

                    // El comando utiliza la misma conexión y transacción.
                    SqlCommand cmdPersona = new SqlCommand(
                        queryPersona,
                        oconexion,
                        transaccion);

                    // Se asignan los nuevos valores de los datos personales.
                    cmdPersona.Parameters.AddWithValue(
                        "@Nombre",
                        obj.oPersona.Nombre);

                    cmdPersona.Parameters.AddWithValue(
                        "@Apellido",
                        obj.oPersona.Apellido);

                    cmdPersona.Parameters.AddWithValue(
                        "@DNI",
                        obj.oPersona.DNI);

                    cmdPersona.Parameters.AddWithValue(
                        "@Telefono",
                        obj.oPersona.Telefono);

                    cmdPersona.Parameters.AddWithValue(
                        "@Email",
                        obj.oPersona.Email);

                    cmdPersona.Parameters.AddWithValue(
                        "@idPersona",
                        obj.oPersona.idPersona);

                    // Se ejecuta la actualización de Persona.
                    cmdPersona.ExecuteNonQuery();

                    string queryEmpleado;

                    // Si la contraseña está vacía o contiene espacios,
                    // se actualizan el rol y el nombre de usuario,
                    // conservando la contraseña que ya tenía el empleado.
                    if (string.IsNullOrWhiteSpace(obj.Contraseña))
                    {
                        queryEmpleado = @"
                    UPDATE Empleado
                    SET
                        idRol = @idRol,
                        Usuario = @Usuario
                    WHERE idEmpleado = @idEmpleado";
                    }
                    else
                    {
                        // Si se proporcionó una contraseña,
                        // también se incluye en la actualización.
                        queryEmpleado = @"
                    UPDATE Empleado
                    SET
                        idRol = @idRol,
                        Usuario = @Usuario,
                        Contraseña = @Contraseña
                    WHERE idEmpleado = @idEmpleado";
                    }

                    SqlCommand cmdEmpleado = new SqlCommand(
                        queryEmpleado,
                        oconexion,
                        transaccion);

                    // Se asignan los datos laborales que se van a actualizar.
                    cmdEmpleado.Parameters.AddWithValue(
                        "@idRol",
                        obj.oRol.idRol);

                    cmdEmpleado.Parameters.AddWithValue(
                        "@Usuario",
                        obj.Usuario);

                    cmdEmpleado.Parameters.AddWithValue(
                        "@idEmpleado",
                        obj.idEmpleado);

                    // El parámetro de contraseña solamente se agrega
                    // cuando se seleccionó la consulta que la actualiza.
                    if (!string.IsNullOrWhiteSpace(obj.Contraseña))
                    {
                        cmdEmpleado.Parameters.AddWithValue(
                            "@Contraseña",
                            obj.Contraseña);
                    }

                    // Se ejecuta la actualización del empleado.
                    cmdEmpleado.ExecuteNonQuery();


                    // Se confirman las modificaciones de ambas tablas.
                    transaccion.Commit();

                    respuesta = true;
                }
                catch (Exception ex)
                {
                    // Ante un error se revierten las modificaciones
                    // realizadas dentro de la transacción.
                    if (transaccion != null)
                        transaccion.Rollback();

                    throw new Exception(
                        "Error al editar empleado: " + ex.Message);
                }
            }

            return respuesta;
        }

        // Realiza una baja lógica del empleado.
        // En lugar de eliminar físicamente el registro,
        // modifica su estado para marcarlo como inactivo.
        public bool Eliminar(int idEmpleado)
        {
            bool respuesta = false;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    // Se actualiza el estado del empleado a cero.
                    string query = @"
                UPDATE Empleado
                SET Estado = 0
                WHERE idEmpleado = @idEmpleado";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    // Se identifica al empleado que se desea desactivar.
                    cmd.Parameters.AddWithValue("@idEmpleado", idEmpleado);

                    oconexion.Open();

                    // ExecuteNonQuery devuelve la cantidad de filas afectadas.
                    // Si se modificó al menos una fila, la operación fue exitosa.
                    respuesta = cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception(
                        "Error al eliminar empleado: " + ex.Message);
                }
            }

            return respuesta;
        }

        // Reactiva un empleado que se encontraba inactivo.
        // Cambia su estado a uno sin eliminar ni crear registros nuevos.
        public bool Activar(int idEmpleado)
        {
            bool respuesta = false;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    // Se actualiza el estado del empleado a uno.
                    string query = @"
                UPDATE Empleado
                SET Estado = 1
                WHERE idEmpleado = @idEmpleado";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    // Se identifica al empleado que se desea reactivar.
                    cmd.Parameters.AddWithValue("@idEmpleado", idEmpleado);

                    oconexion.Open();

                    // Se comprueba si la consulta modificó algún registro.
                    respuesta = cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception(
                        "Error al activar empleado: " + ex.Message);
                }
            }

            return respuesta;
        }

        // Comprueba que dos empleados diferentes autoricen una operación.
        // Ambos deben tener credenciales válidas, pertenecer al rol administrador
        // y encontrarse activos.
        // Devuelve true únicamente si se cumplen todas las condiciones.
        public bool ValidarDobleAutorizacion(
        string usuario1,
        string contraseña1,
        string usuario2,
        string contraseña2)
        {
            using (SqlConnection oconexion =
                   new SqlConnection(Conexion.cadena))
            {
                // EXISTS comprueba si existe al menos un registro
                // que cumpla todas las condiciones de autorización.
                // Se relacionan dos instancias de Empleado mediante un INNER JOIN.
                // La condición e1.idEmpleado <> e2.idEmpleado garantiza
                // que las credenciales correspondan a dos empleados diferentes.
                // idRol = 1 identifica al rol administrador.
                // Estado = 1 exige que ambos empleados estén activos.
                string query = @"
            SELECT CASE
                WHEN EXISTS (
                    SELECT 1
                    FROM Empleado e1
                    INNER JOIN Empleado e2
                        ON e1.idEmpleado <> e2.idEmpleado
                    WHERE
                        e1.Usuario = @Usuario1
                        AND e1.Contraseña = @Contraseña1
                        AND e1.idRol = 1
                        AND e1.Estado = 1

                        AND e2.Usuario = @Usuario2
                        AND e2.Contraseña = @Contraseña2
                        AND e2.idRol = 1
                        AND e2.Estado = 1
                )
                THEN 1
                ELSE 0
            END;";

                using (SqlCommand cmd = new SqlCommand(query, oconexion))
                {
                    // Se agregan las credenciales de los dos administradores
                    // mediante parámetros SQL, indicando sus tipos y longitudes.
                    cmd.Parameters.Add("@Usuario1", SqlDbType.VarChar, 50)
                        .Value = usuario1;

                    cmd.Parameters.Add("@Contraseña1", SqlDbType.VarChar, 100)
                        .Value = contraseña1;

                    cmd.Parameters.Add("@Usuario2", SqlDbType.VarChar, 50)
                        .Value = usuario2;

                    cmd.Parameters.Add("@Contraseña2", SqlDbType.VarChar, 100)
                        .Value = contraseña2;

                    oconexion.Open();

                    // ExecuteScalar obtiene el resultado del CASE:
                    // devuelve 1 si se cumplen las condiciones de autorización
                    // y 0 en caso contrario.
                    return Convert.ToInt32(cmd.ExecuteScalar()) == 1;
                }
            }
        }

    }
}
