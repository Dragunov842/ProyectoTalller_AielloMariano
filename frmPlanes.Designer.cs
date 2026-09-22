namespace CapaPresentacion
{
    partial class frmPlanes
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dataGridPlan = new DataGridView();
            colIdPlan = new DataGridViewTextBoxColumn();
            colNombre = new DataGridViewTextBoxColumn();
            colDescripcion = new DataGridViewTextBoxColumn();
            colPrecio = new DataGridViewTextBoxColumn();
            colDuracion = new DataGridViewTextBoxColumn();
            colEstado = new DataGridViewTextBoxColumn();
            menuStrip1 = new MenuStrip();
            añadir = new FontAwesome.Sharp.IconMenuItem();
            editar = new FontAwesome.Sharp.IconMenuItem();
            eliminar = new FontAwesome.Sharp.IconMenuItem();
            activar = new FontAwesome.Sharp.IconMenuItem();
            buscar = new FontAwesome.Sharp.IconMenuItem();
            escribirToolStripMenuItem = new ToolStripMenuItem();
            txtBuscar = new ToolStripTextBox();
            ((System.ComponentModel.ISupportInitialize)dataGridPlan).BeginInit();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridPlan
            // 
            dataGridPlan.AllowUserToAddRows = false;
            dataGridPlan.AllowUserToDeleteRows = false;
            dataGridPlan.AllowUserToResizeColumns = false;
            dataGridPlan.AllowUserToResizeRows = false;
            dataGridPlan.BackgroundColor = SystemColors.ControlLightLight;
            dataGridPlan.Columns.AddRange(new DataGridViewColumn[] { colIdPlan, colNombre, colDescripcion, colPrecio, colDuracion, colEstado });
            dataGridPlan.Location = new Point(0, 74);
            dataGridPlan.Name = "dataGridPlan";
            dataGridPlan.Size = new Size(1130, 494);
            dataGridPlan.TabIndex = 1;
            // 
            // colIdPlan
            // 
            colIdPlan.HeaderText = "ID";
            colIdPlan.Name = "colIdPlan";
            // 
            // colNombre
            // 
            colNombre.HeaderText = "Nombre";
            colNombre.Name = "colNombre";
            // 
            // colDescripcion
            // 
            colDescripcion.HeaderText = "Descripcion";
            colDescripcion.Name = "colDescripcion";
            colDescripcion.Width = 500;
            // 
            // colPrecio
            // 
            colPrecio.HeaderText = "Precio";
            colPrecio.Name = "colPrecio";
            // 
            // colDuracion
            // 
            colDuracion.HeaderText = "Duracion (Dias)";
            colDuracion.Name = "colDuracion";
            // 
            // colEstado
            // 
            colEstado.HeaderText = "Estado";
            colEstado.Name = "colEstado";
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = SystemColors.Info;
            menuStrip1.Items.AddRange(new ToolStripItem[] { añadir, editar, eliminar, activar, buscar });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1130, 71);
            menuStrip1.TabIndex = 6;
            menuStrip1.Text = "menuStrip1";
            // 
            // añadir
            // 
            añadir.IconChar = FontAwesome.Sharp.IconChar.CircleArrowUp;
            añadir.IconColor = Color.Black;
            añadir.IconFont = FontAwesome.Sharp.IconFont.Auto;
            añadir.ImageScaling = ToolStripItemImageScaling.None;
            añadir.Name = "añadir";
            añadir.Size = new Size(60, 67);
            añadir.Text = "Añadir";
            añadir.TextImageRelation = TextImageRelation.ImageAboveText;
            añadir.Click += añadir_Click;
            // 
            // editar
            // 
            editar.IconChar = FontAwesome.Sharp.IconChar.CodeCompare;
            editar.IconColor = Color.Black;
            editar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            editar.ImageScaling = ToolStripItemImageScaling.None;
            editar.Name = "editar";
            editar.Size = new Size(60, 67);
            editar.Text = "Editar";
            editar.TextImageRelation = TextImageRelation.ImageAboveText;
            editar.Click += editar_Click;
            // 
            // eliminar
            // 
            eliminar.IconChar = FontAwesome.Sharp.IconChar.Cancel;
            eliminar.IconColor = Color.Black;
            eliminar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            eliminar.ImageScaling = ToolStripItemImageScaling.None;
            eliminar.Name = "eliminar";
            eliminar.Size = new Size(78, 67);
            eliminar.Text = "Dar de baja";
            eliminar.TextImageRelation = TextImageRelation.ImageAboveText;
            eliminar.Click += eliminar_Click;
            // 
            // activar
            // 
            activar.IconChar = FontAwesome.Sharp.IconChar.Check;
            activar.IconColor = Color.Black;
            activar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            activar.ImageScaling = ToolStripItemImageScaling.None;
            activar.Name = "activar";
            activar.Size = new Size(75, 67);
            activar.Text = "Dar de alta";
            activar.TextImageRelation = TextImageRelation.ImageAboveText;
            activar.Click += activar_Click;
            // 
            // buscar
            // 
            buscar.DropDownItems.AddRange(new ToolStripItem[] { escribirToolStripMenuItem, txtBuscar });
            buscar.IconChar = FontAwesome.Sharp.IconChar.Compass;
            buscar.IconColor = Color.Black;
            buscar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            buscar.ImageScaling = ToolStripItemImageScaling.None;
            buscar.Name = "buscar";
            buscar.Size = new Size(60, 67);
            buscar.Text = "Buscar";
            buscar.TextImageRelation = TextImageRelation.ImageAboveText;
            buscar.Click += buscar_Click;
            // 
            // escribirToolStripMenuItem
            // 
            escribirToolStripMenuItem.Name = "escribirToolStripMenuItem";
            escribirToolStripMenuItem.Size = new Size(180, 22);
            escribirToolStripMenuItem.Text = "Escribir:";
            // 
            // txtBuscar
            // 
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(100, 23);
            // 
            // frmPlanes
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1130, 568);
            Controls.Add(menuStrip1);
            Controls.Add(dataGridPlan);
            FormBorderStyle = FormBorderStyle.None;
            Name = "frmPlanes";
            Text = "frmPlanes";
            ((System.ComponentModel.ISupportInitialize)dataGridPlan).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridPlan;
        private MenuStrip menuStrip1;
        private FontAwesome.Sharp.IconMenuItem añadir;
        private FontAwesome.Sharp.IconMenuItem editar;
        private FontAwesome.Sharp.IconMenuItem eliminar;
        private FontAwesome.Sharp.IconMenuItem activar;
        private FontAwesome.Sharp.IconMenuItem buscar;
        private DataGridViewTextBoxColumn colIdPlan;
        private DataGridViewTextBoxColumn colNombre;
        private DataGridViewTextBoxColumn colDescripcion;
        private DataGridViewTextBoxColumn colPrecio;
        private DataGridViewTextBoxColumn colDuracion;
        private DataGridViewTextBoxColumn colEstado;
        private ToolStripMenuItem escribirToolStripMenuItem;
        private ToolStripTextBox txtBuscar;
    }
}