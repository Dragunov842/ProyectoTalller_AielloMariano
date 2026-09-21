using CapaEntidad;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class frmDetallePagoPlan : Form
    {
        private List<PlanEntrenamiento> listaPlanes =
            new List<PlanEntrenamiento>();

        private Alumno alumnoSeleccionado;

        public frmDetallePagoPlan(Alumno alumno)
        {
            InitializeComponent();

            cmbPlan.SelectedIndexChanged += cmbPlan_SelectedIndexChanged;

            alumnoSeleccionado = alumno;

            CargarPlanes();
        }

        private void CargarPlanes()
        {
            listaPlanes = new CN_PlanEntrenamiento().Listar();

            cmbPlan.Items.Clear();

            foreach (PlanEntrenamiento item in listaPlanes)
            {
                if (item.Estado)
                {
                    cmbPlan.Items.Add(item.Nombre);
                }
            }

            if (cmbPlan.Items.Count > 0)
            {
                cmbPlan.SelectedIndex = 0;
            }
        }

        private void cmbPlan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPlan.SelectedIndex < 0)
                return;

            PlanEntrenamiento planSeleccionado =
                listaPlanes.Find(x =>
                    x.Estado &&
                    x.Nombre == cmbPlan.SelectedItem.ToString());

            if (planSeleccionado != null)
            {
                txtPrecio.Text =
                    planSeleccionado.Precio.ToString("C2");

                txtDuracion.Text =
                    planSeleccionado.Duracion + " días";
            }
        }

        private void btnPagar_Click(object sender, EventArgs e)
        {
            if (cmbPlan.SelectedIndex < 0)
            {
                MessageBox.Show(
                    "Seleccione un plan.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            try
            {
                PlanEntrenamiento planSeleccionado =
                    listaPlanes.Find(x =>
                        x.Estado &&
                        x.Nombre == cmbPlan.SelectedItem.ToString());

                if (planSeleccionado == null)
                {
                    MessageBox.Show(
                        "No se pudo obtener el plan seleccionado.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    return;
                }

                CN_Pago objCN_Pago = new CN_Pago();

                objCN_Pago.RegistrarPagoPlan(
                    alumnoSeleccionado.idAlumno,
                    planSeleccionado.idPlan,
                    DateTime.Now
                );

                MessageBox.Show(
                    "Pago de plan registrado correctamente.",
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