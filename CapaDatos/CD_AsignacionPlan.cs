using CapaEntidad;
using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace CapaDatos
{
    public class CD_AsignacionPlan
    {

        public int Registrar(AsignacionPlan obj)
        {
            int idGenerado = 0;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    string query = @"
                        INSERT INTO AsignacionPlan
                        (idAlumno, idPlan, FechaInicio, FechaFin, Estado)
                        VALUES
                        (@idAlumno, @idPlan, @FechaInicio, @FechaFin, @Estado);

                        SELECT SCOPE_IDENTITY();";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@idAlumno", obj.oAlumno.idAlumno);
                    cmd.Parameters.AddWithValue("@idPlan", obj.oPlan.idPlan);
                    cmd.Parameters.AddWithValue("@FechaInicio", obj.FechaInicio);
                    cmd.Parameters.AddWithValue("@FechaFin", obj.FechaFin);
                    cmd.Parameters.AddWithValue("@Estado", obj.Estado);

                    oconexion.Open();

                    idGenerado = Convert.ToInt32(cmd.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al registrar asignación de plan: " + ex.Message);
                }
            }

            return idGenerado;
        }

        public int ContarActivos(int idAlumno)
        {
            int cantidad = 0;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    string query = @"
                        SELECT COUNT(*)
                        FROM AsignacionPlan
                        WHERE idAlumno = @idAlumno
                          AND Estado = 1
                          AND CAST(GETDATE() AS date) <= FechaFin";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@idAlumno", idAlumno);

                    oconexion.Open();

                    cantidad = Convert.ToInt32(cmd.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al contar planes activos: " + ex.Message);
                }
            }

            return cantidad;
        }

        public List<AsignacionPlan> ListarActivosPorAlumno(int idAlumno)
        {
            List<AsignacionPlan> lista = new List<AsignacionPlan>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    string query = @"
                SELECT
                    ap.idAsignacion,
                    ap.idAlumno,
                    ap.idPlan,
                    ap.FechaInicio,
                    ap.FechaFin,
                    ap.Estado,

                    p.Nombre,
                    p.Descripcion,
                    p.Precio,
                    p.Duracion

                FROM AsignacionPlan ap
                INNER JOIN PlanEntrenamiento p
                    ON ap.idPlan = p.idPlan

                WHERE ap.idAlumno = @idAlumno
                  AND ap.Estado = 1
                  AND CAST(GETDATE() AS date) <= ap.FechaFin

                ORDER BY ap.FechaFin ASC";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@idAlumno", idAlumno);

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new AsignacionPlan()
                            {
                                idAsignacion = Convert.ToInt32(dr["idAsignacion"]),

                                oAlumno = new Alumno()
                                {
                                    idAlumno = Convert.ToInt32(dr["idAlumno"])
                                },

                                oPlan = new PlanEntrenamiento()
                                {
                                    idPlan = Convert.ToInt32(dr["idPlan"]),
                                    Nombre = dr["Nombre"].ToString(),
                                    Descripcion = dr["Descripcion"].ToString(),
                                    Precio = Convert.ToDecimal(dr["Precio"]),
                                    Duracion = Convert.ToInt32(dr["Duracion"])
                                },

                                FechaInicio = Convert.ToDateTime(dr["FechaInicio"]),
                                FechaFin = Convert.ToDateTime(dr["FechaFin"]),
                                Estado = Convert.ToBoolean(dr["Estado"])
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(
                        "Error al obtener los planes activos del alumno: " + ex.Message
                    );
                }
            }

            return lista;
        }


        public bool YaTienePlanActivo(int idAlumno, int idPlan)
        {
            bool existe = false;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    string query = @"
                        SELECT COUNT(*)
                        FROM AsignacionPlan
                        WHERE idAlumno = @idAlumno
                          AND idPlan = @idPlan
                          AND Estado = 1
                          AND CAST(GETDATE() AS date) <= FechaFin";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@idAlumno", idAlumno);
                    cmd.Parameters.AddWithValue("@idPlan", idPlan);

                    oconexion.Open();

                    existe = Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al verificar plan activo: " + ex.Message);
                }
            }

            return existe;
        }
    }
}