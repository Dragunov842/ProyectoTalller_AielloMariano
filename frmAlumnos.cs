using CapaDatos;
using CapaEntidad;
using CapaNegocio;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class frmAlumnos : Form
    {

        private List<Alumno> listaAlumno = new List<Alumno>();

        private Empleado empleadoLogueado;

        public frmAlumnos(Empleado empleado)
        {
            InitializeComponent();

            empleadoLogueado = empleado;

            ConfigurarPermisos();

            Listar();
        }

        private void ConfigurarPermisos()
        {
            if (empleadoLogueado.oRol.idRol == 3)
            {
                añadir.Visible = false;
                editar.Visible = false;
                eliminar.Visible = false;
                activar.Visible = false;
            }
        }

        private void Listar()
        {
            listaAlumno = new CN_Alumno().Listar();

            dataGridAlumno.Rows.Clear();

            foreach (Alumno item in listaAlumno)
            {
                dataGridAlumno.Rows.Add(
                    item.idAlumno,
                    item.oPersona.Nombre,
                    item.oPersona.Apellido,
                    item.oPersona.DNI,
                    item.oPersona.Telefono,
                    item.oPersona.Email,
                    item.Fecha_Alta.ToString("dd/MM/yyyy"),
                    item.Estado ? "Activo" : "Inactivo"
                );
            }
        }

        private void Buscar(string texto)
        {
            texto = texto.Trim().ToLower();

            dataGridAlumno.Rows.Clear();

            foreach (Alumno item in listaAlumno)
            {
                if (
                    item.oPersona.Nombre.ToLower().Contains(texto) ||
                    item.oPersona.Apellido.ToLower().Contains(texto) ||
                    item.oPersona.DNI.ToLower().Contains(texto)
                )
                {
                    dataGridAlumno.Rows.Add(
                        item.idAlumno,
                        item.oPersona.Nombre,
                        item.oPersona.Apellido,
                        item.oPersona.DNI,
                        item.oPersona.Telefono,
                        item.oPersona.Email,
                        item.Fecha_Alta.ToString("dd/MM/yyyy"),
                        item.Estado ? "Activo" : "Inactivo"
                    );
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void añadir_Click(object sender, EventArgs e)
        {
            using (frmDetalleAlumno formulario = new frmDetalleAlumno())
            {
                if (formulario.ShowDialog() == DialogResult.OK)
                {
                    Listar();
                }
            }
        }



        private void editar_Click(object sender, EventArgs e)
        {

            if (dataGridAlumno.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Seleccione un alumno.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            int indice = dataGridAlumno.SelectedRows[0].Index;

            Alumno alumnoSeleccionado = listaAlumno[indice];

            using (frmDetalleAlumno formulario = new frmDetalleAlumno(alumnoSeleccionado))
            {
                if (formulario.ShowDialog() == DialogResult.OK)
                {
                    Listar();
                }
            }
        }



        private void eliminar_Click(object sender, EventArgs e)
        {
            if (dataGridAlumno.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Seleccione un alumno.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            int idAlumno = Convert.ToInt32(
                dataGridAlumno.SelectedRows[0].Cells["colIdAlumno"].Value
            );

            DialogResult resultado = MessageBox.Show(
                "¿Está seguro de eliminar este alumno?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                bool respuesta = new CN_Alumno().Eliminar(idAlumno);

                if (respuesta)
                {
                    MessageBox.Show(
                        "Alumno eliminado correctamente.",
                        "Eliminación",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    Listar();
                }
            }
        }



        private void activar_Click(object sender, EventArgs e)
        {
            if (dataGridAlumno.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Seleccione un alumno.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            int idAlumno = Convert.ToInt32(
                dataGridAlumno.SelectedRows[0].Cells["colIdAlumno"].Value
            );

            DialogResult resultado = MessageBox.Show(
                "¿Está seguro de activar este alumno?",
                "Confirmar activación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                bool respuesta = new CN_Alumno().Activar(idAlumno);

                if (respuesta)
                {
                    MessageBox.Show(
                        "Alumno activado correctamente.",
                        "Activación",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    Listar();
                }
            }
        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void buscar_Click(object sender, EventArgs e)
        {
            Buscar(txtBuscar.Text);
        }
    }
}
