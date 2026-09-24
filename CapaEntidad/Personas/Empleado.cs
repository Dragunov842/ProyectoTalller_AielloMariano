using System;
using System.Collections.Generic;
using System.Text;

namespace CapaEntidad
{
    public class Empleado
    {
        public int idEmpleado { get; set; }
        public Persona oPersona { get; set; }
        public Rol oRol { get; set; }
        public DateTime Fecha_Alta { get; set; }
        public string Usuario { get; set; }
        public string Contraseña { get; set; }
        public bool Estado { get; set; }
    }
}