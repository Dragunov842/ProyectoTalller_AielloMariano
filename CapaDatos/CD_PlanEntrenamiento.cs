using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_PlanEntrenamiento
    {
        public List<PlanEntrenamiento> Listar()
        {
            List<PlanEntrenamiento> lista = new List<PlanEntrenamiento>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    string query = @"
                        SELECT
                            idPlan,
                            Nombre,
                            Descripcion,
                            Precio,
                            Duracion,
                            Estado
                        FROM PlanEntrenamiento";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new PlanEntrenamiento()
                            {
                                idPlan = Convert.ToInt32(dr["idPlan"]),
                                Nombre = dr["Nombre"].ToString(),
                                Descripcion = dr["Descripcion"].ToString(),
                                Precio = Convert.ToDecimal(dr["Precio"]),
                                Duracion = Convert.ToInt32(dr["Duracion"]),
                                Estado = Convert.ToBoolean(dr["Estado"])
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al obtener planes: " + ex.Message);
                }
            }

            return lista;
        }


        public PlanEntrenamiento BuscarPorId(int idPlan)
        {
            PlanEntrenamiento obj = null;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    string query = @"
                SELECT
                    idPlan,
                    Nombre,
                    Descripcion,
                    Precio,
                    Duracion,
                    Estado
                FROM PlanEntrenamiento
                WHERE idPlan = @idPlan";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@idPlan", idPlan);

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            obj = new PlanEntrenamiento()
                            {
                                idPlan = Convert.ToInt32(dr["idPlan"]),
                                Nombre = dr["Nombre"].ToString(),
                                Descripcion = dr["Descripcion"].ToString(),
                                Precio = Convert.ToDecimal(dr["Precio"]),
                                Duracion = Convert.ToInt32(dr["Duracion"]),
                                Estado = Convert.ToBoolean(dr["Estado"])
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al buscar plan: " + ex.Message);
                }
            }

            return obj;
        }


        public int Registrar(PlanEntrenamiento obj)
        {
            int idPlanGenerado = 0;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    string query = @"
                INSERT INTO PlanEntrenamiento
                (Nombre, Descripcion, Precio, Duracion)
                VALUES
                (@Nombre, @Descripcion, @Precio, @Duracion);

                SELECT SCOPE_IDENTITY();";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("@Descripcion", obj.Descripcion);
                    cmd.Parameters.AddWithValue("@Precio", obj.Precio);
                    cmd.Parameters.AddWithValue("@Duracion", obj.Duracion);

                    oconexion.Open();

                    idPlanGenerado = Convert.ToInt32(cmd.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al registrar plan: " + ex.Message);
                }
            }

            return idPlanGenerado;
        }

        public bool Editar(PlanEntrenamiento obj)
        {
            bool respuesta = false;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    string query = @"
                UPDATE PlanEntrenamiento
                SET
                    Nombre = @Nombre,
                    Descripcion = @Descripcion,
                    Precio = @Precio,
                    Duracion = @Duracion
                WHERE idPlan = @idPlan";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("@Descripcion", obj.Descripcion);
                    cmd.Parameters.AddWithValue("@Precio", obj.Precio);
                    cmd.Parameters.AddWithValue("@Duracion", obj.Duracion);
                    cmd.Parameters.AddWithValue("@idPlan", obj.idPlan);

                    oconexion.Open();

                    respuesta = cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al editar plan: " + ex.Message);
                }
            }

            return respuesta;
        }

        public bool Eliminar(int idPlan)
        {
            bool respuesta = false;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    string query = @"
                UPDATE PlanEntrenamiento
                SET Estado = 0
                WHERE idPlan = @idPlan";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@idPlan", idPlan);

                    oconexion.Open();

                    respuesta = cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al eliminar plan: " + ex.Message);
                }
            }

            return respuesta;
        }

        public bool Activar(int idPlan)
        {
            bool respuesta = false;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    string query = @"
                UPDATE PlanEntrenamiento
                SET Estado = 1
                WHERE idPlan = @idPlan";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@idPlan", idPlan);

                    oconexion.Open();

                    respuesta = cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al activar plan: " + ex.Message);
                }
            }

            return respuesta;
        }

    }
}