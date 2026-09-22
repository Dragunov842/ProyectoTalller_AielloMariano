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
    public partial class frmDetallePlanes : Form
    {

        private int idPlan = 0;

        public frmDetallePlanes()
        {
            InitializeComponent();
        }

        public frmDetallePlanes(PlanEntrenamiento objPlan)
        {
            InitializeComponent();

            idPlan = objPlan.idPlan;

            txtNombre.Text = objPlan.Nombre;
            txtDescripcion.Text = objPlan.Descripcion;
            txtPrecio.Text = objPlan.Precio.ToString();
            txtDuracion.Text = objPlan.Duracion.ToString();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                PlanEntrenamiento objPlan = new PlanEntrenamiento()
                {
                    idPlan = idPlan,
                    Nombre = txtNombre.Text,
                    Descripcion = txtDescripcion.Text,
                    Precio = Convert.ToDecimal(txtPrecio.Text),
                    Duracion = Convert.ToInt32(txtDuracion.Text)
                };

                if (idPlan == 0)
                {
                    new CN_PlanEntrenamiento().Registrar(objPlan);

                    MessageBox.Show(
                        "Plan registrado correctamente.",
                        "Registro",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else
                {
                    bool respuesta = new CN_PlanEntrenamiento().Editar(objPlan);

                    if (respuesta)
                    {
                        MessageBox.Show(
                            "Plan editado correctamente.",
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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

}
