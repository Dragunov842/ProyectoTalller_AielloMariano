using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Empleado
    {
        private CD_Empleado objcd_Empleado= new CD_Empleado();

        public List<Empleado> Listar()
        {
            return objcd_Empleado.Listar();
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
            Empleado empleadoActual = objcd_Empleado.Listar()
                .FirstOrDefault(e => e.idEmpleado == obj.idEmpleado);

            if (empleadoActual != null &&
                empleadoActual.oRol.idRol == 1)
            {
                return false;
            }

            return objcd_Empleado.Editar(obj);
        }

        public bool Eliminar(int idEmpleado)
        {
            Empleado empleado = objcd_Empleado.Listar()
                .FirstOrDefault(e => e.idEmpleado == idEmpleado);

            if (empleado != null && empleado.oRol.idRol == 1)
            {
                return false;
            }

            return objcd_Empleado.Eliminar(idEmpleado);
        }

        public bool Activar(int idEmpleado)
        {
            Empleado empleado = objcd_Empleado.Listar()
                .FirstOrDefault(e => e.idEmpleado == idEmpleado);

            if (empleado != null && empleado.oRol.idRol == 1)
            {
                return false;
            }

            return objcd_Empleado.Activar(idEmpleado);
        }

    }
}
