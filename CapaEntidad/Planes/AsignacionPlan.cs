using System;
using System.Collections.Generic;
using System.Text;

using System;

namespace CapaEntidad
{
    public class AsignacionPlan
    {
        public int idAsignacion { get; set; }
        public Alumno oAlumno { get; set; }
        public PlanEntrenamiento oPlan { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public bool Estado { get; set; }
    }
}