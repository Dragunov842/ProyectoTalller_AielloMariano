using System;
using System.Collections.Generic;
using System.Text;

namespace CapaEntidad.Membresias
{
    public class AsignacionMembresia
    {

        public int idAsignacionMembresia { get; set; }
        public Alumno oAlumno { get; set; }
        public Membresia oMembresia { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public bool Estado { get; set; }

    }
}
