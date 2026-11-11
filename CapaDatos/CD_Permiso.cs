
using CapaEntidad;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;

using CapaEntidad;

namespace CapaDatos
{
    public class CD_Permiso
    {
        // Obtiene la lista de permisos correspondientes a un rol determinado.
        // Cada permiso representa una sección o menú al que puede acceder el usuario.
        public List<Permiso> Listar(int idRol)
        {
            // Lista que almacenará los permisos encontrados.
            List<Permiso> lista = new List<Permiso>();

            // Establece la conexión con la base de datos.
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    // Consulta los permisos asociados al rol recibido.
                    // Se relacionan las tablas Permiso y RolPermiso mediante idPermiso.
                    // El filtro permite obtener únicamente los permisos de ese rol.
                    string query = @"
                        SELECT 
                            p.idPermiso,
                            p.NombreMenu
                        FROM Permiso p
                        INNER JOIN RolPermiso rp 
                            ON p.idPermiso = rp.idPermiso
                        WHERE rp.idRol = @idRol";

                    // Prepara el comando SQL que se ejecutará.
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    // Asigna el identificador del rol como parámetro de la consulta.
                    cmd.Parameters.AddWithValue("@idRol", idRol);

                    // Abre la conexión con SQL Server.
                    oconexion.Open();

                    // ExecuteReader permite recorrer los registros devueltos por la consulta.
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        // Recorre todos los permisos encontrados.
                        while (dr.Read())
                        {
                            // Crea un objeto Permiso con los datos del registro actual
                            // y lo agrega a la lista que se devolverá.
                            lista.Add(new Permiso()
                            {
                                idPermiso = Convert.ToInt32(dr["idPermiso"]),
                                NombreMenu = dr["NombreMenu"].ToString()
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Propaga el error agregando información sobre la operación.
                    throw new Exception("Error al obtener permisos: " + ex.Message);
                }
            }

            // Devuelve todos los permisos asociados al rol consultado.
            return lista;
        }
    }
}
