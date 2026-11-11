
using CapaEntidad;
using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace CapaDatos
{
    public class CD_AsignacionPlan
    {

        // Registra la asignación de un plan de entrenamiento a un alumno.
        // Devuelve el identificador generado por la base de datos.
        public int Registrar(AsignacionPlan obj)
        {
            int idGenerado = 0;

            // Se establece la conexión con la base de datos.
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    // Consulta SQL para insertar una nueva asignación.
                    // SCOPE_IDENTITY() permite recuperar el ID generado
                    // automáticamente para el registro recién insertado.
                    string query = @"
                        INSERT INTO AsignacionPlan
                        (idAlumno, idPlan, FechaInicio, FechaFin, Estado)
                        VALUES
                        (@idAlumno, @idPlan, @FechaInicio, @FechaFin, @Estado);

                        SELECT SCOPE_IDENTITY();";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    // Se asignan los valores recibidos desde el objeto
                    // a los parámetros de la consulta SQL.
                    cmd.Parameters.AddWithValue("@idAlumno", obj.oAlumno.idAlumno);
                    cmd.Parameters.AddWithValue("@idPlan", obj.oPlan.idPlan);
                    cmd.Parameters.AddWithValue("@FechaInicio", obj.FechaInicio);
                    cmd.Parameters.AddWithValue("@FechaFin", obj.FechaFin);
                    cmd.Parameters.AddWithValue("@Estado", obj.Estado);

                    // Se abre la conexión antes de ejecutar la consulta.
                    oconexion.Open();

                    // ExecuteScalar obtiene el primer valor de la primera fila:
                    // en este caso, el identificador generado por SCOPE_IDENTITY().
                    idGenerado = Convert.ToInt32(cmd.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    // Se propaga el error agregando información sobre
                    // la operación que estaba intentando realizarse.
                    throw new Exception("Error al registrar asignación de plan: " + ex.Message);
                }
            }

            return idGenerado;
        }

        // Cuenta cuántas asignaciones de planes vigentes tiene un alumno.
        // Considera únicamente las asignaciones con Estado = 1
        // cuya fecha de finalización no haya pasado.
        public int ContarActivos(int idAlumno)
        {
            int cantidad = 0;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    // COUNT(*) devuelve la cantidad de registros que cumplen
                    // las condiciones indicadas.
                    // GETDATE() obtiene la fecha y hora actuales del servidor.
                    // CAST(... AS date) elimina la parte correspondiente a la hora,
                    // permitiendo comparar únicamente las fechas.
                    string query = @"
                        SELECT COUNT(*)
                        FROM AsignacionPlan
                        WHERE idAlumno = @idAlumno
                          AND Estado = 1
                          AND CAST(GETDATE() AS date) <= FechaFin";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    // Se filtran las asignaciones correspondientes al alumno recibido.
                    cmd.Parameters.AddWithValue("@idAlumno", idAlumno);

                    oconexion.Open();

                    // ExecuteScalar obtiene el resultado de COUNT(*),
                    // que luego se convierte a un número entero.
                    cantidad = Convert.ToInt32(cmd.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al contar planes activos: " + ex.Message);
                }
            }

            return cantidad;
        }

        // Obtiene todas las asignaciones de planes vigentes de un alumno.
        // Devuelve una lista de objetos AsignacionPlan con sus respectivos
        // datos del alumno y del plan de entrenamiento.
        public List<AsignacionPlan> ListarActivosPorAlumno(int idAlumno)
        {
            List<AsignacionPlan> lista = new List<AsignacionPlan>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    // Se seleccionan los datos de la asignación y del plan.
                    // INNER JOIN permite relacionar ambas tablas mediante idPlan,
                    // obteniendo también el nombre, descripción, precio y duración.
                    // Se filtran las asignaciones vigentes del alumno.
                    // ORDER BY organiza los resultados por fecha de finalización,
                    // mostrando primero los planes que vencen antes.
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

                    // Se establece el alumno cuyos planes se desean consultar.
                    cmd.Parameters.AddWithValue("@idAlumno", idAlumno);

                    oconexion.Open();

                    // ExecuteReader permite recorrer los registros devueltos
                    // por la consulta SQL.
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        // Se procesa cada fila obtenida de la base de datos.
                        while (dr.Read())
                        {
                            // Se construye un objeto AsignacionPlan con los datos
                            // recuperados, incluyendo objetos relacionados.
                            lista.Add(new AsignacionPlan()
                            {
                                idAsignacion = Convert.ToInt32(dr["idAsignacion"]),

                                // Se crea el objeto Alumno asociado a la asignación.
                                oAlumno = new Alumno()
                                {
                                    idAlumno = Convert.ToInt32(dr["idAlumno"])
                                },

                                // Se crea el objeto PlanEntrenamiento con
                                // los datos completos del plan asignado.
                                oPlan = new PlanEntrenamiento()
                                {
                                    idPlan = Convert.ToInt32(dr["idPlan"]),
                                    Nombre = dr["Nombre"].ToString(),
                                    Descripcion = dr["Descripcion"].ToString(),
                                    Precio = Convert.ToDecimal(dr["Precio"]),
                                    Duracion = Convert.ToInt32(dr["Duracion"])
                                },

                                // Se recuperan las fechas y el estado
                                // correspondiente a la asignación.
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

            // Se devuelve la lista con todas las asignaciones encontradas.
            return lista;
        }


        // Verifica si un alumno ya tiene asignado un determinado plan vigente.
        // Devuelve true si encuentra al menos una asignación que cumpla
        // las condiciones, o false si no existe ninguna.
        public bool YaTienePlanActivo(int idAlumno, int idPlan)
        {
            bool existe = false;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    // Se buscan asignaciones que coincidan con el alumno y el plan.
                    // También se verifica que estén activas y que no hayan vencido.
                    string query = @"
                        SELECT COUNT(*)
                        FROM AsignacionPlan
                        WHERE idAlumno = @idAlumno
                          AND idPlan = @idPlan
                          AND Estado = 1
                          AND CAST(GETDATE() AS date) <= FechaFin";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    // Se asignan ambos identificadores para realizar la búsqueda.
                    cmd.Parameters.AddWithValue("@idAlumno", idAlumno);
                    cmd.Parameters.AddWithValue("@idPlan", idPlan);

                    oconexion.Open();

                    // Si COUNT(*) devuelve un valor mayor que cero,
                    // significa que existe al menos una asignación vigente.
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
