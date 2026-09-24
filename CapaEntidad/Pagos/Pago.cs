using CapaEntidad.Membresias;
using System;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapaEntidad
{
    public class Pago
    {
        public int idPago { get; set; }
        public AsignacionPlan oAsignacionPlan { get; set; }
        public AsignacionMembresia oAsignacionMembresia { get; set; }
        public DateTime FechaPago { get; set; }
        public decimal Importe { get; set; }
    }
}