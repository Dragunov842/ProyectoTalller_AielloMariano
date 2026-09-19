using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using CapaEntidad;
using CapaNegocio;

namespace CapaPresentacion
{
    public partial class frmPlanes : Form
    {

        private List<PlanEntrenamiento> listaPlan = new List<PlanEntrenamiento>();

        public frmPlanes()
        {
            InitializeComponent();

            Listar();

        }

        private void Listar()
        {
            listaPlan = new CN_PlanEntrenamiento().Listar();

            dataGridPlan.Rows.Clear();

            foreach (PlanEntrenamiento item in listaPlan)
            {
                dataGridPlan.Rows.Add(
                    item.idPlan,
                    item.Nombre,
                    item.Descripcion,
                    item.Precio,
                    item.Duracion,
                    item.Estado ? "Activo" : "Inactivo"
                );
            }
        }

        private void Buscar(string texto)
        {
            texto = texto.Trim().ToLower();

            dataGridPlan.Rows.Clear();

            foreach (PlanEntrenamiento item in listaPlan)
            {
                if (
                    item.Nombre.ToLower().Contains(texto) ||
                    item.Descripcion.ToLower().Contains(texto)
                )
                {
                    dataGridPlan.Rows.Add(
                        item.idPlan,
                        item.Nombre,
                        item.Descripcion,
                        item.Precio,
                        item.Duracion,
                        item.Estado ? "Activo" : "Inactivo"
                    );
                }
            }
        }

        private void añadir_Click(object sender, EventArgs e)
        {
            using (frmDetallePlanes formulario = new frmDetallePlanes())
            {
                if (formulario.ShowDialog() == DialogResult.OK)
                {
                    Listar();
                }
            }
        }

        private void editar_Click(object sender, EventArgs e)
        {
            if (dataGridPlan.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Seleccione un plan.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            int indice = dataGridPlan.SelectedRows[0].Index;

            PlanEntrenamiento planSeleccionado = listaPlan[indice];

            using (frmDetallePlanes formulario = new frmDetallePlanes(planSeleccionado))
            {
                if (formulario.ShowDialog() == DialogResult.OK)
                {
                    Listar();
                }
            }
        }

        private void eliminar_Click(object sender, EventArgs e)
        {
            if (dataGridPlan.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Seleccione un plan.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            int indice = dataGridPlan.SelectedRows[0].Index;
            PlanEntrenamiento planSeleccionado = listaPlan[indice];

            DialogResult resultado = MessageBox.Show(
                "¿Está seguro de eliminar el plan seleccionado?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resultado == DialogResult.No)
            {
                return;
            }

            bool respuesta = new CN_PlanEntrenamiento().Eliminar(
                planSeleccionado.idPlan);

            if (respuesta)
            {
                MessageBox.Show(
                    "Plan eliminado correctamente.",
                    "Eliminación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                Listar();
            }
        }

        private void activar_Click(object sender, EventArgs e)
        {
            if (dataGridPlan.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Seleccione un plan.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            int indice = dataGridPlan.SelectedRows[0].Index;
            PlanEntrenamiento planSeleccionado = listaPlan[indice];

            DialogResult resultado = MessageBox.Show(
                "¿Está seguro de activar el plan seleccionado?",
                "Confirmar activación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resultado == DialogResult.No)
            {
                return;
            }

            bool respuesta = new CN_PlanEntrenamiento().Activar(
                planSeleccionado.idPlan);

            if (respuesta)
            {
                MessageBox.Show(
                    "Plan activado correctamente.",
                    "Activación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                Listar();
            }
        }

        private void buscar_Click(object sender, EventArgs e)
        {
            Buscar(txtBuscar.Text);
        }
    }

}
