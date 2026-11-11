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

            ConfigurarDiseñoResponsive();

            Listar();

            this.añadir.ToolTipText =
                "Registrar un nuevo empleado.";

            this.editar.ToolTipText =
                "Modificar la información de un empleado existente." +
                Environment.NewLine +
                "Nota: Para editar un empleado, primero debe seleccionarlo " +
                "en la tabla mediante la flecha ubicada a la izquierda de su registro. " +
                "Luego, presione el botón Editar.";

            this.eliminar.ToolTipText =
                "Dar de baja a un empleado." +
                Environment.NewLine +
                "Nota: Para dar de baja a un empleado, primero debe seleccionarlo " +
                "en la tabla mediante la flecha ubicada a la izquierda de su registro. " +
                "Luego, presione el botón Eliminar.";

            this.activar.ToolTipText =
                "Dar de alta a un empleado que se encuentra dado de baja." +
                Environment.NewLine +
                "Nota: Para activar un empleado, primero debe seleccionarlo " +
                "en la tabla mediante la flecha ubicada a la izquierda de su registro. " +
                "Luego, presione el botón Activar.";

            this.buscar.ToolTipText =
                "Buscar un empleado por nombre, apellido o DNI." +
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
            menuStrip1.LayoutStyle =
                ToolStripLayoutStyle.HorizontalStackWithOverflow;

            foreach (ToolStripItem item in menuStrip1.Items)
            {
                item.Overflow = ToolStripItemOverflow.AsNeeded;
            }

            // Tabla de empleados
            dataGridEmpleado.Dock = DockStyle.Fill;

            dataGridEmpleado.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            dataGridEmpleado.ScrollBars = ScrollBars.Both;

            dataGridEmpleado.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dataGridEmpleado.MultiSelect = false;

            dataGridEmpleado.ColumnHeadersHeightSizeMode =
                DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            dataGridEmpleado.AutoSizeRowsMode =
                DataGridViewAutoSizeRowsMode.None;

            dataGridEmpleado.DefaultCellStyle.WrapMode =
                DataGridViewTriState.False;
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