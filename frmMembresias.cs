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

            ConfigurarDiseñoResponsive();

            Listar();

            this.añadir.ToolTipText =
                "Registrar una nueva membresía.";

            this.editar.ToolTipText =
                "Modificar la información de una membresía existente." +
                Environment.NewLine +
                "Nota: Para editar una membresía, primero debe seleccionarla " +
                "en la tabla mediante la flecha ubicada a la izquierda de su registro. " +
                "Luego, presione el botón Editar.";

            this.eliminar.ToolTipText =
                "Dar de baja a una membresía." +
                Environment.NewLine +
                "Nota: Para dar de baja a una membresía, primero debe seleccionarla " +
                "en la tabla mediante la flecha ubicada a la izquierda de su registro. " +
                "Luego, presione el botón Eliminar.";

            this.activar.ToolTipText =
                "Dar de alta a una membresía que se encuentra dada de baja." +
                Environment.NewLine +
                "Nota: Para activar una membresía, primero debe seleccionarla " +
                "en la tabla mediante la flecha ubicada a la izquierda de su registro. " +
                "Luego, presione el botón Activar.";

            this.buscar.ToolTipText =
                "Buscar una membresía por nombre." +
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

            // Tabla de membresías
            dataGridMembresias.Dock = DockStyle.Fill;

            dataGridMembresias.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            dataGridMembresias.ScrollBars = ScrollBars.Both;

            dataGridMembresias.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dataGridMembresias.MultiSelect = false;

            dataGridMembresias.ColumnHeadersHeightSizeMode =
                DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            dataGridMembresias.AutoSizeRowsMode =
                DataGridViewAutoSizeRowsMode.None;

            dataGridMembresias.DefaultCellStyle.WrapMode =
                DataGridViewTriState.False;

            // Proporciones de las columnas
            ConfigurarColumna(colIdMembresia, 50, 7);
            ConfigurarColumna(colNombre, 110, 17);
            ConfigurarColumna(colDescripcion, 200, 40);
            ConfigurarColumna(colPrecio, 100, 13);
            ConfigurarColumna(colDuracion, 100, 13);
            ConfigurarColumna(colEstado, 75, 10);
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
