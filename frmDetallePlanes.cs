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

            ConfigurarValidaciones();
        }

        public frmDetallePlanes(PlanEntrenamiento objPlan)
        {
            InitializeComponent();

            ConfigurarValidaciones();

            idPlan = objPlan.idPlan;

            txtNombre.Text = objPlan.Nombre;
            txtDescripcion.Text = objPlan.Descripcion;
            txtPrecio.Text = objPlan.Precio.ToString();
            txtDuracion.Text = objPlan.Duracion.ToString();
        }

        private void ConfigurarValidaciones()
        {
            txtNombre.KeyPress += SoloLetras_KeyPress;
            txtPrecio.KeyPress += Precio_KeyPress;
            txtDuracion.KeyPress += SoloNumeros_KeyPress;
        }

        private void SoloLetras_KeyPress(
            object sender,
            KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) &&
                !char.IsLetter(e.KeyChar) &&
                !char.IsWhiteSpace(e.KeyChar) &&
                e.KeyChar != '-' &&
                e.KeyChar != '\'')
            {
                e.Handled = true;
            }
        }

        private void SoloNumeros_KeyPress(
            object sender,
            KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) &&
                !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void Precio_KeyPress(
            object sender,
            KeyPressEventArgs e)
        {
            TextBox caja = (TextBox)sender;

            if (char.IsControl(e.KeyChar) ||
                char.IsDigit(e.KeyChar))
                return;

            if (e.KeyChar == '.' || e.KeyChar == ',')
            {
                if (caja.Text.Contains('.') ||
                    caja.Text.Contains(','))
                {
                    e.Handled = true;
                }

                return;
            }

            e.Handled = true;
        }

        private bool IntentarObtenerPrecio(out decimal precio)
        {
            string texto = txtPrecio.Text.Trim().Replace(',', '.');

            return decimal.TryParse(
                texto,
                System.Globalization.NumberStyles.AllowDecimalPoint,
                System.Globalization.CultureInfo.InvariantCulture,
                out precio
            ) && precio > 0;
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Debe ingresar el nombre.");
                txtNombre.Focus();
                return false;
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(
                txtNombre.Text.Trim(),
                @"^[\p{L}]+(?:[ '-][\p{L}]+)*$"))
            {
                MessageBox.Show("El nombre contiene caracteres no válidos.");
                txtNombre.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                MessageBox.Show("Debe ingresar una descripción.");
                txtDescripcion.Focus();
                return false;
            }

            if (!IntentarObtenerPrecio(out _))
            {
                MessageBox.Show("Ingrese un precio válido mayor que cero.");
                txtPrecio.Focus();
                return false;
            }

            if (!int.TryParse(txtDuracion.Text, out int duracion) ||
                duracion <= 0)
            {
                MessageBox.Show("La duración debe ser un número entero mayor que cero.");
                txtDuracion.Focus();
                return false;
            }

            return true;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidarCampos())
                    return;

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
