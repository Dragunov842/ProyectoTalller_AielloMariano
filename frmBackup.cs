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
        }


        private bool ValidarCamposContrasena()
        {
            if (string.IsNullOrWhiteSpace(txtContraseña1.Text) ||
                string.IsNullOrWhiteSpace(txtContraseña2.Text))
            {
                MessageBox.Show(
                    "Debés ingresar ambas contraseñas antes de continuar.",
                    "Autorización requerida",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return false;
            }

            return true;
        }


        private void subirBackup_Click(object sender, EventArgs e)
        {
            if (!ValidarCamposContrasena())
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
            if (!ValidarCamposContrasena())
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
    }
}
