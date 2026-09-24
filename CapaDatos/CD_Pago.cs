using CapaEntidad;
using CapaEntidad.Membresias;
using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace CapaDatos
{
    public class CD_Pago
    {
        public int Registrar(Pago obj)
        {
            int idGenerado = 0;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    string query = @"
                        INSERT INTO Pago
                        (idAsignacionPlan, idAsignacionMembresia, FechaPago, Importe)
                        VALUES
                        (@idAsignacionPlan, @idAsignacionMembresia, @FechaPago, @Importe);

                        SELECT SCOPE_IDENTITY();";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    if (obj.oAsignacionPlan != null)
                        cmd.Parameters.AddWithValue("@idAsignacionPlan", obj.oAsignacionPlan.idAsignacion);
                    else
                        cmd.Parameters.AddWithValue("@idAsignacionPlan", DBNull.Value);

                    if (obj.oAsignacionMembresia != null)
                        cmd.Parameters.AddWithValue("@idAsignacionMembresia", obj.oAsignacionMembresia.idAsignacionMembresia);
                    else
                        cmd.Parameters.AddWithValue("@idAsignacionMembresia", DBNull.Value);

                    cmd.Parameters.AddWithValue("@FechaPago", obj.FechaPago);
                    cmd.Parameters.AddWithValue("@Importe", obj.Importe);

                    oconexion.Open();

                    idGenerado = Convert.ToInt32(cmd.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al registrar pago: " + ex.Message);
                }
            }

            return idGenerado;
        }

        public int RegistrarPagoConAsignacion(
            Pago pago,
            AsignacionPlan asignacionPlan,
            AsignacionMembresia asignacionMembresia,
            bool renovarMembresia)
        {
            int idPago = 0;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                SqlTransaction transaccion = null;

                try
                {
                    oconexion.Open();

                    transaccion = oconexion.BeginTransaction();

                    int idAsignacionPlan = 0;
                    int idAsignacionMembresia = 0;

                   
                    if (asignacionPlan != null)
                    {
                        string queryPlan = @"
                            INSERT INTO AsignacionPlan
                            (idAlumno, idPlan, FechaInicio, FechaFin, Estado)
                            VALUES
                            (@idAlumno, @idPlan, @FechaInicio, @FechaFin, @Estado);

                            SELECT SCOPE_IDENTITY();";

                        SqlCommand cmdPlan = new SqlCommand(
                            queryPlan,
                            oconexion,
                            transaccion
                        );

                        cmdPlan.Parameters.AddWithValue(
                            "@idAlumno",
                            asignacionPlan.oAlumno.idAlumno
                        );

                        cmdPlan.Parameters.AddWithValue(
                            "@idPlan",
                            asignacionPlan.oPlan.idPlan
                        );

                        cmdPlan.Parameters.AddWithValue(
                            "@FechaInicio",
                            asignacionPlan.FechaInicio
                        );

                        cmdPlan.Parameters.AddWithValue(
                            "@FechaFin",
                            asignacionPlan.FechaFin
                        );

                        cmdPlan.Parameters.AddWithValue(
                            "@Estado",
                            asignacionPlan.Estado
                        );

                        idAsignacionPlan = Convert.ToInt32(
                            cmdPlan.ExecuteScalar()
                        );
                    }

                    
                    if (asignacionMembresia != null && !renovarMembresia)
                    {
                        string queryMembresia = @"
                            INSERT INTO AsignacionMembresia
                            (idAlumno, idMembresia, FechaInicio, FechaFin, Estado)
                            VALUES
                            (@idAlumno, @idMembresia, @FechaInicio, @FechaFin, @Estado);

                            SELECT SCOPE_IDENTITY();";

                        SqlCommand cmdMembresia = new SqlCommand(
                            queryMembresia,
                            oconexion,
                            transaccion
                        );

                        cmdMembresia.Parameters.AddWithValue(
                            "@idAlumno",
                            asignacionMembresia.oAlumno.idAlumno
                        );

                        cmdMembresia.Parameters.AddWithValue(
                            "@idMembresia",
                            asignacionMembresia.oMembresia.idMembresia
                        );

                        cmdMembresia.Parameters.AddWithValue(
                            "@FechaInicio",
                            asignacionMembresia.FechaInicio
                        );

                        cmdMembresia.Parameters.AddWithValue(
                            "@FechaFin",
                            asignacionMembresia.FechaFin
                        );

                        cmdMembresia.Parameters.AddWithValue(
                            "@Estado",
                            asignacionMembresia.Estado
                        );

                        idAsignacionMembresia = Convert.ToInt32(
                            cmdMembresia.ExecuteScalar()
                        );
                    }

                    
                    if (asignacionMembresia != null && renovarMembresia)
                    {
                        string queryRenovacion = @"
                            UPDATE AsignacionMembresia
                            SET FechaFin = @FechaFin
                            WHERE idAsignacionMembresia = @idAsignacionMembresia";

                        SqlCommand cmdRenovacion = new SqlCommand(
                            queryRenovacion,
                            oconexion,
                            transaccion
                        );

                        cmdRenovacion.Parameters.AddWithValue(
                            "@FechaFin",
                            asignacionMembresia.FechaFin
                        );

                        cmdRenovacion.Parameters.AddWithValue(
                            "@idAsignacionMembresia",
                            asignacionMembresia.idAsignacionMembresia
                        );

                        cmdRenovacion.ExecuteNonQuery();

                        idAsignacionMembresia =
                            asignacionMembresia.idAsignacionMembresia;
                    }

                    
                    string queryPago = @"
                        INSERT INTO Pago
                        (idAsignacionPlan, idAsignacionMembresia, FechaPago, Importe)
                        VALUES
                        (@idAsignacionPlan, @idAsignacionMembresia, @FechaPago, @Importe);

                        SELECT SCOPE_IDENTITY();";

                    SqlCommand cmdPago = new SqlCommand(
                        queryPago,
                        oconexion,
                        transaccion
                    );

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

                    cmdPago.Parameters.AddWithValue(
                        "@FechaPago",
                        pago.FechaPago
                    );

                    cmdPago.Parameters.AddWithValue(
                        "@Importe",
                        pago.Importe
                    );

                    idPago = Convert.ToInt32(
                        cmdPago.ExecuteScalar()
                    );

                    transaccion.Commit();
                }
                catch (Exception ex)
                {
                    try
                    {
                        transaccion?.Rollback();
                    }
                    catch
                    {
                    }

                    throw new Exception(
                        "Error al registrar la operación de pago: " + ex.Message
                    );
                }
            }

            return idPago;
        }
    }
}