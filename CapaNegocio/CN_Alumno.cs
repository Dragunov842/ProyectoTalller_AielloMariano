using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CapaNegocio
{
    public class CN_Alumno
    {

        private CD_Alumno objcd_Alumno = new CD_Alumno();

        public List<Alumno> Listar()
        {
            return objcd_Alumno.Listar();
        }

        public DataTable ListarConMembresia()
        {
            return objcd_Alumno.ListarConMembresia();
        }

        public Alumno BuscarPorId(int idAlumno)
        {
            return objcd_Alumno.BuscarPorId(idAlumno);
        }

        public Persona BuscarPersonaPorDNI(string dni)
        {
            if (string.IsNullOrWhiteSpace(dni))
                return null;

            return objcd_Alumno.BuscarPersonaPorDNI(dni.Trim());
        }

        public bool ExisteAlumnoPorIdPersona(int idPersona)
        {
            if (idPersona <= 0)
                return false;

            return objcd_Alumno.ExisteAlumnoPorIdPersona(idPersona);
        }

        public int Registrar(Alumno obj)
        {
            return objcd_Alumno.Registrar(obj);
        }

        public bool Editar(Alumno obj)
        {
            return objcd_Alumno.Editar(obj);
        }

        public bool Eliminar(int idAlumno)
        {
            return objcd_Alumno.Eliminar(idAlumno);
        }

        public bool Activar(int idAlumno)
        {
            return objcd_Alumno.Activar(idAlumno);
        }

    }
}
