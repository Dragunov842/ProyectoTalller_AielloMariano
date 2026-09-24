using CapaDatos;
using CapaEntidad;
using CapaEntidad.Membresias;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CapaNegocio
{
    public class CN_Asistencia
    {

        private CD_Asistencia objCDAsistencia =
           new CD_Asistencia();

        private CD_Alumno objCDAlumno =
            new CD_Alumno();

        private CN_AsignacionMembresia objCNMembresia =
            new CN_AsignacionMembresia();


        public int Registrar(int idAlumno)
        {
           
            Alumno alumno =
                objCDAlumno.BuscarPorId(idAlumno);

            if (alumno == null)
            {
                throw new Exception(
                    "El alumno no existe."
                );
            }

           
            if (!alumno.Estado)
            {
                throw new Exception(
                    "El alumno se encuentra dado de baja."
                );
            }

           
            AsignacionMembresia membresiaActiva =
                objCNMembresia.BuscarActiva(idAlumno);

            if (membresiaActiva == null)
            {
                throw new Exception(
                    "El alumno no tiene una membresía vigente."
                );
            }

            
            if (objCDAsistencia.YaAsistioHoy(idAlumno))
            {
                throw new Exception(
                    "El alumno ya tiene registrada una asistencia el día de hoy."
                );
            }

            
            Asistencia asistencia = new Asistencia()
            {
                oAlumno = alumno,
                Ingreso = DateTime.Now
            };

            return objCDAsistencia.Registrar(asistencia);
        }
    }
}