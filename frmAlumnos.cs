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

            ConfigurarDiseñoResponsive();

            empleadoLogueado = empleado;

            ConfigurarPermisos();
            Listar();

            this.añadir.ToolTipText =
                "Registrar un nuevo alumno.";

            this.editar.ToolTipText =
                "Modificar la información de un alumno existente." +
                Environment.NewLine +
                "Nota: Para editar un alumno, primero debe seleccionarlo " +
                "en la tabla mediante la flecha ubicada a la izquierda de su registro. " +
                "Luego, presione el botón Editar.";

            this.eliminar.ToolTipText =
                "Dar de baja a un alumno." +
                Environment.NewLine +
                "Nota: Para dar de baja a un alumno, primero debe seleccionarlo " +
                "en la tabla mediante la flecha ubicada a la izquierda de su registro. " +
                "Luego, presione el botón Eliminar.";

            this.activar.ToolTipText =
                "Dar de alta a un alumno que se encuentra dado de baja." +
                Environment.NewLine +
                "Nota: Para activar un alumno, primero debe seleccionarlo " +
                "en la tabla mediante la flecha ubicada a la izquierda de su registro. " +
                "Luego, presione el botón Activar.";

            this.asistencia.ToolTipText =
                "Registrar la asistencia del día de un alumno." +
                Environment.NewLine +
                "Nota: Para registrar la asistencia, primero debe seleccionar " +
                "al alumno en la tabla mediante la flecha ubicada a la izquierda " +
                "de su registro. Luego, presione el botón Asistencia.";

            this.buscar.ToolTipText =
                "Buscar un alumno por nombre, apellido o DNI." +
                Environment.NewLine +
                "Nota: Al presionar este botón, se desplegará un campo de búsqueda. " +
                "Ingrese el dato que desea buscar y vuelva a presionar el botón " +
                "Buscar para ejecutar la búsqueda.";
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

            // Permitir que los botones pasen al menú de desbordamiento
            foreach (ToolStripItem item in menuStrip1.Items)
            {
                item.Overflow = ToolStripItemOverflow.AsNeeded;
            }

            // Tabla de alumnos
            dataGridAlumno.Dock = DockStyle.Fill;
            dataGridAlumno.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridAlumno.ScrollBars = ScrollBars.Both;

            // Selección de registros
            dataGridAlumno.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridAlumno.MultiSelect = false;

            // Presentación de la tabla
            dataGridAlumno.ColumnHeadersHeightSizeMode =
                DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            dataGridAlumno.AutoSizeRowsMode =
                DataGridViewAutoSizeRowsMode.None;

            dataGridAlumno.DefaultCellStyle.WrapMode =
                DataGridViewTriState.False;

            // Anchos mínimos y proporciones de las columnas
            ConfigurarColumna(colIDAlumno, 45, 4);
            ConfigurarColumna(colNombre, 85, 9);
            ConfigurarColumna(colApellido, 85, 9);
            ConfigurarColumna(colDNI, 75, 8);
            ConfigurarColumna(colTelefono, 90, 10);
            ConfigurarColumna(colEmail, 120, 14);
            ConfigurarColumna(colFechaAlta, 85, 9);
            ConfigurarColumna(colEstado, 65, 7);
            ConfigurarColumna(colMembresia, 110, 12);
            ConfigurarColumna(colPlanes, 110, 12);
            ConfigurarColumna(colAsistencia, 70, 6);
        }

        private void ConfigurarColumna(
            DataGridViewColumn columna,
            int anchoMinimo,
            float proporcion)
        {
            columna.MinimumWidth = anchoMinimo;
            columna.FillWeight = proporcion;
        }


        private void ConfigurarPermisos()
        {
           
            asistencia.Visible = false;

           
            if (empleadoLogueado.oRol.idRol == 3 || empleadoLogueado.oRol.idRol == 1)
            {
                asistencia.Visible = true;
            }


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

            CN_AsignacionMembresia objCNMembresia =
                new CN_AsignacionMembresia();

            CN_AsignacionPlan objCNPlan =
                new CN_AsignacionPlan();

            CD_Asistencia objCDAsistencia =
                new CD_Asistencia();

            foreach (Alumno item in listaAlumno)
            {
                
                var membresia =
                    objCNMembresia.BuscarActiva(item.idAlumno);

                string nombreMembresia = "Sin membresía";

                if (membresia != null)
                {
                    nombreMembresia =
                        membresia.oMembresia.Nombre;
                }

                
                var planes =
                    objCNPlan.ListarActivosPorAlumno(item.idAlumno);

                string nombresPlanes = "Sin planes";

                if (planes.Count > 0)
                {
                    nombresPlanes = string.Join(
                        ", ",
                        planes.ConvertAll(x => x.oPlan.Nombre)
                    );
                }

                
                bool asistioHoy =
                    objCDAsistencia.YaAsistioHoy(item.idAlumno);

                string presente =
                    asistioHoy ? "Sí" : "No";

                dataGridAlumno.Rows.Add(
                    item.idAlumno,
                    item.oPersona.Nombre,
                    item.oPersona.Apellido,
                    item.oPersona.DNI,
                    item.oPersona.Telefono,
                    item.oPersona.Email,
                    item.Fecha_Alta.ToString("dd/MM/yyyy"),
                    item.Estado ? "Activo" : "Inactivo",
                    nombreMembresia,
                    nombresPlanes,
                    presente
                );
            }
        }

        private void Buscar(string texto)
        {
            texto = texto.Trim().ToLower();

            dataGridAlumno.Rows.Clear();

            CN_AsignacionMembresia objCNMembresia =
                new CN_AsignacionMembresia();

            CN_AsignacionPlan objCNPlan =
                new CN_AsignacionPlan();

            CD_Asistencia objCDAsistencia =
                new CD_Asistencia();

            foreach (Alumno item in listaAlumno)
            {
                if (
                    item.oPersona.Nombre.ToLower().Contains(texto) ||
                    item.oPersona.Apellido.ToLower().Contains(texto) ||
                    item.oPersona.DNI.ToLower().Contains(texto)
                )
                {
                   
                    var membresia =
                        objCNMembresia.BuscarActiva(item.idAlumno);

                    string nombreMembresia = "Sin membresía";

                    if (membresia != null)
                    {
                        nombreMembresia =
                            membresia.oMembresia.Nombre;
                    }

                    
                    var planes =
                        objCNPlan.ListarActivosPorAlumno(item.idAlumno);

                    string nombresPlanes = "Sin planes";

                    if (planes.Count > 0)
                    {
                        nombresPlanes = string.Join(
                            ", ",
                            planes.ConvertAll(x => x.oPlan.Nombre)
                        );
                    }

                    
                    bool asistioHoy =
                        objCDAsistencia.YaAsistioHoy(item.idAlumno);

                    string presente =
                        asistioHoy ? "Sí" : "No";

                    dataGridAlumno.Rows.Add(
                        item.idAlumno,
                        item.oPersona.Nombre,
                        item.oPersona.Apellido,
                        item.oPersona.DNI,
                        item.oPersona.Telefono,
                        item.oPersona.Email,
                        item.Fecha_Alta.ToString("dd/MM/yyyy"),
                        item.Estado ? "Activo" : "Inactivo",
                        nombreMembresia,
                        nombresPlanes,
                        presente
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

        private void asistencia_Click(object sender, EventArgs e)
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

            try
            {
                CN_Asistencia objCNAsistencia =
                    new CN_Asistencia();

                objCNAsistencia.Registrar(
                    alumnoSeleccionado.idAlumno
                );

                MessageBox.Show(
                    "Asistencia registrada correctamente.",
                    "Éxito",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                Listar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }


        private void buscar_Click(object sender, EventArgs e)
        {
            Buscar(txtBuscar.Text);
        }

        private void txtBuscar_Click(object sender, EventArgs e)
        {

        }
    }
}
