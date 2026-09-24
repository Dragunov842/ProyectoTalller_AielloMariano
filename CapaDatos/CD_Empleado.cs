using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_Empleado
    {
        public List<Empleado> Listar()
        {
            List<Empleado> lista = new List<Empleado>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    string query = @"SELECT 
                        e.idEmpleado,
                        e.Fecha_Alta,
                        e.Usuario,
                        e.Contraseña,
                        e.Estado,

                        p.idPersona,
                        p.Nombre AS NombrePersona,
                        p.Apellido,
                        p.DNI,
                        p.Telefono,
                        p.Email,

                        r.idRol,
                        r.Nombre AS NombreRol
                    FROM Empleado e
                    INNER JOIN Persona p ON e.idPersona = p.idPersona
                    INNER JOIN Rol r ON e.idRol = r.idRol";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Empleado()
                            {
                                idEmpleado = Convert.ToInt32(dr["idEmpleado"]),
                                Fecha_Alta = Convert.ToDateTime(dr["Fecha_Alta"]),
                                Usuario = dr["Usuario"].ToString(),
                                Contraseña = dr["Contraseña"].ToString(),
                                Estado = Convert.ToBoolean(dr["Estado"]),

                                oPersona = new Persona()
                                {
                                    idPersona = Convert.ToInt32(dr["idPersona"]),
                                    Nombre = dr["NombrePersona"].ToString(),
                                    Apellido = dr["Apellido"].ToString(),
                                    DNI = dr["DNI"].ToString(),
                                    Telefono = dr["Telefono"].ToString(),
                                    Email = dr["Email"].ToString()
                                },

                                oRol = new Rol()
                                {
                                    idRol = Convert.ToInt32(dr["idRol"]),
                                    Nombre = dr["NombreRol"].ToString()
                                }
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al obtener empleados: " + ex.Message);
                }
            }

            return lista;
        }

        public int Registrar(Empleado obj)
        {
            int idEmpleadoGenerado = 0;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                SqlTransaction transaccion = null;

                try
                {
                    oconexion.Open();

                    transaccion = oconexion.BeginTransaction();

                   
                    string queryPersona = @"
                INSERT INTO Persona (Nombre, Apellido, DNI, Telefono, Email)
                VALUES (@Nombre, @Apellido, @DNI, @Telefono, @Email);

                SELECT SCOPE_IDENTITY();";

                    SqlCommand cmdPersona = new SqlCommand(
                        queryPersona,
                        oconexion,
                        transaccion);

                    cmdPersona.Parameters.AddWithValue("@Nombre", obj.oPersona.Nombre);
                    cmdPersona.Parameters.AddWithValue("@Apellido", obj.oPersona.Apellido);
                    cmdPersona.Parameters.AddWithValue("@DNI", obj.oPersona.DNI);
                    cmdPersona.Parameters.AddWithValue("@Telefono", obj.oPersona.Telefono);
                    cmdPersona.Parameters.AddWithValue("@Email", obj.oPersona.Email);

                    int idPersonaGenerado =
                        Convert.ToInt32(cmdPersona.ExecuteScalar());

                   
                    string queryEmpleado = @"
                INSERT INTO Empleado
                (idPersona, idRol, Usuario, Contraseña)
                VALUES
                (@idPersona, @idRol, @Usuario, @Contraseña);

                SELECT SCOPE_IDENTITY();";

                    SqlCommand cmdEmpleado = new SqlCommand(
                        queryEmpleado,
                        oconexion,
                        transaccion);

                    cmdEmpleado.Parameters.AddWithValue(
                        "@idPersona",
                        idPersonaGenerado);

                    cmdEmpleado.Parameters.AddWithValue(
                        "@idRol",
                        obj.oRol.idRol);

                    cmdEmpleado.Parameters.AddWithValue(
                        "@Usuario",
                        obj.Usuario);

                    cmdEmpleado.Parameters.AddWithValue(
                        "@Contraseña",
                        obj.Contraseña);

                    idEmpleadoGenerado =
                        Convert.ToInt32(cmdEmpleado.ExecuteScalar());

                    transaccion.Commit();
                }
                catch (Exception ex)
                {
                    if (transaccion != null)
                        transaccion.Rollback();

                    throw new Exception(
                        "Error al registrar usuario: " + ex.Message);
                }
            }

            return idEmpleadoGenerado;
        }

        public bool Editar(Empleado obj)
        {
            bool respuesta = false;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                SqlTransaction transaccion = null;

                try
                {
                    oconexion.Open();

                    transaccion = oconexion.BeginTransaction();

                    
                    string queryPersona = @"
                UPDATE Persona
                SET
                    Nombre = @Nombre,
                    Apellido = @Apellido,
                    DNI = @DNI,
                    Telefono = @Telefono,
                    Email = @Email
                WHERE idPersona = @idPersona";

                    SqlCommand cmdPersona = new SqlCommand(
                        queryPersona,
                        oconexion,
                        transaccion);

                    cmdPersona.Parameters.AddWithValue(
                        "@Nombre",
                        obj.oPersona.Nombre);

                    cmdPersona.Parameters.AddWithValue(
                        "@Apellido",
                        obj.oPersona.Apellido);

                    cmdPersona.Parameters.AddWithValue(
                        "@DNI",
                        obj.oPersona.DNI);

                    cmdPersona.Parameters.AddWithValue(
                        "@Telefono",
                        obj.oPersona.Telefono);

                    cmdPersona.Parameters.AddWithValue(
                        "@Email",
                        obj.oPersona.Email);

                    cmdPersona.Parameters.AddWithValue(
                        "@idPersona",
                        obj.oPersona.idPersona);

                    cmdPersona.ExecuteNonQuery();

                    string queryEmpleado;

                    if (string.IsNullOrWhiteSpace(obj.Contraseña))
                    {
                        queryEmpleado = @"
                    UPDATE Empleado
                    SET
                        idRol = @idRol,
                        Usuario = @Usuario
                    WHERE idEmpleado = @idEmpleado";
                    }
                    else
                    {
                        queryEmpleado = @"
                    UPDATE Empleado
                    SET
                        idRol = @idRol,
                        Usuario = @Usuario,
                        Contraseña = @Contraseña
                    WHERE idEmpleado = @idEmpleado";
                    }

                    SqlCommand cmdEmpleado = new SqlCommand(
                        queryEmpleado,
                        oconexion,
                        transaccion);

                    cmdEmpleado.Parameters.AddWithValue(
                        "@idRol",
                        obj.oRol.idRol);

                    cmdEmpleado.Parameters.AddWithValue(
                        "@Usuario",
                        obj.Usuario);

                    cmdEmpleado.Parameters.AddWithValue(
                        "@idEmpleado",
                        obj.idEmpleado);

                    if (!string.IsNullOrWhiteSpace(obj.Contraseña))
                    {
                        cmdEmpleado.Parameters.AddWithValue(
                            "@Contraseña",
                            obj.Contraseña);
                    }

                    cmdEmpleado.ExecuteNonQuery();


                    transaccion.Commit();

                    respuesta = true;
                }
                catch (Exception ex)
                {
                    if (transaccion != null)
                        transaccion.Rollback();

                    throw new Exception(
                        "Error al editar empleado: " + ex.Message);
                }
            }

            return respuesta;
        }

        public bool Eliminar(int idEmpleado)
        {
            bool respuesta = false;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    string query = @"
                UPDATE Empleado
                SET Estado = 0
                WHERE idEmpleado = @idEmpleado";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@idEmpleado", idEmpleado);

                    oconexion.Open();

                    respuesta = cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception(
                        "Error al eliminar empleado: " + ex.Message);
                }
            }

            return respuesta;
        }

        public bool Activar(int idEmpleado)
        {
            bool respuesta = false;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    string query = @"
                UPDATE Empleado
                SET Estado = 1
                WHERE idEmpleado = @idEmpleado";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@idEmpleado", idEmpleado);

                    oconexion.Open();

                    respuesta = cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception(
                        "Error al activar empleado: " + ex.Message);
                }
            }

            return respuesta;
        }


    }
}