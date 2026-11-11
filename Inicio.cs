
using System;
using System.Collections.Generic;
using System.Drawing;
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

            // Aprovechar el espacio disponible de la pantalla.
            this.WindowState = FormWindowState.Maximized;

            // Mantener el encabezado adaptable.
            panel1.Resize += panel1_Resize;

            this.toolTipMensaje.SetToolTip(
                this.lblusuario,
                "Usuario: " + empleadoActual.Usuario +
                "\nRol: " + empleadoActual.oRol.Nombre
            );

            this.menuSalir.ToolTipText = "Cerrar sesión";

            this.menuBackup.ToolTipText =
                "Realizar copia de seguridad o restaurar la base de datos" +
                Environment.NewLine +
                "Nota: Esta operacion debe realizarse conjuntamente por dos usuarios con el rol de administrador";

            this.menuAlumnos.ToolTipText = "Administrar alumnos";
            this.menuMembresias.ToolTipText = "Administrar membresías";
            this.menuPlanes.ToolTipText = "Administrar planes";
            this.menuPagos.ToolTipText = "Administrar pagos";
            this.menuReportes.ToolTipText = "Generar reportes";
            this.menuUsuarios.ToolTipText = "Administrar empleados";
        }

        private bool TienePermiso(List<Permiso> listaPermisos, string nombreMenu)
        {
            return listaPermisos.Exists(x => x.NombreMenu == nombreMenu);
        }

        private void Inicio_Load(object sender, EventArgs e)
        {
            List<Permiso> listaPermisos =
                new CN_Permiso().Listar(empleadoActual.oRol.idRol);

            lblusuario.Text = empleadoActual.Usuario;

            // Encabezado.
            panel1.Dock = DockStyle.Top;
            panel1.Height = 60;

            // Menú adaptable.
            menuStrip1.Dock = DockStyle.Top;
            menuStrip1.AutoSize = false;
            menuStrip1.Height = 58;
            menuStrip1.GripStyle = ToolStripGripStyle.Hidden;
            menuStrip1.CanOverflow = true;
            menuStrip1.LayoutStyle =
                ToolStripLayoutStyle.HorizontalStackWithOverflow;

            menuStrip1.ImageScalingSize = new Size(24, 24);

            // Ajustar los elementos del menú.
            foreach (ToolStripItem item in menuStrip1.Items)
            {
                item.AutoSize = true;
                item.Overflow = ToolStripItemOverflow.AsNeeded;
                item.Padding = new Padding(6, 3, 6, 3);
                item.ImageScaling = ToolStripItemImageScaling.SizeToFit;

                if (item is IconMenuItem iconItem)
                {
                    iconItem.IconSize = 24;
                    iconItem.TextImageRelation =
                        TextImageRelation.ImageAboveText;
                }
            }

            // Área principal.
            contenedor.Dock = DockStyle.Fill;

            // Permisos.
            menuAlumnos.Visible =
                TienePermiso(listaPermisos, "Alumnos");

            menuMembresias.Visible =
                TienePermiso(listaPermisos, "Membresias");

            menuPlanes.Visible =
                TienePermiso(listaPermisos, "Planes");

            menuPagos.Visible =
                TienePermiso(listaPermisos, "Pagos");

            menuReportes.Visible =
                TienePermiso(listaPermisos, "Reportes");

            menuUsuarios.Visible =
                TienePermiso(listaPermisos, "Usuarios");

            menuBackup.Visible =
                TienePermiso(listaPermisos, "Backup");

            // Posicionar los elementos del encabezado.
            AjustarEncabezado();
        }

        private void panel1_Resize(object sender, EventArgs e)
        {
            AjustarEncabezado();
        }

        private void AjustarEncabezado()
        {
            if (panel1 == null ||
                label1 == null ||
                lblusuario == null ||
                Usuario == null)
                return;

            // Título centrado.
            label1.Left =
                (panel1.ClientSize.Width - label1.Width) / 2;

            label1.Top =
                (panel1.ClientSize.Height - label1.Height) / 2;

            // Usuario alineado a la derecha.
            lblusuario.Left =
                panel1.ClientSize.Width - lblusuario.Width - 15;

            lblusuario.Top =
                (panel1.ClientSize.Height - lblusuario.Height) / 2;

            Usuario.Left =
                lblusuario.Left - Usuario.Width - 5;

            Usuario.Top =
                (panel1.ClientSize.Height - Usuario.Height) / 2;
        }

        private void Abrir_Formulario(
            IconMenuItem menu,
            Form formulario)
        {
            if (menuActivo != null)
            {
                menuActivo.BackColor =
                    Color.FromArgb(240, 240, 240);
            }

            menu.BackColor = Color.FromArgb(150, 150, 150);
            menuActivo = menu;

            if (FormActivo != null)
            {
                FormActivo.Close();
                FormActivo.Dispose();
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
            Abrir_Formulario(
                menuAlumnos,
                new frmAlumnos(empleadoActual)
            );
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
                MessageBoxIcon.Question
            );

            if (resultado == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void menuBackup_Click(object sender, EventArgs e)
        {
            Abrir_Formulario(menuBackup, new frmBackup());
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

        private void menuManualUsuario_Click(object sender, EventArgs e)
        {
        }
    }
}
