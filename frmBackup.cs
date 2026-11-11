using CapaDatos;
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
    public partial class frmBackup : Form
    {
        public frmBackup()
        {
            InitializeComponent();

            ConfigurarDiseñoResponsive();

            this.subirBackup.ToolTipText = "Crear una copia de seguridad de la base de datos actual del gimnasio." +
                Environment.NewLine +
            "Nota: Esta operacion debe realizarse conjuntamente por dos usuarios con el rol de administrador";
            this.copiarBackup.ToolTipText = "Restaurar la base de datos del gimnasio a partir de una copia de seguridad valida." +
                Environment.NewLine +
            "Nota: Esta operacion debe realizarse conjuntamente por dos usuarios con el rol de administrador";

        }


        private void ConfigurarDiseñoResponsive()
        {
            // Menú superior
            menuStrip1.Dock = DockStyle.Top;
            menuStrip1.AutoSize = false;
            menuStrip1.Height = 71;
            menuStrip1.GripStyle = ToolStripGripStyle.Hidden;
            menuStrip1.CanOverflow = true;
            menuStrip1.LayoutStyle =
                ToolStripLayoutStyle.HorizontalStackWithOverflow;

            foreach (ToolStripItem item in menuStrip1.Items)
            {
                item.Overflow = ToolStripItemOverflow.AsNeeded;
            }

            // Contenedor principal
            TableLayoutPanel contenido = new TableLayoutPanel();

            contenido.Dock = DockStyle.Fill;
            contenido.ColumnCount = 1;
            contenido.RowCount = 3;
            contenido.Padding = new Padding(25);
            contenido.BackColor = this.BackColor;

            contenido.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 100F));

            contenido.RowStyles.Add(
                new RowStyle(SizeType.Percent, 18F));

            contenido.RowStyles.Add(
                new RowStyle(SizeType.Percent, 32F));

            contenido.RowStyles.Add(
                new RowStyle(SizeType.Percent, 50F));

            // Título
            label1.Dock = DockStyle.Fill;
            label1.TextAlign = ContentAlignment.MiddleCenter;

            // Advertencia
            label3.Dock = DockStyle.Fill;
            label3.AutoSize = false;
            label3.TextAlign = ContentAlignment.MiddleCenter;
            label3.Padding = new Padding(15);

            // Contenedor de autorizaciones
            TableLayoutPanel autorizaciones = new TableLayoutPanel();

            autorizaciones.Dock = DockStyle.Fill;
            autorizaciones.ColumnCount = 2;
            autorizaciones.RowCount = 1;
            autorizaciones.Padding = new Padding(10);

            autorizaciones.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 50F));

            autorizaciones.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 50F));

            // Grupo del administrador
            TableLayoutPanel grupoAdministrador =
                CrearGrupoAutorizacion(
                    label4,
                    txtUsuario1,
                    Username,
                    txtContraseña1
                );

            // Grupo del supervisor
            TableLayoutPanel grupoSupervisor =
                CrearGrupoAutorizacion(
                    label5,
                    txtUsuario2,
                    label2,
                    txtContraseña2
                );

            autorizaciones.Controls.Add(grupoAdministrador, 0, 0);
            autorizaciones.Controls.Add(grupoSupervisor, 1, 0);

            // Distribución general
            contenido.Controls.Add(label1, 0, 0);
            contenido.Controls.Add(label3, 0, 1);
            contenido.Controls.Add(autorizaciones, 0, 2);

            // Incorporar al formulario
            this.Controls.Add(contenido);
            this.Controls.Add(menuStrip1);

            menuStrip1.BringToFront();
        }

        private TableLayoutPanel CrearGrupoAutorizacion(
            Label etiquetaUsuario,
            TextBox campoUsuario,
            Label etiquetaContraseña,
            TextBox campoContraseña)
        {
            TableLayoutPanel grupo = new TableLayoutPanel();

            grupo.Dock = DockStyle.Fill;
            grupo.ColumnCount = 1;
            grupo.RowCount = 4;
            grupo.Padding = new Padding(15);

            grupo.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 100F));

            grupo.RowStyles.Add(
                new RowStyle(SizeType.Percent, 25F));

            grupo.RowStyles.Add(
                new RowStyle(SizeType.Percent, 25F));

            grupo.RowStyles.Add(
                new RowStyle(SizeType.Percent, 25F));

            grupo.RowStyles.Add(
                new RowStyle(SizeType.Percent, 25F));

            // Etiquetas
            etiquetaUsuario.Dock = DockStyle.Fill;
            etiquetaUsuario.TextAlign = ContentAlignment.BottomLeft;
            etiquetaUsuario.AutoSize = false;

            etiquetaContraseña.Dock = DockStyle.Fill;
            etiquetaContraseña.TextAlign = ContentAlignment.BottomLeft;
            etiquetaContraseña.AutoSize = false;

            // Campos de texto
            campoUsuario.Dock = DockStyle.Fill;
            campoUsuario.Margin = new Padding(3, 5, 3, 12);

            campoContraseña.Dock = DockStyle.Fill;
            campoContraseña.Margin = new Padding(3, 5, 3, 12);

            grupo.Controls.Add(etiquetaUsuario, 0, 0);
            grupo.Controls.Add(campoUsuario, 0, 1);
            grupo.Controls.Add(etiquetaContraseña, 0, 2);
            grupo.Controls.Add(campoContraseña, 0, 3);

            return grupo;
        }



        private bool ValidarDobleAutorizacion()
        {
            CN_Empleado negocioEmpleado = new CN_Empleado();

            bool autorizado = negocioEmpleado.ValidarDobleAutorizacion(
                txtUsuario1.Text,
                txtContraseña1.Text,
                txtUsuario2.Text,
                txtContraseña2.Text
            );

            if (!autorizado)
            {
                MessageBox.Show(
                    "La autorización fue rechazada.\n\n" +
                    "Verificá que ambos usuarios y contraseñas sean correctos " +
                    "y que correspondan a dos administradores activos diferentes.",
                    "Autorización denegada",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return false;
            }

            return true;
        }


        private void subirBackup_Click(object sender, EventArgs e)
        {
            if (!ValidarDobleAutorizacion())
                return;

            using (SaveFileDialog guardar = new SaveFileDialog())
            {
                guardar.Title = "Guardar copia de seguridad";
                guardar.Filter = "Copia de seguridad del gimnasio (*.osb)|*.osb|Backup SQL Server (*.bak)|*.bak";
                guardar.DefaultExt = "osb";
                guardar.AddExtension = true;
                guardar.FileName = "Backup_Gimnasio_" +
                    DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".osb";
                guardar.OverwritePrompt = true;

                if (guardar.ShowDialog() != DialogResult.OK)
                    return;

                try
                {
                    CN_Backup negocioBackup = new CN_Backup();
                    negocioBackup.CrearBackup(guardar.FileName);

                    MessageBox.Show(
                        "La copia de seguridad se creó correctamente.",
                        "Copia de seguridad",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "No se pudo crear la copia de seguridad.\n\n" + ex.Message,
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        private void copiarBackup_Click(object sender, EventArgs e)
        {
            if (!ValidarDobleAutorizacion())
                return;

            using (OpenFileDialog abrir = new OpenFileDialog())
            {
                abrir.Title = "Seleccionar copia de seguridad";
                abrir.Filter = "Copias de seguridad (*.osb;*.bak)|*.osb;*.bak|Todos los archivos (*.*)|*.*";
                abrir.CheckFileExists = true;
                abrir.Multiselect = false;

                if (abrir.ShowDialog() != DialogResult.OK)
                    return;

                DialogResult confirmacion = MessageBox.Show(
                    "La restauración reemplazará los datos actuales del gimnasio " +
                    "por los contenidos en la copia seleccionada.\n\n" +
                    "¿Deseás continuar?",
                    "Confirmar restauración",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirmacion != DialogResult.Yes)
                    return;

                try
                {
                    CN_Backup negocioBackup = new CN_Backup();
                    negocioBackup.RestaurarBackup(abrir.FileName);

                    MessageBox.Show(
                        "La copia de seguridad se restauró correctamente.",
                        "Restauración completada",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }

                catch (Exception ex)
                {
                    MessageBox.Show(
                        "El archivo seleccionado no es una copia de seguridad " +
                        "válida o está dañado.\n\n" +
                        "Verificá que hayas seleccionado un backup correcto " +
                        "del sistema.",
                        "Archivo de backup inválido",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
        }


        private void Username_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
