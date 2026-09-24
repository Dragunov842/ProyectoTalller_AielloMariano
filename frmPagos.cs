using CapaEntidad;
using CapaNegocio;
using System.Linq;
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
                
                var membresia = objCNMembresia.BuscarActiva(item.idAlumno);

                string nombreMembresia = "Sin membresía";

                if (membresia != null)
                {
                    nombreMembresia = membresia.oMembresia.Nombre;
                }

               
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

            CN_AsignacionMembresia objCNMembresia =
                new CN_AsignacionMembresia();

            CN_AsignacionPlan objCNPlan =
                new CN_AsignacionPlan();

            foreach (Alumno item in listaAlumno)
            {
                if (
                    item.oPersona.Nombre.ToLower().Contains(texto) ||
                    item.oPersona.Apellido.ToLower().Contains(texto) ||
                    item.oPersona.DNI.ToLower().Contains(texto)
                )
                {
                    
                    var membresia = objCNMembresia.BuscarActiva(item.idAlumno);

                    string nombreMembresia = "Sin membresía";

                    if (membresia != null)
                    {
                        nombreMembresia = membresia.oMembresia.Nombre;
                    }

                    
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

            int idAlumno = Convert.ToInt32(
                dataGridPago.SelectedRows[0].Cells[0].Value
            );

            Alumno alumnoSeleccionado = listaAlumno
                .FirstOrDefault(x => x.idAlumno == idAlumno);

            if (alumnoSeleccionado == null)
            {
                MessageBox.Show(
                    "No se pudo encontrar el alumno seleccionado.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            using (frmDetallePagoMembresia formulario =
                   new frmDetallePagoMembresia(alumnoSeleccionado))
            {
                formulario.ShowDialog();
            }
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

            int idAlumno = Convert.ToInt32(
                dataGridPago.SelectedRows[0].Cells[0].Value
            );

            Alumno alumnoSeleccionado = listaAlumno
                .FirstOrDefault(x => x.idAlumno == idAlumno);

            if (alumnoSeleccionado == null)
            {
                MessageBox.Show(
                    "No se pudo encontrar el alumno seleccionado.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            using (frmDetallePagoPlan formulario =
                   new frmDetallePagoPlan(alumnoSeleccionado))
            {
                formulario.ShowDialog();

                Listar();
            }
        }

        private void Buscar_Click(object sender, EventArgs e)
        {
            Buscar(txtBuscar.Text);
        }
    }
}