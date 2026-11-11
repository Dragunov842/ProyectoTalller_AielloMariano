
using CapaEntidad;
using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace CapaDatos
{
    public class CD_Asistencia
    {
        // Registra una nueva asistencia para un alumno.
        // Devuelve el identificador generado automáticamente por la base de datos.
        public int Registrar(Asistencia obj)
        {
            int idGenerado = 0;

            // Se establece la conexión con la base de datos utilizando
            // la cadena de conexión definida en la clase Conexion.
            using (SqlConnection oconexion =
                   new SqlConnection(Conexion.cadena))
            {
                try
                {
                    // Consulta SQL que inserta una nueva asistencia,
                    // almacenando el identificador del alumno y la fecha de ingreso.
                    // SCOPE_IDENTITY() permite recuperar el ID generado
                    // para el registro recién insertado.
                    string query = @"
                        INSERT INTO Asistencia
                        (idAlumno, Ingreso)
                        VALUES
                        (@idAlumno, @Ingreso);

                        SELECT SCOPE_IDENTITY();";

                    SqlCommand cmd =
                        new SqlCommand(query, oconexion);

                    cmd.CommandType = CommandType.Text;

                    // Se asigna el identificador del alumno asociado
                    // a la asistencia que se desea registrar.
                    cmd.Parameters.AddWithValue(
                        "@idAlumno",
                        obj.oAlumno.idAlumno
                    );

                    // Se establece la fecha y hora de ingreso.
                    cmd.Parameters.AddWithValue(
                        "@Ingreso",
                        obj.Ingreso
                    );

                    // Se abre la conexión para ejecutar la consulta.
                    oconexion.Open();

                    // ExecuteScalar recupera el identificador generado
                    // por SQL Server y lo convierte a entero.
                    idGenerado =
                        Convert.ToInt32(cmd.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    // Se informa que ocurrió un error durante el registro
                    // y se conserva el mensaje de la excepción original.
                    throw new Exception(
                        "Error al registrar asistencia: " +
                        ex.Message
                    );
                }
            }

            return idGenerado;
        }


        // Comprueba si un alumno ya tiene registrada una asistencia
        // correspondiente al día actual.
        // Devuelve true si encuentra al menos una asistencia y false si no.
        public bool YaAsistioHoy(int idAlumno)
        {
            bool respuesta = false;

            // Se establece la conexión con la base de datos.
            using (SqlConnection oconexion =
                   new SqlConnection(Conexion.cadena))
            {
                try
                {
                    // COUNT(*) cuenta las asistencias que cumplen
                    // las condiciones indicadas.
                    // Se filtra por el identificador del alumno.
                    // CAST(... AS date) elimina la parte horaria de Ingreso
                    // y GETDATE(), permitiendo comparar solamente las fechas.
                    // De esta manera se verifica si existe una asistencia
                    // registrada durante el día actual.
                    string query = @"
                SELECT COUNT(*)
                FROM Asistencia
                WHERE idAlumno = @idAlumno
                  AND CAST(Ingreso AS date) = CAST(GETDATE() AS date)";

                    SqlCommand cmd =
                        new SqlCommand(query, oconexion);

                    cmd.CommandType = CommandType.Text;

                    // Se establece el identificador del alumno
                    // cuya asistencia se desea verificar.
                    cmd.Parameters.AddWithValue(
                        "@idAlumno",
                        idAlumno
                    );

                    oconexion.Open();

                    // Si COUNT(*) devuelve un valor mayor que cero,
                    // significa que el alumno ya tiene una asistencia
                    // registrada para la fecha actual.
                    respuesta =
                        Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                }
                catch (Exception ex)
                {
                    // Se propaga el error indicando que ocurrió
                    // durante la verificación de asistencia.
                    throw new Exception(
                        "Error al verificar asistencia: " +
                        ex.Message
                    );
                }
            }

            return respuesta;
        }


    }
}
