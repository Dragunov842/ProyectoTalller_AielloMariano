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

        // Determina si corresponde registrar una nueva membresía
        // o extender la fecha de finalización de una membresía activa.
        public int RegistrarOModificar(AsignacionMembresia obj)
        {
            // Busca si el alumno ya posee una membresía activa.
            AsignacionMembresia membresiaActiva = objCD.BuscarActiva(obj.oAlumno.idAlumno);

            // Si existe una membresía activa, se realiza una renovación
            // en lugar de crear una nueva asignación.
            if (membresiaActiva != null)
            {
                // Calcula la nueva fecha de finalización sumando
                // la duración de la membresía adquirida a la fecha
                // de finalización de la asignación que ya estaba activa.
                DateTime nuevaFechaFin = membresiaActiva.FechaFin.AddDays(obj.oMembresia.Duracion);

                // Actualiza la fecha de finalización de la asignación existente.
                objCD.ActualizarFechaFin(
                    membresiaActiva.idAsignacionMembresia,
                    nuevaFechaFin
                );

                // Devuelve el identificador de la asignación renovada.
                return membresiaActiva.idAsignacionMembresia;
            }

            // Si el alumno no tiene una membresía activa,
            // se prepara una nueva asignación.
            // Se descarta la parte horaria de la fecha de inicio.
            obj.FechaInicio = obj.FechaInicio.Date;

            // Calcula la fecha de finalización sumando
            // la duración de la membresía a la fecha de inicio.
            obj.FechaFin = obj.FechaInicio.AddDays(obj.oMembresia.Duracion);

            // Registra la nueva asignación y devuelve su identificador.
            return objCD.Registrar(obj);
        }
    }
}