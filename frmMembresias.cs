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
    public partial class frmMembresias : Form
    {
        private List<Membresia> listaMembresias = new List<Membresia>();

        public frmMembresias()
        {
            InitializeComponent();

            Listar();

        }

        private void Listar()
        {
            listaMembresias = new CN_Membresia().Listar();

            dataGridMembresias.Rows.Clear();

            foreach (Membresia item in listaMembresias)
            {
                dataGridMembresias.Rows.Add(
                    item.idMembresia,
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

            dataGridMembresias.Rows.Clear();

            foreach (Membresia item in listaMembresias)
            {
                if (
                    item.Nombre.ToLower().Contains(texto) ||
                    item.Descripcion.ToLower().Contains(texto)
                )
                {
                    dataGridMembresias.Rows.Add(
                        item.idMembresia,
                        item.Nombre,
                        item.Descripcion,
                        item.Precio,
                        item.Duracion,
                        item.Estado ? "Activa" : "Inactiva"
                    );
                }
            }
        }

        private void añadir_Click(object sender, EventArgs e)
        {
            using (frmDetalleMembresia formulario = new frmDetalleMembresia())
            {
                if (formulario.ShowDialog() == DialogResult.OK)
                {
                    Listar();
                }
            }
        }

        private void editar_Click(object sender, EventArgs e)
        {
            if (dataGridMembresias.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Seleccione una membresía.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            int indice = dataGridMembresias.SelectedRows[0].Index;

            Membresia membresiaSeleccionada = listaMembresias[indice];

            using (frmDetalleMembresia formulario = new frmDetalleMembresia(membresiaSeleccionada))
            {
                if (formulario.ShowDialog() == DialogResult.OK)
                {
                    Listar();
                }
            }
        }

        private void eliminar_Click(object sender, EventArgs e)
        {
            if (dataGridMembresias.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Seleccione una membresía.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            int indice = dataGridMembresias.SelectedRows[0].Index;
            Membresia membresiaSeleccionada = listaMembresias[indice];

            DialogResult resultado = MessageBox.Show(
                "¿Está seguro de eliminar la membresía seleccionada?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resultado == DialogResult.No)
            {
                return;
            }

            bool respuesta = new CN_Membresia().Eliminar(
                membresiaSeleccionada.idMembresia);

            if (respuesta)
            {
                MessageBox.Show(
                    "Membresía eliminada correctamente.",
                    "Eliminación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                Listar();
            }
        }

        private void activar_Click(object sender, EventArgs e)
        {
            if (dataGridMembresias.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Seleccione un plan.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            int indice = dataGridMembresias.SelectedRows[0].Index;
            Membresia membresiaSeleccionada = listaMembresias[indice];

            DialogResult resultado = MessageBox.Show(
                "¿Está seguro de activar la membresía seleccionada?",
                "Confirmar activación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resultado == DialogResult.No)
            {
                return;
            }

            bool respuesta = new CN_Membresia().Activar(
                membresiaSeleccionada.idMembresia);

            if (respuesta)
            {
                MessageBox.Show(
                    "Membresía activada correctamente.",
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
