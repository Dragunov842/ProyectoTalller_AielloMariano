using System;
using System.Collections.Generic;
using System.Text;

namespace CapaEntidad
{
    public class PlanEntrenamiento
    {
        public int idPlan { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Duracion { get; set; }
        public bool Estado { get; set; }
    }
}