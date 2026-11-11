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

            ConfigurarDiseñoResponsive();

            Listar();

            this.iconBuscar.ToolTipText =
                "Buscar un alumno por nombre, apellido o DNI." +
                Environment.NewLine +
                "Nota: Al presionar este botón, se desplegará un campo de búsqueda. " +
                "Ingrese el dato que desea buscar y vuelva a presionar el botón " +
                "Buscar para ejecutar la búsqueda.";

            this.pagarPlan.ToolTipText =
                "Realizar un pago por un plan." +
                Environment.NewLine +
                "Nota: Para pagar un plan, primero debe seleccionar al alumno que lo va a pagar " +
                "en la tabla mediante la flecha ubicada a la izquierda de su registro. " +
                "Luego, presione el botón Pagar Plan.";

            this.pagarMembresia.ToolTipText =
                "Realizar un pago por una membresía." +
                Environment.NewLine +
                "Nota: Para pagar una membresía, primero debe seleccionar al alumno que la va a pagar " +
                "en la tabla mediante la flecha ubicada a la izquierda de su registro. " +
                "Luego, presione el botón Pagar Membresía.";
        }


        private void ConfigurarDiseñoResponsive()
        {
            // Menú superior
            menuStrip1.Dock = DockStyle.Top;
            menuStrip1.AutoSize = false;
            menuStrip1.Height = 71;
            menuStrip1.GripStyle = ToolStripGripStyle.Hidden;
            menuStrip1.CanOverflow = true;
            menuStrip1.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;

            foreach (ToolStripItem item in menuStrip1.Items)
            {
                item.Overflow = ToolStripItemOverflow.AsNeeded;
            }

            // Tabla de pagos
            dataGridPago.Dock = DockStyle.Fill;
            dataGridPago.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridPago.ScrollBars = ScrollBars.Both;
            dataGridPago.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridPago.MultiSelect = false;

            dataGridPago.ColumnHeadersHeightSizeMode =
                DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            dataGridPago.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dataGridPago.DefaultCellStyle.WrapMode = DataGridViewTriState.False;

            // Distribución de columnas
            ConfigurarColumna(colIdAlumno, 45, 5);
            ConfigurarColumna(colNombre, 85, 9);
            ConfigurarColumna(colApellido, 85, 9);
            ConfigurarColumna(colDNI, 75, 8);
            ConfigurarColumna(colTelefono, 90, 9);
            ConfigurarColumna(colEmail, 120, 13);
            ConfigurarColumna(colFechaAlta, 85, 8);
            ConfigurarColumna(colEstado, 65, 6);
            ConfigurarColumna(colMembresia, 120, 13);
            ConfigurarColumna(colPlanes, 150, 20);
        }

        private void ConfigurarColumna(
            DataGridViewColumn columna,
            int anchoMinimo,
            float proporcion)
        {
            columna.MinimumWidth = anchoMinimo;
            columna.FillWeight = proporcion;
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