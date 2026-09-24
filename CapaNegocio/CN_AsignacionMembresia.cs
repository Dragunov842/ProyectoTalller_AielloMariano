using CapaDatos;
using CapaEntidad;
using CapaEntidad.Membresias;
using System;

namespace CapaNegocio
{
    public class CN_AsignacionMembresia
    {
        private CD_AsignacionMembresia objCD = new CD_AsignacionMembresia();

        public AsignacionMembresia BuscarActiva(int idAlumno)
        {
            return objCD.BuscarActiva(idAlumno);
        }

        public int Registrar(AsignacionMembresia obj)
        {
            return objCD.Registrar(obj);
        }

        public bool ActualizarFechaFin(int idAsignacionMembresia, DateTime nuevaFechaFin)
        {
            return objCD.ActualizarFechaFin(idAsignacionMembresia, nuevaFechaFin);
        }

        public int RegistrarOModificar(AsignacionMembresia obj)
        {
            AsignacionMembresia membresiaActiva = objCD.BuscarActiva(obj.oAlumno.idAlumno);

            if (membresiaActiva != null)
            {
                DateTime nuevaFechaFin = membresiaActiva.FechaFin.AddDays(obj.oMembresia.Duracion);

                objCD.ActualizarFechaFin(
                    membresiaActiva.idAsignacionMembresia,
                    nuevaFechaFin
                );

                return membresiaActiva.idAsignacionMembresia;
            }

            obj.FechaInicio = obj.FechaInicio.Date;
            obj.FechaFin = obj.FechaInicio.AddDays(obj.oMembresia.Duracion);

            return objCD.Registrar(obj);
        }
    }
}