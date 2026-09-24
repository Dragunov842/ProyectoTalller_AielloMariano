using CapaDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CapaNegocio
{
    public class CN_Reportes
    {
            private CD_Reportes objCD = new CD_Reportes();

            public DataTable IngresosMembresias()
            {
                return objCD.IngresosMembresias();
            }

            public DataTable IngresosPlanes()
            {
                return objCD.IngresosPlanes();
            }

            public DataTable AsistenciasPorFecha()
            {
                return objCD.AsistenciasPorFecha();
            }

    }
}
