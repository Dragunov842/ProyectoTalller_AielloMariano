using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Empleado
    {
        private CD_Empleado objcd_Empleado = new CD_Empleado();

        public List<Empleado> Listar()
        {
            return objcd_Empleado.Listar();
        }


        public Persona BuscarPersonaPorDNI(string dni)
        {
            if (string.IsNullOrWhiteSpace(dni))
                return null;

            return objcd_Empleado.BuscarPersonaPorDNI(dni.Trim());
        }

        public bool ExisteEmpleadoPorIdPersona(int idPersona)
        {
            if (idPersona <= 0)
                return false;

            return objcd_Empleado.ExisteEmpleadoPorIdPersona(idPersona);
        }


        public int Registrar(Empleado obj)
        {
            if (obj.oRol.idRol == 1)
            {
                return 0;
            }

            return objcd_Empleado.Registrar(obj);
        }


        public bool Editar(Empleado obj)
        {
            // Obtiene la lista de empleados y busca al empleado que se desea modificar,
            // utilizando su identificador para encontrar el registro correspondiente.
            Empleado empleadoActual = objcd_Empleado.Listar()
                .FirstOrDefault(e => e.idEmpleado == obj.idEmpleado);

            // Comprueba si el empleado existe y si tiene el rol de administrador (ID 1).
            // Si es administrador, se impide su modificación.
            if (empleadoActual != null &&
                empleadoActual.oRol.idRol == 1)
            {
                return false;
            }

            // Si no se trata de un administrador protegido,
            // delega la modificación a la capa de datos.
            return objcd_Empleado.Editar(obj);
        }

        public bool Eliminar(int idEmpleado)
        {
            // Busca al empleado que se desea eliminar mediante su identificador.
            Empleado empleado = objcd_Empleado.Listar()
                .FirstOrDefault(e => e.idEmpleado == idEmpleado);

            // Comprueba si el empleado existe y posee el rol de administrador.
            // En ese caso, impide su eliminación.
            if (empleado != null && empleado.oRol.idRol == 1)
            {
                return false;
            }

            // Si no es un administrador protegido,
            // delega la eliminación a la capa de datos.
            return objcd_Empleado.Eliminar(idEmpleado);
        }

        public bool Activar(int idEmpleado)
        {
            // Busca al empleado que se desea activar.
            Empleado empleado = objcd_Empleado.Listar()
                .FirstOrDefault(e => e.idEmpleado == idEmpleado);

            // Impide realizar esta operación sobre una cuenta de administrador.
            if (empleado != null && empleado.oRol.idRol == 1)
            {
                return false;
            }

            // Si el empleado no tiene el rol protegido,
            // solicita a la capa de datos que active su cuenta.
            return objcd_Empleado.Activar(idEmpleado);
        }

        // Valida la identidad de dos usuarios diferentes para autorizar
        // operaciones que requieren una doble autorización administrativa.
        public bool ValidarDobleAutorizacion(
        string usuario1,
        string contraseña1,
        string usuario2,
        string contraseña2)
        {
            // Comprueba que ninguno de los campos esté vacío
            // o contenga únicamente espacios en blanco.
            if (string.IsNullOrWhiteSpace(usuario1) ||
                string.IsNullOrWhiteSpace(contraseña1) ||
                string.IsNullOrWhiteSpace(usuario2) ||
                string.IsNullOrWhiteSpace(contraseña2))
            {
                // Si falta algún dato, la autorización se rechaza.
                return false;
            }

            // Comprueba que ambos usuarios sean diferentes.
            // Trim() elimina los espacios al principio y al final
            // antes de comparar los nombres de usuario.
            if (usuario1.Trim() == usuario2.Trim())
            {
                return false;
            }

            // Si las validaciones iniciales se cumplen,
            // delega la comprobación de las credenciales a la capa de datos.
            // Allí se verifica que ambos usuarios sean administradores activos
            // y que sus credenciales sean válidas.
            return objcd_Empleado.ValidarDobleAutorizacion(
                usuario1.Trim(),
                contraseña1,
                usuario2.Trim(),
                contraseña2);
        }

    }
}