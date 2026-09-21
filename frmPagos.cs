using CapaEntidad;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class frmPagos : Form
    {
        private CN_AsignacionMembresia objCNMembresia = new CN_AsignacionMembresia();

        private List<Alumno> listaAlumno = new List<Alumno>();

        public frmPagos()
        {
            InitializeComponent();

            Listar();
        }

        private void Listar()
        {
            listaAlumno = new CN_Alumno().Listar();

            dataGridPago.Rows.Clear();

            CN_AsignacionMembresia objCNMembresia =
                new CN_AsignacionMembresia();

            CN_AsignacionPlan objCNPlan =
                new CN_AsignacionPlan();

            foreach (Alumno item in listaAlumno)
            {
                // MEMBRESÍA
                var membresia = objCNMembresia.BuscarActiva(item.idAlumno);

                string nombreMembresia = "Sin membresía";

                if (membresia != null)
                {
                    nombreMembresia = membresia.oMembresia.Nombre;
                }

                // PLANES
                var planes = objCNPlan.ListarActivosPorAlumno(item.idAlumno);

                string nombresPlanes = "Sin planes";

                if (planes.Count > 0)
                {
                    nombresPlanes = string.Join(
                        ", ",
                        planes.ConvertAll(x => x.oPlan.Nombre)
                    );
                }

                dataGridPago.Rows.Add(
                    item.idAlumno,
                    item.oPersona.Nombre,
                    item.oPersona.Apellido,
                    item.oPersona.DNI,
                    item.oPersona.Telefono,
                    item.oPersona.Email,
                    item.Fecha_Alta.ToString("dd/MM/yyyy"),
                    item.Estado ? "Activo" : "Inactivo",
                    nombreMembresia,
                    nombresPlanes
                );
            }
        }

        private void Buscar(string texto)
        {
            texto = texto.Trim().ToLower();

            dataGridPago.Rows.Clear();

            foreach (Alumno item in listaAlumno)
            {
                if (
                    item.oPersona.Nombre.ToLower().Contains(texto) ||
                    item.oPersona.Apellido.ToLower().Contains(texto) ||
                    item.oPersona.DNI.ToLower().Contains(texto)
                )
                {
                    dataGridPago.Rows.Add(
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

        private void pagarMembresia_Click(object sender, EventArgs e)
        {
            if (dataGridPago.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Seleccione un alumno.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            int indice = dataGridPago.SelectedRows[0].Index;

            Alumno alumnoSeleccionado = listaAlumno[indice];

            using (frmDetallePagoMembresia formulario =
                   new frmDetallePagoMembresia(alumnoSeleccionado))
            {
                formulario.ShowDialog();
            }
        }

        private void buscar_Click(object sender, EventArgs e)
        {
            Buscar(txtBuscar.Text);
        }

        private void pagarPlan_Click(object sender, EventArgs e)
        {
            if (dataGridPago.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Seleccione un alumno.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            int indice = dataGridPago.SelectedRows[0].Index;

            Alumno alumnoSeleccionado = listaAlumno[indice];

            using (frmDetallePagoPlan formulario =
                   new frmDetallePagoPlan(alumnoSeleccionado))
            {
                formulario.ShowDialog();

                Listar();
            }
        }
    }
}