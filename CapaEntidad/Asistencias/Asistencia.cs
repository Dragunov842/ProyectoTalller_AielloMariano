using System;
using System.Collections.Generic;
using System.Text;

using System;

namespace CapaEntidad
{
    public class Asistencia
    {
        public int idAsistencia { get; set; }
        public Alumno oAlumno { get; set; }
        public DateTime Ingreso { get; set; }
    }
}