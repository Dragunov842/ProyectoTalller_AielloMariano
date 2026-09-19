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
    public partial class frmEmpleados : Form
    {

        private List<Empleado> listaEmpleado = new List<Empleado>();

        public frmEmpleados()
        {
            InitializeComponent();

            Listar();
        }

        private void Listar()
        {
            listaEmpleado = new CN_Empleado().Listar();

            dataGridEmpleado.Rows.Clear();

            foreach (Empleado item in listaEmpleado)
            {
                dataGridEmpleado.Rows.Add(
                    item.idEmpleado,
                    item.oPersona.Nombre,
                    item.oPersona.Apellido,
                    item.oPersona.DNI,
                    item.oPersona.Telefono,
                    item.oPersona.Email,
                    item.Usuario,
                    item.oRol.Nombre,
                    item.Fecha_Alta.ToString("dd/MM/yyyy"),
                    item.Estado ? "Activo" : "Inactivo"
                );
            }
        }

        private void Buscar(string texto)
        {
            texto = texto.Trim().ToLower();

            dataGridEmpleado.Rows.Clear();

            foreach (Empleado item in listaEmpleado)
            {
                if (
                    item.oPersona.Nombre.ToLower().Contains(texto) ||
                    item.oPersona.Apellido.ToLower().Contains(texto) ||
                    item.oPersona.DNI.ToLower().Contains(texto) ||
                    item.Usuario.ToLower().Contains(texto)
                )
                {
                    dataGridEmpleado.Rows.Add(
                        item.idEmpleado,
                        item.oPersona.Nombre,
                        item.oPersona.Apellido,
                        item.oPersona.DNI,
                        item.oPersona.Telefono,
                        item.oPersona.Email,
                        item.Usuario,
                        item.oRol.Nombre,
                        item.Fecha_Alta.ToString("dd/MM/yyyy"),
                        item.Estado ? "Activo" : "Inactivo"
                    );
                }
            }
        }

        private void añadir_Click(object sender, EventArgs e)
        {
            using (frmDetalleEmpleado formulario = new frmDetalleEmpleado())
            {
                if (formulario.ShowDialog() == DialogResult.OK)
                {
                    Listar();
                }
            }
        }

        private void editar_Click(object sender, EventArgs e)
        {

            if (dataGridEmpleado.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Seleccione un empleado.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            int indice = dataGridEmpleado.SelectedRows[0].Index;

            Empleado empleadoSeleccionado = listaEmpleado[indice];

            using (frmDetalleEmpleado formulario = new frmDetalleEmpleado(empleadoSeleccionado))
            {
                if (formulario.ShowDialog() == DialogResult.OK)
                {
                    Listar();
                }
            }
        }

        private void eliminar_Click(object sender, EventArgs e)
        {
            if (dataGridEmpleado.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Seleccione un empleado.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            int indice = dataGridEmpleado.SelectedRows[0].Index;

            Empleado empleadoSeleccionado = listaEmpleado[indice];

            DialogResult resultado = MessageBox.Show(
                "¿Está seguro de eliminar al empleado seleccionado?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resultado == DialogResult.No)
            {
                return;
            }

            bool respuesta = new CN_Empleado().Eliminar(
                empleadoSeleccionado.idEmpleado);

            if (respuesta)
            {
                MessageBox.Show(
                    "Empleado eliminado correctamente.",
                    "Eliminación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                Listar();
            }
        }

        private void activar_Click(object sender, EventArgs e)
        {
            if (dataGridEmpleado.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Seleccione un empleado.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            int indice = dataGridEmpleado.SelectedRows[0].Index;

            Empleado empleadoSeleccionado = listaEmpleado[indice];

            DialogResult resultado = MessageBox.Show(
                "¿Está seguro de activar al empleado seleccionado?",
                "Confirmar activación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resultado == DialogResult.No)
            {
                return;
            }

            bool respuesta = new CN_Empleado().Activar(
                empleadoSeleccionado.idEmpleado);

            if (respuesta)
            {
                MessageBox.Show(
                    "Empleado activado correctamente.",
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