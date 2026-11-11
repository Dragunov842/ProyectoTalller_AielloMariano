
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_PlanEntrenamiento
    {
        // Obtiene todos los planes de entrenamiento registrados en la base de datos.
        public List<PlanEntrenamiento> Listar()
        {
            // Lista donde se almacenarán los planes recuperados.
            List<PlanEntrenamiento> lista = new List<PlanEntrenamiento>();

            // Establece la conexión con la base de datos.
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    // Consulta los datos de todos los planes de entrenamiento.
                    string query = @"
                        SELECT
                            idPlan,
                            Nombre,
                            Descripcion,
                            Precio,
                            Duracion,
                            Estado
                        FROM PlanEntrenamiento";

                    // Prepara el comando SQL.
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    // Abre la conexión.
                    oconexion.Open();

                    // Ejecuta la consulta y permite recorrer los resultados.
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        // Recorre cada registro devuelto por la consulta.
                        while (dr.Read())
                        {
                            // Convierte los datos del registro actual
                            // en un objeto PlanEntrenamiento y lo agrega a la lista.
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
                    // Propaga el error indicando que ocurrió al obtener los planes.
                    throw new Exception("Error al obtener planes: " + ex.Message);
                }
            }

            // Devuelve la lista con los planes encontrados.
            return lista;
        }


        // Busca un plan de entrenamiento utilizando su identificador.
        // Si no encuentra ningún registro, devuelve null.
        public PlanEntrenamiento BuscarPorId(int idPlan)
        {
            // Inicialmente no existe ningún plan encontrado.
            PlanEntrenamiento obj = null;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    // Recupera los datos del plan cuyo ID coincide con el recibido.
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

                    // Asigna el identificador como parámetro de la consulta.
                    cmd.Parameters.AddWithValue("@idPlan", idPlan);

                    oconexion.Open();

                    // Ejecuta la consulta y obtiene el resultado.
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        // Si existe un registro, construye el objeto correspondiente.
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
                    // Propaga el error indicando que ocurrió durante la búsqueda.
                    throw new Exception("Error al buscar plan: " + ex.Message);
                }
            }

            // Devuelve el plan encontrado o null si no existe.
            return obj;
        }


        // Registra un nuevo plan de entrenamiento en la base de datos.
        public int Registrar(PlanEntrenamiento obj)
        {
            // Almacena el identificador generado automáticamente por SQL Server.
            int idPlanGenerado = 0;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    // Inserta los datos del nuevo plan.
                    // SCOPE_IDENTITY() recupera el ID generado por la columna IDENTITY.
                    string query = @"
                INSERT INTO PlanEntrenamiento
                (Nombre, Descripcion, Precio, Duracion)
                VALUES
                (@Nombre, @Descripcion, @Precio, @Duracion);

                SELECT SCOPE_IDENTITY();";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    // Asigna los valores del objeto a los parámetros de la consulta.
                    cmd.Parameters.AddWithValue("@Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("@Descripcion", obj.Descripcion);
                    cmd.Parameters.AddWithValue("@Precio", obj.Precio);
                    cmd.Parameters.AddWithValue("@Duracion", obj.Duracion);

                    oconexion.Open();

                    // Ejecuta la inserción y recupera el ID del nuevo plan.
                    idPlanGenerado = Convert.ToInt32(cmd.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    // Propaga el error indicando que ocurrió durante el registro.
                    throw new Exception("Error al registrar plan: " + ex.Message);
                }
            }

            // Devuelve el identificador del plan registrado.
            return idPlanGenerado;
        }

        // Modifica los datos de un plan de entrenamiento existente.
        public bool Editar(PlanEntrenamiento obj)
        {
            // Indica inicialmente que la operación no se realizó correctamente.
            bool respuesta = false;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    // Actualiza los datos del plan identificado por su ID.
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

                    // Asigna los nuevos valores que reemplazarán a los existentes.
                    cmd.Parameters.AddWithValue("@Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("@Descripcion", obj.Descripcion);
                    cmd.Parameters.AddWithValue("@Precio", obj.Precio);
                    cmd.Parameters.AddWithValue("@Duracion", obj.Duracion);
                    cmd.Parameters.AddWithValue("@idPlan", obj.idPlan);

                    oconexion.Open();

                    // ExecuteNonQuery devuelve la cantidad de filas afectadas.
                    // Si se modificó al menos una fila, la operación fue exitosa.
                    respuesta = cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    // Propaga el error indicando que ocurrió durante la edición.
                    throw new Exception("Error al editar plan: " + ex.Message);
                }
            }

            // Devuelve true si se modificó algún registro.
            return respuesta;
        }

        // Realiza una eliminación lógica del plan de entrenamiento.
        // No elimina físicamente el registro de la base de datos:
        // simplemente cambia su estado a inactivo.
        public bool Eliminar(int idPlan)
        {
            bool respuesta = false;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    // Desactiva el plan cuyo identificador coincide con el recibido.
                    string query = @"
                UPDATE PlanEntrenamiento
                SET Estado = 0
                WHERE idPlan = @idPlan";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    // Identifica el plan que se desea desactivar.
                    cmd.Parameters.AddWithValue("@idPlan", idPlan);

                    oconexion.Open();

                    // Comprueba si se modificó al menos un registro.
                    respuesta = cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    // Propaga el error indicando que ocurrió durante la eliminación.
                    throw new Exception("Error al eliminar plan: " + ex.Message);
                }
            }

            return respuesta;
        }

        // Reactiva un plan de entrenamiento que se encontraba inactivo.
        public bool Activar(int idPlan)
        {
            bool respuesta = false;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    // Cambia el estado del plan a activo.
                    string query = @"
                UPDATE PlanEntrenamiento
                SET Estado = 1
                WHERE idPlan = @idPlan";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    // Identifica el plan que se desea activar.
                    cmd.Parameters.AddWithValue("@idPlan", idPlan);

                    oconexion.Open();

                    // Comprueba si se modificó al menos un registro.
                    respuesta = cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    // Propaga el error indicando que ocurrió durante la activación.
                    throw new Exception("Error al activar plan: " + ex.Message);
                }
            }

            return respuesta;
        }

    }
}
