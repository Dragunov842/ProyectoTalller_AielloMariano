using CapaDatos;
using CapaEntidad;
using CapaEntidad.Membresias;
using System;
using System.Collections.Generic;

namespace CapaNegocio
{
    public class CN_AsignacionPlan
    {
        private CD_AsignacionPlan objCD = new CD_AsignacionPlan();
        private CN_AsignacionMembresia objCNMembresia = new CN_AsignacionMembresia();

        public int Registrar(AsignacionPlan obj)
        {
            return objCD.Registrar(obj);
        }

        public int ContarActivos(int idAlumno)
        {
            return objCD.ContarActivos(idAlumno);
        }

        public bool YaTienePlanActivo(int idAlumno, int idPlan)
        {
            return objCD.YaTienePlanActivo(idAlumno, idPlan);
        }

        public List<AsignacionPlan> ListarActivosPorAlumno(int idAlumno)
        {
            return objCD.ListarActivosPorAlumno(idAlumno);
        }

        public bool PuedeContratarPlan(int idAlumno, int idPlan)
        {
            AsignacionMembresia membresiaActiva = objCNMembresia.BuscarActiva(idAlumno);

            if (membresiaActiva == null)
                return false;

            if (ContarActivos(idAlumno) >= 5)
                return false;

            if (YaTienePlanActivo(idAlumno, idPlan))
                return false;

            return true;
        }
    }
}