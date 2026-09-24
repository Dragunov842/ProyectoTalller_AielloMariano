using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CapaDatos
{
    public class CD_Reportes
    {

        public DataTable IngresosMembresias()
        {
            DataTable tabla = new DataTable();

            using (SqlConnection oconexion =
                   new SqlConnection(Conexion.cadena))
            {
                try
                {
                    string query = @"
                SELECT
                    m.Nombre AS Membresia,

                    (
                        SELECT COUNT(*)
                        FROM AsignacionMembresia am2
                        WHERE am2.idMembresia = m.idMembresia
                          AND am2.Estado = 1
                          AND CAST(GETDATE() AS DATE) <= am2.FechaFin
                    ) AS AlumnosActivos,

                    COUNT(p.idPago) AS CantidadPagos,

                    ISNULL(SUM(p.Importe), 0) AS Ingresos

                FROM Membresia m

                LEFT JOIN AsignacionMembresia am
                    ON m.idMembresia = am.idMembresia

                LEFT JOIN Pago p
                    ON am.idAsignacionMembresia = p.idAsignacionMembresia
                    AND MONTH(p.FechaPago) = MONTH(GETDATE())
                    AND YEAR(p.FechaPago) = YEAR(GETDATE())

                WHERE m.Estado = 1

                GROUP BY
                    m.idMembresia,
                    m.Nombre

                ORDER BY m.Nombre;";

                    SqlCommand cmd =
                        new SqlCommand(query, oconexion);

                    cmd.CommandType = CommandType.Text;

                    SqlDataAdapter adapter =
                        new SqlDataAdapter(cmd);

                    adapter.Fill(tabla);
                }
                catch (Exception ex)
                {
                    throw new Exception(
                        "Error al obtener los ingresos por membresía: "
                        + ex.Message
                    );
                }
            }

            return tabla;
        }

        public DataTable IngresosPlanes()
        {
            DataTable tabla = new DataTable();

            using (SqlConnection oconexion =
                   new SqlConnection(Conexion.cadena))
            {
                try
                {
                    string query = @"
                SELECT
                    p.Nombre AS PlanEntrenamiento,

                    (
                        SELECT COUNT(*)
                        FROM AsignacionPlan ap2
                        WHERE ap2.idPlan = p.idPlan
                          AND ap2.Estado = 1
                          AND CAST(GETDATE() AS DATE) <= ap2.FechaFin
                    ) AS AlumnosActivos,

                    COUNT(pg.idPago) AS CantidadPagos,

                    ISNULL(SUM(pg.Importe), 0) AS Ingresos

                FROM PlanEntrenamiento p

                LEFT JOIN AsignacionPlan ap
                    ON p.idPlan = ap.idPlan

                LEFT JOIN Pago pg
                    ON ap.idAsignacion = pg.idAsignacionPlan
                    AND MONTH(pg.FechaPago) = MONTH(GETDATE())
                    AND YEAR(pg.FechaPago) = YEAR(GETDATE())

                WHERE p.Estado = 1

                GROUP BY
                    p.idPlan,
                    p.Nombre

                ORDER BY p.Nombre;";

                    SqlCommand cmd =
                        new SqlCommand(query, oconexion);

                    cmd.CommandType = CommandType.Text;

                    SqlDataAdapter adapter =
                        new SqlDataAdapter(cmd);

                    adapter.Fill(tabla);
                }
                catch (Exception ex)
                {
                    throw new Exception(
                        "Error al obtener los ingresos por plan: "
                        + ex.Message
                    );
                }
            }

            return tabla;
        }

        public DataTable AsistenciasPorFecha()
        {
            DataTable tabla = new DataTable();

            using (SqlConnection oconexion =
                   new SqlConnection(Conexion.cadena))
            {
                try
                {
                    string query = @"
                SELECT
                    CAST(Ingreso AS DATE) AS Fecha,
                    COUNT(*) AS CantidadAsistencias

                FROM Asistencia

                WHERE Ingreso >=
                    DATEADD(DAY,-29,CAST(GETDATE() AS DATE))

                GROUP BY CAST(Ingreso AS DATE)

                ORDER BY Fecha ASC;";

                    SqlCommand cmd =
                        new SqlCommand(query, oconexion);

                    cmd.CommandType = CommandType.Text;

                    SqlDataAdapter adapter =
                        new SqlDataAdapter(cmd);

                    adapter.Fill(tabla);
                }
                catch (Exception ex)
                {
                    throw new Exception(
                        "Error al obtener las asistencias por fecha: "
                        + ex.Message
                    );
                }
            }

            return tabla;
        }

    }
}