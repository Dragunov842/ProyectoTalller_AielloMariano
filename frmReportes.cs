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
            
            ConfigurarDiseñoResponsive();

            CargarReportes();
        }


        private void ConfigurarDiseñoResponsive()
        {
            // Configuración común de las tablas
            ConfigurarTabla(dataGridMembresias);
            ConfigurarTabla(dataGridPlanes);
            ConfigurarTabla(dataGridAsistencias);

            // Contenedor principal
            TableLayoutPanel contenedor = new TableLayoutPanel();

            contenedor.Dock = DockStyle.Fill;
            contenedor.ColumnCount = 2;
            contenedor.RowCount = 1;
            contenedor.Padding = new Padding(12);
            contenedor.BackColor = this.BackColor;

            // Distribución de columnas
            contenedor.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 65F));

            contenedor.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 35F));

            contenedor.RowStyles.Add(
                new RowStyle(SizeType.Percent, 100F));

            // Panel izquierdo: dos reportes
            TableLayoutPanel panelIzquierdo = new TableLayoutPanel();

            panelIzquierdo.Dock = DockStyle.Fill;
            panelIzquierdo.ColumnCount = 1;
            panelIzquierdo.RowCount = 2;

            panelIzquierdo.RowStyles.Add(
                new RowStyle(SizeType.Percent, 50F));

            panelIzquierdo.RowStyles.Add(
                new RowStyle(SizeType.Percent, 50F));

            panelIzquierdo.Controls.Add(dataGridMembresias, 0, 0);
            panelIzquierdo.Controls.Add(dataGridPlanes, 0, 1);

            // Panel derecho: asistencias
            dataGridAsistencias.Dock = DockStyle.Fill;

            // Agregar paneles al contenedor
            contenedor.Controls.Add(panelIzquierdo, 0, 0);
            contenedor.Controls.Add(dataGridAsistencias, 1, 0);

            // Reemplazar la distribución original
            this.Controls.Clear();
            this.Controls.Add(contenedor);
        }

        private void ConfigurarTabla(DataGridView tabla)
        {
            tabla.Dock = DockStyle.Fill;

            tabla.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            tabla.ScrollBars = ScrollBars.Both;

            tabla.AllowUserToAddRows = false;
            tabla.AllowUserToResizeRows = false;

            tabla.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            tabla.MultiSelect = false;

            tabla.AutoSizeRowsMode =
                DataGridViewAutoSizeRowsMode.None;

            tabla.DefaultCellStyle.WrapMode =
                DataGridViewTriState.False;

            tabla.ColumnHeadersHeightSizeMode =
                DataGridViewColumnHeadersHeightSizeMode.AutoSize;
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
