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

            ConfigurarDiseñoResponsive();

            Listar();

            this.añadir.ToolTipText =
            "Registrar un nuevo plan.";

            this.editar.ToolTipText =
                "Modificar la información de un plan existente." +
                Environment.NewLine +
                "Nota: Para editar un plan, primero debe seleccionarlo " +
                "en la tabla mediante la flecha ubicada a la izquierda de su registro. " +
                "Luego, presione el botón Editar.";

            this.eliminar.ToolTipText =
                "Dar de baja a un plan." +
                Environment.NewLine +
                "Nota: Para dar de baja a un plan, primero debe seleccionarlo " +
                "en la tabla mediante la flecha ubicada a la izquierda de su registro. " +
                "Luego, presione el botón Eliminar.";

            this.activar.ToolTipText =
                "Dar de alta a un plan que se encuentra dada de baja." +
                Environment.NewLine +
                "Nota: Para activar un plan, primero debe seleccionarlo " +
                "en la tabla mediante la flecha ubicada a la izquierda de su registro. " +
                "Luego, presione el botón Activar.";

            this.buscar.ToolTipText =
                "Buscar un plan por nombre." +
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

            foreach (ToolStripItem item in menuStrip1.Items)
            {
                item.Overflow = ToolStripItemOverflow.AsNeeded;
            }

            // Tabla de planes
            dataGridPlan.Dock = DockStyle.Fill;
            dataGridPlan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridPlan.ScrollBars = ScrollBars.Both;
            dataGridPlan.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridPlan.MultiSelect = false;

            dataGridPlan.ColumnHeadersHeightSizeMode =
                DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            dataGridPlan.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dataGridPlan.DefaultCellStyle.WrapMode = DataGridViewTriState.False;

            // Distribución de columnas
            ConfigurarColumna(colIdPlan, 50, 7);
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
