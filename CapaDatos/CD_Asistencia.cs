using CapaEntidad;
using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace CapaDatos
{
    public class CD_Asistencia
    {
        public int Registrar(Asistencia obj)
        {
            int idGenerado = 0;

            using (SqlConnection oconexion =
                   new SqlConnection(Conexion.cadena))
            {
                try
                {
                    string query = @"
                        INSERT INTO Asistencia
                        (idAlumno, Ingreso)
                        VALUES
                        (@idAlumno, @Ingreso);

                        SELECT SCOPE_IDENTITY();";

                    SqlCommand cmd =
                        new SqlCommand(query, oconexion);

                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue(
                        "@idAlumno",
                        obj.oAlumno.idAlumno
                    );

                    cmd.Parameters.AddWithValue(
                        "@Ingreso",
                        obj.Ingreso
                    );

                    oconexion.Open();

                    idGenerado =
                        Convert.ToInt32(cmd.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    throw new Exception(
                        "Error al registrar asistencia: " +
                        ex.Message
                    );
                }
            }

            return idGenerado;
        }


        public bool YaAsistioHoy(int idAlumno)
        {
            bool respuesta = false;

            using (SqlConnection oconexion =
                   new SqlConnection(Conexion.cadena))
            {
                try
                {
                    string query = @"
                SELECT COUNT(*)
                FROM Asistencia
                WHERE idAlumno = @idAlumno
                  AND CAST(Ingreso AS date) = CAST(GETDATE() AS date)";

                    SqlCommand cmd =
                        new SqlCommand(query, oconexion);

                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue(
                        "@idAlumno",
                        idAlumno
                    );

                    oconexion.Open();

                    respuesta =
                        Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                }
                catch (Exception ex)
                {
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