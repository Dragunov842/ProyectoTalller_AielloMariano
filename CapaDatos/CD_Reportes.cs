using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CapaDatos
{
    public class CD_Reportes
    {

        // Obtiene un resumen de los ingresos generados por cada membresía
        // durante el mes y año actuales.
        public DataTable IngresosMembresias()
        {
            // Tabla que almacenará los resultados de la consulta SQL.
            DataTable tabla = new DataTable();

            // Establece la conexión con la base de datos.
            using (SqlConnection oconexion =
                   new SqlConnection(Conexion.cadena))
            {
                try
                {
                    // Consulta información de las membresías activas.
                    // Para cada una obtiene:
                    // - La cantidad de alumnos con asignaciones vigentes.
                    // - La cantidad de pagos realizados durante el mes actual.
                    // - El importe total de esos pagos.
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

                    // Prepara el comando que contiene la consulta SQL.
                    SqlCommand cmd =
                        new SqlCommand(query, oconexion);

                    cmd.CommandType = CommandType.Text;

                    // SqlDataAdapter ejecuta la consulta y permite
                    // cargar directamente los resultados en un DataTable.
                    SqlDataAdapter adapter =
                        new SqlDataAdapter(cmd);

                    // Completa la tabla con los registros obtenidos.
                    adapter.Fill(tabla);
                }
                catch (Exception ex)
                {
                    // Propaga el error indicando que ocurrió
                    // durante la obtención de los ingresos por membresía.
                    throw new Exception(
                        "Error al obtener los ingresos por membresía: "
                        + ex.Message
                    );
                }
            }

            // Devuelve la tabla con los resultados del reporte.
            return tabla;
        }

        // Obtiene un resumen de los ingresos generados por cada plan
        // de entrenamiento durante el mes y año actuales.
        public DataTable IngresosPlanes()
        {
            // Tabla que almacenará los resultados de la consulta.
            DataTable tabla = new DataTable();

            using (SqlConnection oconexion =
                   new SqlConnection(Conexion.cadena))
            {
                try
                {
                    // Consulta los planes activos y calcula:
                    // - La cantidad de alumnos con asignaciones vigentes.
                    // - La cantidad de pagos asociados al plan durante el mes actual.
                    // - El importe total recaudado durante ese período.
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

                    // Prepara el comando SQL.
                    SqlCommand cmd =
                        new SqlCommand(query, oconexion);

                    cmd.CommandType = CommandType.Text;

                    // Permite ejecutar la consulta y cargar sus resultados
                    // directamente en una tabla en memoria.
                    SqlDataAdapter adapter =
                        new SqlDataAdapter(cmd);

                    // Llena el DataTable con los datos obtenidos.
                    adapter.Fill(tabla);
                }
                catch (Exception ex)
                {
                    // Propaga el error indicando que ocurrió
                    // durante la obtención de los ingresos por plan.
                    throw new Exception(
                        "Error al obtener los ingresos por plan: "
                        + ex.Message
                    );
                }
            }

            return tabla;
        }

        // Obtiene la cantidad de asistencias registradas por día
        // durante los últimos 30 días, incluyendo la fecha actual.
        public DataTable AsistenciasPorFecha()
        {
            // Tabla que almacenará las fechas y sus cantidades de asistencias.
            DataTable tabla = new DataTable();

            using (SqlConnection oconexion =
                   new SqlConnection(Conexion.cadena))
            {
                try
                {
                    // Agrupa las asistencias por fecha.
                    // El período comienza 29 días antes de la fecha actual,
                    // por lo que se consideran 30 fechas contando el día de hoy.
                    string query = @"
                SELECT
                    CAST(Ingreso AS DATE) AS Fecha,
                    COUNT(*) AS CantidadAsistencias

                FROM Asistencia

                WHERE Ingreso >=
                    DATEADD(DAY,-29,CAST(GETDATE() AS DATE))

                GROUP BY CAST(Ingreso AS DATE)

                ORDER BY Fecha ASC;";

                    // Prepara el comando SQL.
                    SqlCommand cmd =
                        new SqlCommand(query, oconexion);

                    cmd.CommandType = CommandType.Text;

                    // Ejecuta la consulta y permite cargar sus resultados
                    // en un DataTable.
                    SqlDataAdapter adapter =
                        new SqlDataAdapter(cmd);

                    // Llena la tabla con las fechas y cantidades obtenidas.
                    adapter.Fill(tabla);
                }
                catch (Exception ex)
                {
                    // Propaga el error indicando que ocurrió
                    // durante la obtención de las asistencias.
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
