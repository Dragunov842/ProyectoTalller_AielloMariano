using CapaEntidad;
using CapaEntidad.Membresias;
using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace CapaDatos
{
    public class CD_AsignacionMembresia
    {
        public AsignacionMembresia BuscarActiva(int idAlumno)
        {
            AsignacionMembresia obj = null;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    string query = @"
                SELECT TOP 1
                    am.idAsignacionMembresia,
                    am.idAlumno,
                    am.idMembresia,
                    am.FechaInicio,
                    am.FechaFin,
                    am.Estado,

                    m.Nombre,
                    m.Descripcion,
                    m.Precio,
                    m.Duracion

                FROM AsignacionMembresia am

                INNER JOIN Membresia m
                    ON am.idMembresia = m.idMembresia

                WHERE am.idAlumno = @idAlumno
                  AND am.Estado = 1
                  AND CAST(GETDATE() AS date) <= am.FechaFin

                ORDER BY am.FechaFin DESC";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@idAlumno", idAlumno);

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            obj = new AsignacionMembresia()
                            {
                                idAsignacionMembresia =
                                    Convert.ToInt32(dr["idAsignacionMembresia"]),

                                oAlumno = new Alumno()
                                {
                                    idAlumno = Convert.ToInt32(dr["idAlumno"])
                                },

                                oMembresia = new Membresia()
                                {
                                    idMembresia =
                                        Convert.ToInt32(dr["idMembresia"]),

                                    Nombre = dr["Nombre"].ToString(),
                                    Descripcion = dr["Descripcion"].ToString(),
                                    Precio = Convert.ToDecimal(dr["Precio"]),
                                    Duracion = Convert.ToInt32(dr["Duracion"])
                                },

                                FechaInicio =
                                    Convert.ToDateTime(dr["FechaInicio"]),

                                FechaFin =
                                    Convert.ToDateTime(dr["FechaFin"]),

                                Estado =
                                    Convert.ToBoolean(dr["Estado"])
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(
                        "Error al buscar membresía activa: " + ex.Message
                    );
                }
            }

            return obj;
        }
        public int Registrar(AsignacionMembresia obj)
        {
            int idGenerado = 0;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    string query = @"
                        INSERT INTO AsignacionMembresia
                        (idAlumno, idMembresia, FechaInicio, FechaFin, Estado)
                        VALUES
                        (@idAlumno, @idMembresia, @FechaInicio, @FechaFin, @Estado);

                        SELECT SCOPE_IDENTITY();";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@idAlumno", obj.oAlumno.idAlumno);
                    cmd.Parameters.AddWithValue("@idMembresia", obj.oMembresia.idMembresia);
                    cmd.Parameters.AddWithValue("@FechaInicio", obj.FechaInicio);
                    cmd.Parameters.AddWithValue("@FechaFin", obj.FechaFin);
                    cmd.Parameters.AddWithValue("@Estado", obj.Estado);

                    oconexion.Open();

                    idGenerado = Convert.ToInt32(cmd.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al registrar asignación de membresía: " + ex.Message);
                }
            }

            return idGenerado;
        }

        public bool ActualizarFechaFin(int idAsignacionMembresia, DateTime nuevaFechaFin)
        {
            bool respuesta = false;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    string query = @"
                        UPDATE AsignacionMembresia
                        SET FechaFin = @FechaFin
                        WHERE idAsignacionMembresia = @idAsignacionMembresia";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@FechaFin", nuevaFechaFin);
                    cmd.Parameters.AddWithValue("@idAsignacionMembresia", idAsignacionMembresia);

                    oconexion.Open();

                    respuesta = cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al actualizar fecha de membresía: " + ex.Message);
                }
            }

            return respuesta;
        }
    }
}