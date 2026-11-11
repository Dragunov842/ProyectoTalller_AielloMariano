using CapaEntidad;
using CapaEntidad.Membresias;
using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace CapaDatos
{
    public class CD_AsignacionMembresia
    {
        // Busca la membresía activa de un alumno determinado.
        // Si encuentra una asignación vigente, devuelve sus datos junto con la información de la membresía.
        // Si no encuentra ninguna, devuelve null.
        public AsignacionMembresia BuscarActiva(int idAlumno)
        {
            AsignacionMembresia obj = null;

            // Se establece la conexión con la base de datos utilizando la cadena configurada.
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    // TOP 1 permite obtener solamente un registro.
                    // Se relacionan las tablas AsignacionMembresia y Membresia para recuperar
                    // tanto los datos de la asignación como los de la membresía correspondiente.
                    // Se filtra por alumno, estado activo y fecha de vencimiento vigente.
                    // ORDER BY permite priorizar la asignación con la fecha de finalización más reciente.
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

                    // Se prepara el comando SQL y se establece el identificador del alumno
                    // como parámetro para filtrar los resultados.
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@idAlumno", idAlumno);

                    oconexion.Open();

                    // SqlDataReader permite recorrer los resultados obtenidos por la consulta.
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        // Si se encuentra una asignación, se construye el objeto correspondiente.
                        if (dr.Read())
                        {
                            obj = new AsignacionMembresia()
                            {
                                // Se recupera el identificador de la asignación.
                                idAsignacionMembresia =
                                    Convert.ToInt32(dr["idAsignacionMembresia"]),

                                // Se crea el objeto Alumno asociado a la asignación.
                                // En esta consulta solamente se recupera su identificador.
                                oAlumno = new Alumno()
                                {
                                    idAlumno = Convert.ToInt32(dr["idAlumno"])
                                },

                                // Se construye el objeto Membresia con los datos
                                // recuperados de la tabla correspondiente.
                                oMembresia = new Membresia()
                                {
                                    idMembresia =
                                        Convert.ToInt32(dr["idMembresia"]),

                                    Nombre = dr["Nombre"].ToString(),
                                    Descripcion = dr["Descripcion"].ToString(),
                                    Precio = Convert.ToDecimal(dr["Precio"]),
                                    Duracion = Convert.ToInt32(dr["Duracion"])
                                },

                                // Se recuperan las fechas de inicio y finalización
                                // junto con el estado de la asignación.
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
                    // Si ocurre un error durante la búsqueda, se informa mediante una excepción.
                    throw new Exception(
                        "Error al buscar membresía activa: " + ex.Message
                    );
                }
            }

            return obj;
        }

        // Registra una nueva asignación de membresía para un alumno.
        // Devuelve el identificador generado para el nuevo registro.
        public int Registrar(AsignacionMembresia obj)
        {
            int idGenerado = 0;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    // Se inserta una nueva asignación relacionando un alumno con una membresía.
                    // También se almacenan las fechas de inicio y finalización y el estado.
                    // SCOPE_IDENTITY permite recuperar el identificador generado por el INSERT.
                    string query = @"
                        INSERT INTO AsignacionMembresia
                        (idAlumno, idMembresia, FechaInicio, FechaFin, Estado)
                        VALUES
                        (@idAlumno, @idMembresia, @FechaInicio, @FechaFin, @Estado);

                        SELECT SCOPE_IDENTITY();";

                    // Se prepara el comando que ejecutará la inserción.
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    // Se asignan los valores del objeto recibido a los parámetros SQL.
                    // Los identificadores se obtienen de los objetos Alumno y Membresia asociados.
                    cmd.Parameters.AddWithValue("@idAlumno", obj.oAlumno.idAlumno);
                    cmd.Parameters.AddWithValue("@idMembresia", obj.oMembresia.idMembresia);
                    cmd.Parameters.AddWithValue("@FechaInicio", obj.FechaInicio);
                    cmd.Parameters.AddWithValue("@FechaFin", obj.FechaFin);
                    cmd.Parameters.AddWithValue("@Estado", obj.Estado);

                    oconexion.Open();

                    // ExecuteScalar obtiene el valor devuelto por SCOPE_IDENTITY,
                    // es decir, el identificador de la asignación recién registrada.
                    idGenerado = Convert.ToInt32(cmd.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    // Se informa cualquier error producido durante el registro.
                    throw new Exception("Error al registrar asignación de membresía: " + ex.Message);
                }
            }

            return idGenerado;
        }

        // Actualiza la fecha de finalización de una asignación de membresía existente.
        // Devuelve true si se modificó al menos un registro y false en caso contrario.
        public bool ActualizarFechaFin(int idAsignacionMembresia, DateTime nuevaFechaFin)
        {
            bool respuesta = false;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    // Se modifica únicamente la fecha de finalización de la asignación
                    // cuyo identificador coincide con el recibido.
                    string query = @"
                        UPDATE AsignacionMembresia
                        SET FechaFin = @FechaFin
                        WHERE idAsignacionMembresia = @idAsignacionMembresia";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    // Se establecen los parámetros correspondientes a la nueva fecha
                    // y al identificador de la asignación que se desea actualizar.
                    cmd.Parameters.AddWithValue("@FechaFin", nuevaFechaFin);
                    cmd.Parameters.AddWithValue("@idAsignacionMembresia", idAsignacionMembresia);

                    oconexion.Open();

                    // ExecuteNonQuery devuelve la cantidad de registros afectados.
                    // Se considera exitosa la operación si se modificó al menos uno.
                    respuesta = cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    // Se informa cualquier error producido durante la actualización.
                    throw new Exception("Error al actualizar fecha de membresía: " + ex.Message);
                }
            }

            return respuesta;
        }
    }
}
