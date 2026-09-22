using CapaNegocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class frmReportes : Form
    {
        public frmReportes()
        {
            InitializeComponent();

            CargarReportes();
        }

        private void CargarReportes()
        {
            CN_Reportes objCNReportes = new CN_Reportes();

            dataGridMembresias.DataSource =
                objCNReportes.IngresosMembresias();

            dataGridPlanes.DataSource =
                objCNReportes.IngresosPlanes();

            dataGridAsistencias.DataSource =
                objCNReportes.AsistenciasPorFecha();


            dataGridMembresias.Columns["Membresia"].HeaderText = "Membresía";
            dataGridMembresias.Columns["AlumnosActivos"].HeaderText = "Alumnos activos";
            dataGridMembresias.Columns["CantidadPagos"].HeaderText = "Pagos del mes";
            dataGridMembresias.Columns["Ingresos"].HeaderText = "Ingresos";



            dataGridPlanes.Columns["PlanEntrenamiento"].HeaderText = "Plan";
            dataGridPlanes.Columns["AlumnosActivos"].HeaderText = "Alumnos activos";
            dataGridPlanes.Columns["CantidadPagos"].HeaderText = "Pagos del mes";
            dataGridPlanes.Columns["Ingresos"].HeaderText = "Ingresos";



            dataGridAsistencias.Columns["Fecha"].HeaderText = "Fecha";
            dataGridAsistencias.Columns["CantidadAsistencias"].HeaderText = "Asistencias";
        }
    }
}
