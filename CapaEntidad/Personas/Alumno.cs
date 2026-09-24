using System;
using System.Collections.Generic;
using System.Text;

namespace CapaEntidad
{
    public class Alumno
    {
        public int idAlumno { get; set; }
        public Persona oPersona { get; set; }
        public DateTime Fecha_Alta { get; set; }
        public bool Estado { get; set; }
    }
}