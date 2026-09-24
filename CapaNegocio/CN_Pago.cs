using CapaDatos;
using CapaEntidad;
using CapaEntidad.Membresias;
using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace CapaNegocio
{
    public class CN_Pago
    {
        private CD_Pago objCDPago = new CD_Pago();
        private CD_Alumno objCDAlumno = new CD_Alumno();
        private CD_Membresia objCDMembresia = new CD_Membresia();
        private CN_AsignacionMembresia objCNAsignacionMembresia = new CN_AsignacionMembresia();
        private CD_AsignacionPlan objCDAsignacionPlan = new CD_AsignacionPlan();

        private int ContarPlanesActivos(int idAlumno)
        {
            return objCDAsignacionPlan.ContarActivos(idAlumno);
        }

        private bool YaTienePlanActivo(int idAlumno, int idPlan)
        {
            return objCDAsignacionPlan.YaTienePlanActivo(idAlumno, idPlan);
        }

        public int RegistrarPagoMembresia(int idAlumno, int idMembresia, DateTime fechaPago)
        {
           
            Alumno alumno = objCDAlumno.BuscarPorId(idAlumno);

            if (alumno == null)
                throw new Exception("El alumno no existe.");

           
            if (!alumno.Estado)
                throw new Exception("El alumno se encuentra dado de baja.");

            
            Membresia membresia = objCDMembresia.BuscarPorId(idMembresia);

            if (membresia == null)
                throw new Exception("La membresía no existe.");

            
            if (!membresia.Estado)
                throw new Exception("La membresía seleccionada no está activa.");

          
            AsignacionMembresia membresiaActiva =
                objCNAsignacionMembresia.BuscarActiva(idAlumno);

            AsignacionMembresia asignacion = new AsignacionMembresia();

            bool renovar = false;

            
            if (membresiaActiva != null)
            {
                asignacion.idAsignacionMembresia =
                    membresiaActiva.idAsignacionMembresia;

                asignacion.oAlumno = alumno;
                asignacion.oMembresia = membresia;
                asignacion.FechaInicio = membresiaActiva.FechaInicio;
                asignacion.FechaFin =
                    membresiaActiva.FechaFin.AddDays(membresia.Duracion);
                asignacion.Estado = true;

                renovar = true;
            }
            else
            {
                
                asignacion.oAlumno = alumno;
                asignacion.oMembresia = membresia;
                asignacion.FechaInicio = fechaPago.Date;
                asignacion.FechaFin =
                    fechaPago.Date.AddDays(membresia.Duracion);
                asignacion.Estado = true;
            }

            
            Pago pago = new Pago()
            {
                oAsignacionMembresia = asignacion,
                FechaPago = fechaPago,
                Importe = membresia.Precio
            };

          
            return objCDPago.RegistrarPagoConAsignacion(
                pago,
                null,
                asignacion,
                renovar
            );
        }


        public int RegistrarPagoPlan(int idAlumno, int idPlan, DateTime fechaPago)
        {
            
            Alumno alumno = objCDAlumno.BuscarPorId(idAlumno);

            if (alumno == null)
                throw new Exception("El alumno no existe.");

           
            if (!alumno.Estado)
                throw new Exception("El alumno se encuentra dado de baja.");

            
            CD_PlanEntrenamiento objCDPlan = new CD_PlanEntrenamiento();

            PlanEntrenamiento plan = objCDPlan.BuscarPorId(idPlan);

            if (plan == null)
                throw new Exception("El plan no existe.");

           
            if (!plan.Estado)
                throw new Exception("El plan seleccionado no está activo.");

            
            AsignacionMembresia membresiaActiva =
                objCNAsignacionMembresia.BuscarActiva(idAlumno);

            if (membresiaActiva == null)
                throw new Exception(
                    "El alumno debe tener una membresía activa para contratar un plan."
                );

            
            if (ContarPlanesActivos(idAlumno) >= 5)
                throw new Exception(
                    "El alumno ya tiene el máximo de 5 planes activos."
                );

            
            if (YaTienePlanActivo(idAlumno, idPlan))
                throw new Exception(
                    "El alumno ya tiene contratado este plan."
                );

            
            AsignacionPlan asignacion = new AsignacionPlan()
            {
                oAlumno = alumno,
                oPlan = plan,
                FechaInicio = fechaPago.Date,
                FechaFin = fechaPago.Date.AddDays(plan.Duracion),
                Estado = true
            };

            
            Pago pago = new Pago()
            {
                oAsignacionPlan = asignacion,
                FechaPago = fechaPago,
                Importe = plan.Precio
            };

            
            return objCDPago.RegistrarPagoConAsignacion(
                pago,
                asignacion,
                null,
                false
            );
        }


    }
}