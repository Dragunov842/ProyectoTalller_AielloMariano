
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

        // Obtiene todas las membresías registradas en la base de datos.
        // Devuelve una lista de objetos Membresia con sus respectivos datos.
        public List<Membresia> Listar()
        {
            List<Membresia> lista = new List<Membresia>();

            // Se establece la conexión con la base de datos.
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    // Se consultan los datos principales de todas las membresías.
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

                    // ExecuteReader permite recorrer los registros
                    // devueltos por la consulta SQL.
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            // Se crea un objeto Membresia por cada registro
                            // y se cargan sus propiedades con los datos obtenidos.
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
                    // Se informa que ocurrió un error durante la consulta.
                    throw new Exception("Error al obtener membresías" + ex.Message);
                }
            }

            return lista;
        }

        // Busca una membresía específica mediante su identificador.
        // Devuelve un objeto Membresia si encuentra el registro
        // o null si no existe una membresía con ese ID.
        public Membresia BuscarPorId(int idMembresia)
        {
            Membresia obj = null;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    // Se consultan los datos de la membresía cuyo identificador
                    // coincide con el valor recibido como parámetro.
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

                    // Se establece el identificador utilizado para realizar la búsqueda.
                    cmd.Parameters.AddWithValue("@idMembresia", idMembresia);

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        // Se comprueba si la consulta devolvió un registro.
                        if (dr.Read())
                        {
                            // Se construye el objeto con los datos recuperados.
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

        // Registra una nueva membresía en la base de datos.
        // Recibe un objeto Membresia con los datos que se desean almacenar.
        // Devuelve el identificador generado automáticamente.
        public int Registrar(Membresia obj)
        {
            int idMembresiaGenerada = 0;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    // Se insertan los datos de la nueva membresía.
                    // SCOPE_IDENTITY() permite recuperar el identificador
                    // generado automáticamente por SQL Server.
                    string query = @"
                INSERT INTO Membresia
                (Nombre, Descripcion, Precio, Duracion)
                VALUES
                (@Nombre, @Descripcion, @Precio, @Duracion);

                SELECT SCOPE_IDENTITY();";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    // Se asignan los valores del objeto a los parámetros SQL.
                    cmd.Parameters.AddWithValue("@Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("@Descripcion", obj.Descripcion);
                    cmd.Parameters.AddWithValue("@Precio", obj.Precio);
                    cmd.Parameters.AddWithValue("@Duracion", obj.Duracion);

                    oconexion.Open();

                    // ExecuteScalar obtiene el identificador generado
                    // y se convierte al tipo entero.
                    idMembresiaGenerada = Convert.ToInt32(cmd.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al registrar membresía: " + ex.Message);
                }
            }

            return idMembresiaGenerada;
        }

        // Modifica los datos de una membresía existente.
        // Recibe el objeto con el identificador y los nuevos valores.
        // Devuelve true si se modificó al menos un registro.
        public bool Editar(Membresia obj)
        {
            bool respuesta = false;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    // Se actualizan los datos de la membresía
                    // identificada mediante su ID.
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

                    // Se asignan los nuevos valores que se desean guardar.
                    cmd.Parameters.AddWithValue("@Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("@Descripcion", obj.Descripcion);
                    cmd.Parameters.AddWithValue("@Precio", obj.Precio);
                    cmd.Parameters.AddWithValue("@Duracion", obj.Duracion);

                    // Este parámetro identifica qué membresía debe modificarse.
                    cmd.Parameters.AddWithValue("@idMembresia", obj.idMembresia);

                    oconexion.Open();

                    // ExecuteNonQuery devuelve la cantidad de filas afectadas.
                    // Si es mayor que cero, significa que se realizó una modificación.
                    respuesta = cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al editar membresía: " + ex.Message);
                }
            }

            return respuesta;
        }

        // Realiza una baja lógica de una membresía.
        // No elimina físicamente el registro, sino que cambia su estado a inactivo.
        public bool Eliminar(int idMembresia)
        {
            bool respuesta = false;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    // Se establece Estado = 0 para indicar que la membresía
                    // ya no se encuentra disponible.
                    string query = @"
                UPDATE Membresia
                SET Estado = 0
                WHERE idMembresia = @idMembresia";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    // Se identifica la membresía que se desea desactivar.
                    cmd.Parameters.AddWithValue("@idMembresia", idMembresia);

                    oconexion.Open();

                    // Se comprueba si la actualización afectó algún registro.
                    respuesta = cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al eliminar membresía: " + ex.Message);
                }
            }

            return respuesta;
        }

        // Reactiva una membresía que se encontraba inactiva.
        // Cambia su estado a uno sin necesidad de crear un registro nuevo.
        public bool Activar(int idMembresia)
        {
            bool respuesta = false;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    // Se establece Estado = 1 para indicar que la membresía
                    // vuelve a estar disponible.
                    string query = @"
                UPDATE Membresia
                SET Estado = 1
                WHERE idMembresia = @idMembresia";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    // Se identifica la membresía que se desea reactivar.
                    cmd.Parameters.AddWithValue("@idMembresia", idMembresia);

                    oconexion.Open();

                    // Se comprueba si la actualización afectó algún registro.
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
