using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapaNegocio
{
    public class CN_Membresia
    {

        private CD_Membresia objcd_Membresia = new CD_Membresia();

        public List<Membresia> Listar()
        {
            return objcd_Membresia.Listar();
        }

        public int Registrar(Membresia obj)
        {
            return objcd_Membresia.Registrar(obj);
        }

        public bool Editar(Membresia obj)
        {
            return objcd_Membresia.Editar(obj);
        }

        public bool Eliminar(int idMembresia)
        {
            return objcd_Membresia.Eliminar(idMembresia);
        }

        public bool Activar(int idMembresia)
        {
            return objcd_Membresia.Activar(idMembresia);
        }

    }
}
