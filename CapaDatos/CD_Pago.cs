
using CapaEntidad;
using CapaEntidad.Membresias;
using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace CapaDatos
{
    public class CD_Pago
    {
        // Registra un pago utilizando las asignaciones que ya existen.
        // Las asignaciones de plan y membresía son opcionales:
        // un pago puede estar relacionado con una u otra.
        public int Registrar(Pago obj)
        {
            // Almacena el identificador generado por SQL Server.
            int idGenerado = 0;

            // Crea y administra la conexión con la base de datos.
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    // Inserta el pago y obtiene el ID generado automáticamente.
                    string query = @"
                        INSERT INTO Pago
                        (idAsignacionPlan, idAsignacionMembresia, FechaPago, Importe)
                        VALUES
                        (@idAsignacionPlan, @idAsignacionMembresia, @FechaPago, @Importe);

                        SELECT SCOPE_IDENTITY();";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    // Si el pago está asociado a un plan, se envía su identificador.
                    // En caso contrario, se almacena NULL en la columna correspondiente.
                    if (obj.oAsignacionPlan != null)
                        cmd.Parameters.AddWithValue("@idAsignacionPlan", obj.oAsignacionPlan.idAsignacion);
                    else
                        cmd.Parameters.AddWithValue("@idAsignacionPlan", DBNull.Value);

                    // Si el pago está asociado a una membresía, se envía su identificador.
                    // Si no existe una asignación de membresía, se envía NULL.
                    if (obj.oAsignacionMembresia != null)
                        cmd.Parameters.AddWithValue("@idAsignacionMembresia", obj.oAsignacionMembresia.idAsignacionMembresia);
                    else
                        cmd.Parameters.AddWithValue("@idAsignacionMembresia", DBNull.Value);

                    // Asigna los datos propios del pago.
                    cmd.Parameters.AddWithValue("@FechaPago", obj.FechaPago);
                    cmd.Parameters.AddWithValue("@Importe", obj.Importe);

                    // Abre la conexión antes de ejecutar la consulta.
                    oconexion.Open();

                    // ExecuteScalar obtiene el valor de la primera columna
                    // de la primera fila: en este caso, el ID del pago generado.
                    idGenerado = Convert.ToInt32(cmd.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    // Propaga el error agregando información sobre la operación.
                    throw new Exception("Error al registrar pago: " + ex.Message);
                }
            }

            // Devuelve el identificador del pago registrado.
            return idGenerado;
        }

        // Registra una operación de pago que puede incluir:
        // - La creación de una asignación de plan.
        // - La creación de una asignación de membresía.
        // - La renovación de una membresía existente.
        //
        // Todas las operaciones se ejecutan dentro de una única transacción
        // para evitar que queden registros incompletos si ocurre un error.
        public int RegistrarPagoConAsignacion(
            Pago pago,
            AsignacionPlan asignacionPlan,
            AsignacionMembresia asignacionMembresia,
            bool renovarMembresia)
        {
            // Almacena el identificador del pago que se registrará al finalizar.
            int idPago = 0;

            // Crea la conexión con la base de datos.
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                // Representa la transacción que agrupa todas las operaciones SQL.
                SqlTransaction transaccion = null;

                try
                {
                    // Abre la conexión para comenzar las operaciones.
                    oconexion.Open();

                    // Inicia la transacción.
                    // Los cambios se confirmarán todos juntos mediante Commit().
                    transaccion = oconexion.BeginTransaction();

                    // Estos identificadores almacenarán las asignaciones
                    // que se creen o utilicen durante la operación.
                    int idAsignacionPlan = 0;
                    int idAsignacionMembresia = 0;

                    // Si se recibió una asignación de plan, se registra en la base de datos.
                    if (asignacionPlan != null)
                    {
                        // Inserta una nueva asignación de plan.
                        // SCOPE_IDENTITY() permite recuperar su identificador generado.
                        string queryPlan = @"
                            INSERT INTO AsignacionPlan
                            (idAlumno, idPlan, FechaInicio, FechaFin, Estado)
                            VALUES
                            (@idAlumno, @idPlan, @FechaInicio, @FechaFin, @Estado);

                            SELECT SCOPE_IDENTITY();";

                        // El comando utiliza la misma conexión y transacción.
                        SqlCommand cmdPlan = new SqlCommand(
                            queryPlan,
                            oconexion,
                            transaccion
                        );

                        // Asocia la asignación con el alumno correspondiente.
                        cmdPlan.Parameters.AddWithValue(
                            "@idAlumno",
                            asignacionPlan.oAlumno.idAlumno
                        );

                        // Indica qué plan se está asignando.
                        cmdPlan.Parameters.AddWithValue(
                            "@idPlan",
                            asignacionPlan.oPlan.idPlan
                        );

                        // Establece las fechas de inicio y finalización.
                        cmdPlan.Parameters.AddWithValue(
                            "@FechaInicio",
                            asignacionPlan.FechaInicio
                        );

                        cmdPlan.Parameters.AddWithValue(
                            "@FechaFin",
                            asignacionPlan.FechaFin
                        );

                        // Establece el estado de la asignación.
                        cmdPlan.Parameters.AddWithValue(
                            "@Estado",
                            asignacionPlan.Estado
                        );

                        // Ejecuta la inserción y recupera el ID generado.
                        idAsignacionPlan = Convert.ToInt32(
                            cmdPlan.ExecuteScalar()
                        );
                    }

                    // Si existe una asignación de membresía y no se trata
                    // de una renovación, se registra una nueva asignación.
                    if (asignacionMembresia != null && !renovarMembresia)
                    {
                        // Inserta una nueva asignación de membresía.
                        string queryMembresia = @"
                            INSERT INTO AsignacionMembresia
                            (idAlumno, idMembresia, FechaInicio, FechaFin, Estado)
                            VALUES
                            (@idAlumno, @idMembresia, @FechaInicio, @FechaFin, @Estado);

                            SELECT SCOPE_IDENTITY();";

                        // El comando también forma parte de la transacción actual.
                        SqlCommand cmdMembresia = new SqlCommand(
                            queryMembresia,
                            oconexion,
                            transaccion
                        );

                        // Identifica al alumno que recibe la membresía.
                        cmdMembresia.Parameters.AddWithValue(
                            "@idAlumno",
                            asignacionMembresia.oAlumno.idAlumno
                        );

                        // Identifica el tipo de membresía asignada.
                        cmdMembresia.Parameters.AddWithValue(
                            "@idMembresia",
                            asignacionMembresia.oMembresia.idMembresia
                        );

                        // Define el período de vigencia de la membresía.
                        cmdMembresia.Parameters.AddWithValue(
                            "@FechaInicio",
                            asignacionMembresia.FechaInicio
                        );

                        cmdMembresia.Parameters.AddWithValue(
                            "@FechaFin",
                            asignacionMembresia.FechaFin
                        );

                        // Establece el estado de la asignación.
                        cmdMembresia.Parameters.AddWithValue(
                            "@Estado",
                            asignacionMembresia.Estado
                        );

                        // Recupera el identificador de la nueva asignación.
                        idAsignacionMembresia = Convert.ToInt32(
                            cmdMembresia.ExecuteScalar()
                        );
                    }

                    // Si se recibió una membresía y se indicó que debe renovarse,
                    // se actualiza la fecha de finalización de la asignación existente.
                    if (asignacionMembresia != null && renovarMembresia)
                    {
                        // Actualiza la fecha de finalización de la membresía.
                        string queryRenovacion = @"
                            UPDATE AsignacionMembresia
                            SET FechaFin = @FechaFin
                            WHERE idAsignacionMembresia = @idAsignacionMembresia";

                        // Ejecuta la actualización dentro de la transacción.
                        SqlCommand cmdRenovacion = new SqlCommand(
                            queryRenovacion,
                            oconexion,
                            transaccion
                        );

                        // Establece la nueva fecha de finalización.
                        cmdRenovacion.Parameters.AddWithValue(
                            "@FechaFin",
                            asignacionMembresia.FechaFin
                        );

                        // Identifica la asignación de membresía que se renovará.
                        cmdRenovacion.Parameters.AddWithValue(
                            "@idAsignacionMembresia",
                            asignacionMembresia.idAsignacionMembresia
                        );

                        // Ejecuta la actualización.
                        cmdRenovacion.ExecuteNonQuery();

                        // Conserva el ID de la asignación existente,
                        // ya que no se creó un nuevo registro.
                        idAsignacionMembresia =
                            asignacionMembresia.idAsignacionMembresia;
                    }

                    // Una vez procesadas las asignaciones, se registra el pago.
                    // Se utilizan los identificadores obtenidos anteriormente.
                    string queryPago = @"
                        INSERT INTO Pago
                        (idAsignacionPlan, idAsignacionMembresia, FechaPago, Importe)
                        VALUES
                        (@idAsignacionPlan, @idAsignacionMembresia, @FechaPago, @Importe);

                        SELECT SCOPE_IDENTITY();";

                    // El pago se registra dentro de la misma transacción.
                    SqlCommand cmdPago = new SqlCommand(
                        queryPago,
                        oconexion,
                        transaccion
                    );

                    // Si se creó o recuperó una asignación de plan,
                    // se utiliza su ID. Si no, se envía NULL.
                    if (idAsignacionPlan > 0)
                        cmdPago.Parameters.AddWithValue(
                            "@idAsignacionPlan",
                            idAsignacionPlan
                        );
                    else
                        cmdPago.Parameters.AddWithValue(
                            "@idAsignacionPlan",
                            DBNull.Value
                        );

                    // Si se creó o recuperó una asignación de membresía,
                    // se utiliza su ID. Si no, se envía NULL.
                    if (idAsignacionMembresia > 0)
                        cmdPago.Parameters.AddWithValue(
                            "@idAsignacionMembresia",
                            idAsignacionMembresia
                        );
                    else
                        cmdPago.Parameters.AddWithValue(
                            "@idAsignacionMembresia",
                            DBNull.Value
                        );

                    // Asigna la fecha y el importe correspondientes al pago.
                    cmdPago.Parameters.AddWithValue(
                        "@FechaPago",
                        pago.FechaPago
                    );

                    cmdPago.Parameters.AddWithValue(
                        "@Importe",
                        pago.Importe
                    );

                    // Ejecuta la inserción y obtiene el ID del pago generado.
                    idPago = Convert.ToInt32(
                        cmdPago.ExecuteScalar()
                    );

                    // Confirma todas las operaciones realizadas.
                    // Si se llegó hasta aquí, los cambios quedan guardados.
                    transaccion.Commit();
                }
                catch (Exception ex)
                {
                    // Si ocurre un error, intenta deshacer todos los cambios.
                    // El operador ?. evita llamar a Rollback si la transacción
                    // todavía no llegó a inicializarse.
                    try
                    {
                        transaccion?.Rollback();
                    }
                    catch
                    {
                        // Evita que un error durante el Rollback
                        // oculte la excepción original.
                    }

                    // Propaga el error indicando que ocurrió durante
                    // el registro de la operación completa de pago.
                    throw new Exception(
                        "Error al registrar la operación de pago: " + ex.Message
                    );
                }
            }

            // Devuelve el ID del pago registrado.
            return idPago;
        }
    }
}
