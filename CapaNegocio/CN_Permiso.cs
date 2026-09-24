using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapaNegocio
{
    public class CN_Permiso
    {


        private CD_Permiso objcd_Permiso = new CD_Permiso();

        public List<Permiso> Listar(int idRol)
        {
            return objcd_Permiso.Listar(idRol);
        }

    }
}
