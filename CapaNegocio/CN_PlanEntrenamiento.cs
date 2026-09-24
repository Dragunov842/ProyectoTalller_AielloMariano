using CapaDatos;
using CapaEntidad;
using System.Collections.Generic;

namespace CapaNegocio
{
    public class CN_PlanEntrenamiento
    {
        private CD_PlanEntrenamiento objcd_PlanEntrenamiento = new CD_PlanEntrenamiento();

        public List<PlanEntrenamiento> Listar()
        {
            return objcd_PlanEntrenamiento.Listar();
        }

        public int Registrar(PlanEntrenamiento obj)
        {
            return objcd_PlanEntrenamiento.Registrar(obj);
        }

        public bool Editar(PlanEntrenamiento obj)
        {
            return objcd_PlanEntrenamiento.Editar(obj);
        }

        public bool Eliminar(int idPlan)
        {
            return objcd_PlanEntrenamiento.Eliminar(idPlan);
        }

        public bool Activar(int idPlan)
        {
            return objcd_PlanEntrenamiento.Activar(idPlan);
        }

    }
}