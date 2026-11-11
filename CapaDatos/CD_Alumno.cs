
using CapaEntidad;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CapaDatos
{
    public class CD_Alumno
    {
        // Obtiene todos los alumnos registrados junto con sus datos personales.
        // Devuelve una lista de objetos Alumno con sus respectivas Persona asociadas.
        public List<Alumno> Listar()
        {
            List<Alumno> lista = new List<Alumno>();

            // Se establece la conexión con la base de datos utilizando la cadena configurada.
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    // Consulta que relaciona las tablas Alumno y Persona
                    // para recuperar tanto los datos del alumno como los personales.
                    string query = @"SELECT
                                    a.idAlumno,
                                    a.Fecha_Alta,
                                    a.Estado,

                                    p.idPersona,
                                    p.Nombre,
                                    p.Apellido,
                                    p.DNI,
                                    p.Telefono,
                                    p.Email

                                FROM Alumno a
                                INNER JOIN Persona p
                                    ON a.idPersona = p.idPersona";

                    // Se prepara el comando SQL que ejecutará la consulta.
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    // Se abre la conexión antes de ejecutar la consulta.
                    oconexion.Open();

                    // SqlDataReader permite recorrer los registros devueltos por SQL Server.
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            // Por cada registro se construye un objeto Alumno.
                            // Los datos personales se almacenan en un objeto Persona asociado.
                            lista.Add(new Alumno()
                            {
                                idAlumno = Convert.ToInt32(dr["idAlumno"]),
                                Fecha_Alta = Convert.ToDateTime(dr["Fecha_Alta"]),
                                Estado = Convert.ToBoolean(dr["Estado"]),

                                oPersona = new Persona()
                                {
                                    idPersona = Convert.ToInt32(dr["idPersona"]),
                                    Nombre = dr["Nombre"].ToString(),
                                    Apellido = dr["Apellido"].ToString(),
                                    DNI = dr["DNI"].ToString(),
                                    Telefono = dr["Telefono"].ToString(),
                                    Email = dr["Email"].ToString()
                                }
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Si ocurre un error durante la consulta, se informa mediante una excepción.
                    throw new Exception("Error al obtener alumnos: " + ex.Message);
                }
            }

            // Se devuelve la lista completa de alumnos obtenida.
            return lista;
        }


        // Obtiene los alumnos activos junto con la información de sus membresías.
        // Devuelve un DataTable para facilitar su visualización en controles como DataGridView.
        public DataTable ListarConMembresia()
        {
            DataTable dt = new DataTable();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    // Se consultan los datos personales del alumno y su membresía asignada.
                    // LEFT JOIN permite incluir alumnos que todavía no tienen una membresía.
                    // CASE determina si la membresía está activa, vencida o si no existe.
                    string query = @"
            SELECT
                a.idAlumno,
                p.Nombre,
                p.Apellido,
                p.DNI,

                m.Nombre AS Membresia,

                am.FechaInicio,
                am.FechaFin,

                CASE
                    WHEN am.idAsignacionMembresia IS NULL
                        THEN 'Sin membresía'

                    WHEN CAST(GETDATE() AS DATE) <= am.FechaFin
                        THEN 'Activa'

                    ELSE 'Vencida'
                END AS EstadoMembresia

            FROM Alumno a

            INNER JOIN Persona p
                ON a.idPersona = p.idPersona

            LEFT JOIN AsignacionMembresia am
                ON am.idAlumno = a.idAlumno
                AND am.Estado = 1

            LEFT JOIN Membresia m
                ON m.idMembresia = am.idMembresia

            WHERE a.Estado = 1";

                    // SqlDataAdapter ejecuta la consulta y carga los resultados en el DataTable.
                    SqlDataAdapter da = new SqlDataAdapter(query, oconexion);

                    da.Fill(dt);
                }
                catch (Exception ex)
                {
                    // Se informa cualquier error producido durante la obtención de los datos.
                    throw new Exception("Error al listar alumnos con membresía: " + ex.Message);
                }
            }

            return dt;
        }


        // Busca un alumno específico utilizando su identificador.
        // Si lo encuentra, devuelve el objeto Alumno con sus datos personales.
        // Si no existe, devuelve null.
        public Alumno BuscarPorId(int idAlumno)
        {
            Alumno obj = null;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    // La consulta utiliza un parámetro para identificar al alumno buscado.
                    string query = @"
                SELECT
                    a.idAlumno,
                    a.Fecha_Alta,
                    a.Estado,

                    p.idPersona,
                    p.Nombre,
                    p.Apellido,
                    p.DNI,
                    p.Telefono,
                    p.Email

                FROM Alumno a
                INNER JOIN Persona p
                    ON a.idPersona = p.idPersona
                WHERE a.idAlumno = @idAlumno";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    // Se asigna el identificador recibido al parámetro de la consulta.
                    cmd.Parameters.AddWithValue("@idAlumno", idAlumno);

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        // Si existe un registro, se construye el objeto correspondiente.
                        if (dr.Read())
                        {
                            obj = new Alumno()
                            {
                                idAlumno = Convert.ToInt32(dr["idAlumno"]),
                                Fecha_Alta = Convert.ToDateTime(dr["Fecha_Alta"]),
                                Estado = Convert.ToBoolean(dr["Estado"]),

                                oPersona = new Persona()
                                {
                                    idPersona = Convert.ToInt32(dr["idPersona"]),
                                    Nombre = dr["Nombre"].ToString(),
                                    Apellido = dr["Apellido"].ToString(),
                                    DNI = dr["DNI"].ToString(),
                                    Telefono = dr["Telefono"].ToString(),
                                    Email = dr["Email"].ToString()
                                }
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al buscar alumno: " + ex.Message);
                }
            }

            return obj;
        }

        // Busca una persona en la tabla Persona utilizando su DNI.
        // Permite comprobar si los datos personales ya existen antes de registrar un alumno.
        public Persona BuscarPersonaPorDNI(string dni)
        {
            Persona persona = null;

            using (SqlConnection oconexion =
                   new SqlConnection(Conexion.cadena))
            {
                // Se recuperan los datos personales de la persona cuyo DNI coincide con el recibido.
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
                    // Se agrega el DNI como parámetro de tipo VARCHAR con una longitud máxima de 16.
                    cmd.Parameters.Add("@DNI", SqlDbType.VarChar, 16)
                        .Value = dni;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        // Si se encuentra una coincidencia, se construye el objeto Persona.
                        if (dr.Read())
                        {
                            persona = new Persona()
                            {
                                idPersona = Convert.ToInt32(dr["idPersona"]),
                                Nombre = dr["Nombre"].ToString(),
                                Apellido = dr["Apellido"].ToString(),
                                DNI = dr["DNI"].ToString(),

                                // Si el teléfono o el email contienen NULL en la base de datos,
                                // se asigna una cadena vacía para evitar problemas de conversión.
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

        // Comprueba si ya existe un registro de alumno asociado a una persona determinada.
        // Devuelve true si encuentra al menos uno y false si no existe ninguno.
        public bool ExisteAlumnoPorIdPersona(int idPersona)
        {
            using (SqlConnection oconexion =
                   new SqlConnection(Conexion.cadena))
            {
                // COUNT(1) cuenta los registros que coinciden con el identificador de persona.
                string query = @"
            SELECT COUNT(1)
            FROM Alumno
            WHERE idPersona = @idPersona";

                using (SqlCommand cmd = new SqlCommand(query, oconexion))
                {
                    // Se establece el identificador de persona que se desea verificar.
                    cmd.Parameters.Add("@idPersona", SqlDbType.Int)
                        .Value = idPersona;

                    oconexion.Open();

                    // ExecuteScalar obtiene el primer valor de la consulta.
                    // Se convierte el resultado a entero y se comprueba si es mayor que cero.
                    return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                }
            }
        }

        // Registra un nuevo alumno en la base de datos.
        // Si la persona asociada todavía no existe, primero la registra.
        // Ambas operaciones se ejecutan dentro de una misma transacción.
        // Devuelve el identificador generado para el nuevo alumno.
        public int Registrar(Alumno obj)
        {
            int idAlumnoGenerado = 0;

            using (SqlConnection oconexion =
                   new SqlConnection(Conexion.cadena))
            {
                SqlTransaction transaccion = null;

                try
                {
                    // Se abre la conexión y se inicia una transacción.
                    // Esto permite confirmar o deshacer conjuntamente las operaciones realizadas.
                    oconexion.Open();
                    transaccion = oconexion.BeginTransaction();

                    // Se obtiene el identificador de la persona asociada al alumno.
                    int idPersona = obj.oPersona.idPersona;

                    // Si el identificador es cero, significa que la persona todavía no existe
                    // y debe registrarse antes de crear el alumno.
                    if (idPersona == 0)
                    {
                        string queryPersona = @"
                    INSERT INTO Persona
                    (Nombre, Apellido, DNI, Telefono, Email)
                    VALUES
                    (@Nombre, @Apellido, @DNI, @Telefono, @Email);

                    SELECT SCOPE_IDENTITY();";

                        using (SqlCommand cmdPersona = new SqlCommand(
                            queryPersona, oconexion, transaccion))
                        {
                            // Se envían los datos personales mediante parámetros SQL.
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

                            // SCOPE_IDENTITY devuelve el último identificador generado
                            // en el ámbito de esta operación INSERT.
                            // Se utiliza para asociar posteriormente el alumno con la persona.
                            idPersona = Convert.ToInt32(
                                cmdPersona.ExecuteScalar());
                        }
                    }

                    // Se registra el alumno utilizando el identificador de la persona,
                    // tanto si acaba de crearse como si ya existía.
                    string queryAlumno = @"
                INSERT INTO Alumno (idPersona)
                VALUES (@idPersona);

                SELECT SCOPE_IDENTITY();";

                    using (SqlCommand cmdAlumno = new SqlCommand(
                        queryAlumno, oconexion, transaccion))
                    {
                        // Se establece la relación entre el alumno y la persona.
                        cmdAlumno.Parameters.AddWithValue(
                            "@idPersona", idPersona);

                        // Se obtiene el identificador generado para el nuevo alumno.
                        idAlumnoGenerado = Convert.ToInt32(
                            cmdAlumno.ExecuteScalar());
                    }

                    // Si ambas operaciones finalizaron correctamente,
                    // se confirman los cambios en la base de datos.
                    transaccion.Commit();
                }
                catch (Exception ex)
                {
                    // Si ocurre un error, se deshacen las operaciones realizadas
                    // para evitar que quede registrada solamente una parte del proceso.
                    if (transaccion != null)
                        transaccion.Rollback();

                    throw new Exception(
                        "Error al registrar alumno: " + ex.Message);
                }
            }

            return idAlumnoGenerado;
        }



        // Modifica los datos personales de la persona asociada al alumno.
        // Devuelve true si la actualización afecta al menos un registro.
        public bool Editar(Alumno obj)
        {
            bool respuesta = false;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    // Se actualizan los datos de la tabla Persona.
                    // La identificación del registro se realiza mediante idPersona.
                    string query = @"
                UPDATE Persona
                SET
                    Nombre = @Nombre,
                    Apellido = @Apellido,
                    DNI = @DNI,
                    Telefono = @Telefono,
                    Email = @Email
                WHERE idPersona = @idPersona";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    // Se asignan los nuevos valores a los parámetros de la consulta.
                    cmd.Parameters.AddWithValue("@Nombre", obj.oPersona.Nombre);
                    cmd.Parameters.AddWithValue("@Apellido", obj.oPersona.Apellido);
                    cmd.Parameters.AddWithValue("@DNI", obj.oPersona.DNI);
                    cmd.Parameters.AddWithValue("@Telefono", obj.oPersona.Telefono);
                    cmd.Parameters.AddWithValue("@Email", obj.oPersona.Email);
                    cmd.Parameters.AddWithValue("@idPersona", obj.oPersona.idPersona);

                    oconexion.Open();

                    // ExecuteNonQuery devuelve la cantidad de filas afectadas.
                    // La respuesta será true si se modificó al menos una.
                    respuesta = cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al editar alumno: " + ex.Message);
                }
            }

            return respuesta;
        }

        // Realiza una baja lógica del alumno.
        // No elimina físicamente el registro, sino que cambia su estado a inactivo.
        public bool Eliminar(int idAlumno)
        {
            bool respuesta = false;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    // Se actualiza el estado del alumno a 0 (inactivo).
                    string query = @"
                UPDATE Alumno
                SET Estado = 0
                WHERE idAlumno = @idAlumno";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    // Se establece el identificador del alumno que se desea desactivar.
                    cmd.Parameters.AddWithValue("@idAlumno", idAlumno);

                    oconexion.Open();

                    // Se comprueba si la operación modificó algún registro.
                    respuesta = cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al eliminar alumno: " + ex.Message);
                }
            }

            return respuesta;
        }

        // Reactiva un alumno que anteriormente había sido dado de baja lógica.
        // Cambia su estado de 0 (inactivo) a 1 (activo).
        public bool Activar(int idAlumno)
        {
            bool respuesta = false;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    // Se actualiza el estado del alumno a 1 (activo).
                    string query = @"
                UPDATE Alumno
                SET Estado = 1
                WHERE idAlumno = @idAlumno";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    // Se establece el identificador del alumno que se desea reactivar.
                    cmd.Parameters.AddWithValue("@idAlumno", idAlumno);

                    oconexion.Open();

                    // Se comprueba si la operación modificó algún registro.
                    respuesta = cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al activar alumno: " + ex.Message);
                }
            }

            return respuesta;
        }

    }
}
