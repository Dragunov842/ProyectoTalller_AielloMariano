using CapaEntidad;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CapaDatos
{
    public class CD_Alumno
    {
        
        public List<Alumno> Listar()
        {
            List<Alumno> lista = new List<Alumno>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
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

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
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
                    throw new Exception("Error al obtener alumnos: " + ex.Message);
                }
            }

            return lista;
        }


        public DataTable ListarConMembresia()
        {
            DataTable dt = new DataTable();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
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

                    SqlDataAdapter da = new SqlDataAdapter(query, oconexion);

                    da.Fill(dt);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al listar alumnos con membresía: " + ex.Message);
                }
            }

            return dt;
        }


        public Alumno BuscarPorId(int idAlumno)
        {
            Alumno obj = null;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
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

                    cmd.Parameters.AddWithValue("@idAlumno", idAlumno);

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
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


        public int Registrar(Alumno obj)
        {
            int idAlumnoGenerado = 0;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                SqlTransaction transaccion = null;

                try
                {
                    oconexion.Open();

                    transaccion = oconexion.BeginTransaction();


                    string queryPersona = @"
                INSERT INTO Persona (Nombre, Apellido, DNI, Telefono, Email)
                VALUES (@Nombre, @Apellido, @DNI, @Telefono, @Email);

                SELECT SCOPE_IDENTITY();";

                    SqlCommand cmdPersona = new SqlCommand(queryPersona, oconexion, transaccion);

                    cmdPersona.Parameters.AddWithValue("@Nombre", obj.oPersona.Nombre);
                    cmdPersona.Parameters.AddWithValue("@Apellido", obj.oPersona.Apellido);
                    cmdPersona.Parameters.AddWithValue("@DNI", obj.oPersona.DNI);
                    cmdPersona.Parameters.AddWithValue("@Telefono", obj.oPersona.Telefono);
                    cmdPersona.Parameters.AddWithValue("@Email", obj.oPersona.Email);

                    int idPersonaGenerado = Convert.ToInt32(cmdPersona.ExecuteScalar());


                    string queryAlumno = @"
                INSERT INTO Alumno (idPersona)
                VALUES (@idPersona);

                SELECT SCOPE_IDENTITY();";

                    SqlCommand cmdAlumno = new SqlCommand(queryAlumno, oconexion, transaccion);

                    cmdAlumno.Parameters.AddWithValue("@idPersona", idPersonaGenerado);

                    idAlumnoGenerado = Convert.ToInt32(cmdAlumno.ExecuteScalar());


                    transaccion.Commit();
                }
                catch (Exception ex)
                {
                    if (transaccion != null)
                        transaccion.Rollback();

                    throw new Exception("Error al registrar alumno: " + ex.Message);
                }
            }

            return idAlumnoGenerado;
        }

        public bool Editar(Alumno obj)
        {
            bool respuesta = false;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
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

                    cmd.Parameters.AddWithValue("@Nombre", obj.oPersona.Nombre);
                    cmd.Parameters.AddWithValue("@Apellido", obj.oPersona.Apellido);
                    cmd.Parameters.AddWithValue("@DNI", obj.oPersona.DNI);
                    cmd.Parameters.AddWithValue("@Telefono", obj.oPersona.Telefono);
                    cmd.Parameters.AddWithValue("@Email", obj.oPersona.Email);
                    cmd.Parameters.AddWithValue("@idPersona", obj.oPersona.idPersona);

                    oconexion.Open();

                    respuesta = cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al editar alumno: " + ex.Message);
                }
            }

            return respuesta;
        }

        public bool Eliminar(int idAlumno)
        {
            bool respuesta = false;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    string query = @"
                UPDATE Alumno
                SET Estado = 0
                WHERE idAlumno = @idAlumno";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@idAlumno", idAlumno);

                    oconexion.Open();

                    respuesta = cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al eliminar alumno: " + ex.Message);
                }
            }

            return respuesta;
        }

        public bool Activar(int idAlumno)
        {
            bool respuesta = false;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    string query = @"
                UPDATE Alumno
                SET Estado = 1
                WHERE idAlumno = @idAlumno";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@idAlumno", idAlumno);

                    oconexion.Open();

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