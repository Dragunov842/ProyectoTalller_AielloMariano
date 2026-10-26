using System;
using System.Collections.Generic;
using System.Text;
using CapaDatos;

namespace CapaNegocio
{
        public class CN_Backup
        {
            private CD_Backup objetoCD = new CD_Backup();

            public void CrearBackup(string rutaDestino)
            {
                objetoCD.CrearBackup(rutaDestino);
            }

            public void RestaurarBackup(string rutaOrigen)
            {
                objetoCD.RestaurarBackup(rutaOrigen);
            }
        }
    }
