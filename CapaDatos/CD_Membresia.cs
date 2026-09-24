using CapaEntidad;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CapaDatos
{
    public class CD_Membresia
    {

        public List<Membresia> Listar()
        {
            List<Membresia> lista = new List<Membresia>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    string query = @"
                        SELECT
                            idMembresia,
                            Nombre,
                            Descripcion,
                            Precio,
                            Duracion,
                            Estado
                        FROM Membresia";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Membresia()
                            {
                                idMembresia = Convert.ToInt32(dr["idMembresia"]),
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
                    throw new Exception("Error al obtener membresías" + ex.Message);
                }
            }

            return lista;
        }

        public Membresia BuscarPorId(int idMembresia)
        {
            Membresia obj = null;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    string query = @"
                SELECT
                    idMembresia,
                    Nombre,
                    Descripcion,
                    Precio,
                    Duracion,
                    Estado
                FROM Membresia
                WHERE idMembresia = @idMembresia";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@idMembresia", idMembresia);

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            obj = new Membresia()
                            {
                                idMembresia = Convert.ToInt32(dr["idMembresia"]),
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
                    throw new Exception("Error al buscar membresía: " + ex.Message);
                }
            }

            return obj;
        }

        public int Registrar(Membresia obj)
        {
            int idMembresiaGenerada = 0;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    string query = @"
                INSERT INTO Membresia
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

                    idMembresiaGenerada = Convert.ToInt32(cmd.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al registrar membresía: " + ex.Message);
                }
            }

            return idMembresiaGenerada;
        }

        public bool Editar(Membresia obj)
        {
            bool respuesta = false;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    string query = @"
                UPDATE Membresia
                SET
                    Nombre = @Nombre,
                    Descripcion = @Descripcion,
                    Precio = @Precio,
                    Duracion = @Duracion
                WHERE idMembresia = @idMembresia";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("@Descripcion", obj.Descripcion);
                    cmd.Parameters.AddWithValue("@Precio", obj.Precio);
                    cmd.Parameters.AddWithValue("@Duracion", obj.Duracion);
                    cmd.Parameters.AddWithValue("@idMembresia", obj.idMembresia);

                    oconexion.Open();

                    respuesta = cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al editar membresía: " + ex.Message);
                }
            }

            return respuesta;
        }

        public bool Eliminar(int idMembresia)
        {
            bool respuesta = false;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    string query = @"
                UPDATE Membresia
                SET Estado = 0
                WHERE idMembresia = @idMembresia";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@idMembresia", idMembresia);

                    oconexion.Open();

                    respuesta = cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al eliminar membresía: " + ex.Message);
                }
            }

            return respuesta;
        }

        public bool Activar(int idMembresia)
        {
            bool respuesta = false;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    string query = @"
                UPDATE Membresia
                SET Estado = 1
                WHERE idMembresia = @idMembresia";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@idMembresia", idMembresia);

                    oconexion.Open();

                    respuesta = cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al activar membresía: " + ex.Message);
                }
            }

            return respuesta;
        }

    }
}   