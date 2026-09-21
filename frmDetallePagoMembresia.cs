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
    public partial class frmDetallePagoMembresia : Form
    {
        private List<Membresia> listaMembresias = new List<Membresia>();
        private Alumno alumnoSeleccionado;

        public frmDetallePagoMembresia(Alumno alumno)
        {
            InitializeComponent();

            cmbMembresia.SelectedIndexChanged += cmbMembresia_SelectedIndexChanged;

            alumnoSeleccionado = alumno;

            CargarMembresias();
        }

        private void CargarMembresias()
        {
            listaMembresias = new CN_Membresia().Listar();

            cmbMembresia.Items.Clear();

            foreach (Membresia item in listaMembresias)
            {
                if (item.Estado)
                {
                    cmbMembresia.Items.Add(item.Nombre);
                }
            }

            if (cmbMembresia.Items.Count > 0)
            {
                cmbMembresia.SelectedIndex = 0;
            }
        }

        private void cmbMembresia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMembresia.SelectedIndex < 0)
                return;

            Membresia membresiaSeleccionada =
                listaMembresias.Find(x =>
                    x.Estado &&
                    x.Nombre == cmbMembresia.SelectedItem.ToString());

            if (membresiaSeleccionada != null)
            {
                txtPrecio.Text = membresiaSeleccionada.Precio.ToString("C2");
                txtDuracion.Text = membresiaSeleccionada.Duracion + " días";
            }
        }

        private void btnPagar_Click(object sender, EventArgs e)
        {
            if (cmbMembresia.SelectedIndex < 0)
            {
                MessageBox.Show(
                    "Seleccione una membresía.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            try
            {
                Membresia membresiaSeleccionada =
                    listaMembresias.Find(x =>
                        x.Estado &&
                        x.Nombre == cmbMembresia.SelectedItem.ToString());

                if (membresiaSeleccionada == null)
                {
                    MessageBox.Show(
                        "No se pudo obtener la membresía seleccionada.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    return;
                }

                CN_Pago objCN_Pago = new CN_Pago();

                objCN_Pago.RegistrarPagoMembresia(
                    alumnoSeleccionado.idAlumno,
                    membresiaSeleccionada.idMembresia,
                    DateTime.Now
                );

                MessageBox.Show(
                    "Pago de membresía registrado correctamente.",
                    "Éxito",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

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
