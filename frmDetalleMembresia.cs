using CapaEntidad;
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
    public partial class frmDetalleMembresia : Form
    {

        private int idMembresia = 0;

        public frmDetalleMembresia()
        {
            InitializeComponent();
        }

        public frmDetalleMembresia(Membresia objMembresia)
        {
            InitializeComponent();

            idMembresia = objMembresia.idMembresia;

            txtNombre.Text = objMembresia.Nombre;
            txtDescripcion.Text = objMembresia.Descripcion;
            txtPrecio.Text = objMembresia.Precio.ToString();
            txtDuracion.Text = objMembresia.Duracion.ToString();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Membresia objMembresia = new Membresia()
                {
                    idMembresia = idMembresia,
                    Nombre = txtNombre.Text,
                    Descripcion = txtDescripcion.Text,
                    Precio = Convert.ToDecimal(txtPrecio.Text),
                    Duracion = Convert.ToInt32(txtDuracion.Text)
                };

                if (idMembresia == 0)
                {
                    new CN_Membresia().Registrar(objMembresia);

                    MessageBox.Show(
                        "Membresía registrada correctamente.",
                        "Registro",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else
                {
                    bool respuesta = new CN_Membresia().Editar(objMembresia);

                    if (respuesta)
                    {
                        MessageBox.Show(
                            "Membresía editada correctamente.",
                            "Edición",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
