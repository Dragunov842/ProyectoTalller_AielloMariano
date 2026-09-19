using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using CapaEntidad;
using CapaNegocio;
using FontAwesome.Sharp;

namespace CapaPresentacion
{
    public partial class Inicio : Form
    {
        private static Empleado empleadoActual;
        private static IconMenuItem menuActivo = null;
        private static Form FormActivo = null;

        public Inicio(Empleado objEmpleado)
        {
            empleadoActual = objEmpleado;

            InitializeComponent();


            this.ControlBox = false;
        }

        private bool TienePermiso(List<Permiso> listaPermisos, string nombreMenu)
        {
            return listaPermisos.Exists(x => x.NombreMenu == nombreMenu);
        }

        private void Inicio_Load(object sender, EventArgs e)
        {
            List<Permiso> listaPermisos = new CN_Permiso().Listar(empleadoActual.oRol.idRol);   

            lblusuario.Text = empleadoActual.Usuario;

            panel1.Dock = DockStyle.Top;
            panel1.Height = 60;

            menuStrip1.Dock = DockStyle.Top;

            contenedor.Dock = DockStyle.Fill;

            menuAlumnos.Visible = TienePermiso(listaPermisos, "Alumnos");
            menuAsistencias.Visible = TienePermiso(listaPermisos, "Asistencias");
            menuMembresias.Visible = TienePermiso(listaPermisos, "Membresias");
            menuPlanes.Visible = TienePermiso(listaPermisos, "Planes");
            menuPagos.Visible = TienePermiso(listaPermisos, "Pagos");
            menuReportes.Visible = TienePermiso(listaPermisos, "Reportes");
            menuUsuarios.Visible = TienePermiso(listaPermisos, "Usuarios");

        }

        private void Abrir_Formulario(IconMenuItem menu, Form formulario)
        {

            if (menuActivo != null)
            {
                menuActivo.BackColor = Color.FromArgb(240, 240, 240);
            }

            menu.BackColor = Color.FromArgb(150, 150, 150);
            menuActivo = menu;

            if (FormActivo != null)
            {
                FormActivo.Close();
            }

            FormActivo = formulario;
            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock = DockStyle.Fill;
            formulario.BringToFront();
            formulario.BackColor = Color.Cornsilk;

            contenedor.Controls.Add(formulario);
            formulario.Show();
        }


        private void menuAlumnos_Click(object sender, EventArgs e)
        {
            Abrir_Formulario(menuAlumnos, new frmAlumnos(empleadoActual));
        }

        private void menuAsistencias_Click(object sender, EventArgs e)
        {
            Abrir_Formulario(menuAsistencias, new frmAsistencias());
        }

        private void menuMembresias_Click(object sender, EventArgs e)
        {
            Abrir_Formulario(menuMembresias, new frmMembresias());
        }

        private void menuPlanes_Click(object sender, EventArgs e)
        {
            Abrir_Formulario(menuPlanes, new frmPlanes());
        }

        private void menuPagos_Click(object sender, EventArgs e)
        {
            Abrir_Formulario(menuPagos, new frmPagos());
        }

        private void menuReportes_Click(object sender, EventArgs e)
        {
            Abrir_Formulario(menuReportes, new frmReportes());
        }

        private void menuUsuarios_Click(object sender, EventArgs e)
        {
            Abrir_Formulario(menuUsuarios, new frmEmpleados());
        }

        private void menuSalir_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show(
                "¿Desea cerrar sesión?",
                "Confirmación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                Login login = new Login();
                login.Show();
                this.Close();
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void contenedor_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}