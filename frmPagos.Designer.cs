namespace CapaPresentacion
{
    partial class frmPagos
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
            dataGridPago = new DataGridView();
            colIdAlumno = new DataGridViewTextBoxColumn();
            colNombre = new DataGridViewTextBoxColumn();
            colApellido = new DataGridViewTextBoxColumn();
            colDNI = new DataGridViewTextBoxColumn();
            colTelefono = new DataGridViewTextBoxColumn();
            colEmail = new DataGridViewTextBoxColumn();
            colFechaAlta = new DataGridViewTextBoxColumn();
            colEstado = new DataGridViewTextBoxColumn();
            colMembresia = new DataGridViewTextBoxColumn();
            colPlanes = new DataGridViewTextBoxColumn();
            menuStrip1 = new MenuStrip();
            pagarMembresia = new FontAwesome.Sharp.IconMenuItem();
            pagarPlan = new FontAwesome.Sharp.IconMenuItem();
            iconBuscar = new FontAwesome.Sharp.IconMenuItem();
            buscarToolStripMenuItem = new ToolStripMenuItem();
            txtBuscar = new ToolStripTextBox();
            ((System.ComponentModel.ISupportInitialize)dataGridPago).BeginInit();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridPago
            // 
            dataGridPago.AllowUserToAddRows = false;
            dataGridPago.AllowUserToDeleteRows = false;
            dataGridPago.AllowUserToResizeColumns = false;
            dataGridPago.AllowUserToResizeRows = false;
            dataGridPago.BackgroundColor = SystemColors.ControlLightLight;
            dataGridPago.Columns.AddRange(new DataGridViewColumn[] { colIdAlumno, colNombre, colApellido, colDNI, colTelefono, colEmail, colFechaAlta, colEstado, colMembresia, colPlanes });
            dataGridPago.Location = new Point(0, 74);
            dataGridPago.Name = "dataGridPago";
            dataGridPago.Size = new Size(1131, 511);
            dataGridPago.TabIndex = 1;
            // 
            // colIdAlumno
            // 
            colIdAlumno.HeaderText = "ID";
            colIdAlumno.Name = "colIdAlumno";
            // 
            // colNombre
            // 
            colNombre.HeaderText = "Nombre";
            colNombre.Name = "colNombre";
            // 
            // colApellido
            // 
            colApellido.HeaderText = "Apellido";
            colApellido.Name = "colApellido";
            // 
            // colDNI
            // 
            colDNI.HeaderText = "DNI";
            colDNI.Name = "colDNI";
            // 
            // colTelefono
            // 
            colTelefono.HeaderText = "Teléfono";
            colTelefono.Name = "colTelefono";
            // 
            // colEmail
            // 
            colEmail.HeaderText = "Email";
            colEmail.Name = "colEmail";
            // 
            // colFechaAlta
            // 
            colFechaAlta.HeaderText = "Fecha Alta";
            colFechaAlta.Name = "colFechaAlta";
            // 
            // colEstado
            // 
            colEstado.HeaderText = "Estado";
            colEstado.Name = "colEstado";
            // 
            // colMembresia
            // 
            colMembresia.HeaderText = "Membresia";
            colMembresia.Name = "colMembresia";
            // 
            // colPlanes
            // 
            colPlanes.HeaderText = "Planes";
            colPlanes.Name = "colPlanes";
            colPlanes.Width = 175;
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = SystemColors.Info;
            menuStrip1.Items.AddRange(new ToolStripItem[] { pagarMembresia, pagarPlan, iconBuscar });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1131, 71);
            menuStrip1.TabIndex = 5;
            menuStrip1.Text = "menuStrip1";
            // 
            // pagarMembresia
            // 
            pagarMembresia.IconChar = FontAwesome.Sharp.IconChar.IdCardClip;
            pagarMembresia.IconColor = Color.Black;
            pagarMembresia.IconFont = FontAwesome.Sharp.IconFont.Auto;
            pagarMembresia.ImageScaling = ToolStripItemImageScaling.None;
            pagarMembresia.Name = "pagarMembresia";
            pagarMembresia.Size = new Size(111, 67);
            pagarMembresia.Text = "Pagar Membresia";
            pagarMembresia.TextImageRelation = TextImageRelation.ImageAboveText;
            pagarMembresia.Click += pagarMembresia_Click;
            // 
            // pagarPlan
            // 
            pagarPlan.IconChar = FontAwesome.Sharp.IconChar.Newspaper;
            pagarPlan.IconColor = Color.Black;
            pagarPlan.IconFont = FontAwesome.Sharp.IconFont.Auto;
            pagarPlan.ImageScaling = ToolStripItemImageScaling.None;
            pagarPlan.Name = "pagarPlan";
            pagarPlan.Size = new Size(75, 67);
            pagarPlan.Text = "Pagar Plan";
            pagarPlan.TextImageRelation = TextImageRelation.ImageAboveText;
            pagarPlan.Click += pagarPlan_Click;
            // 
            // iconBuscar
            // 
            iconBuscar.DropDownItems.AddRange(new ToolStripItem[] { buscarToolStripMenuItem, txtBuscar });
            iconBuscar.IconChar = FontAwesome.Sharp.IconChar.Compass;
            iconBuscar.IconColor = Color.Black;
            iconBuscar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconBuscar.ImageScaling = ToolStripItemImageScaling.None;
            iconBuscar.Name = "iconBuscar";
            iconBuscar.Size = new Size(60, 67);
            iconBuscar.Text = "Buscar";
            iconBuscar.TextImageRelation = TextImageRelation.ImageAboveText;
            iconBuscar.Click += Buscar_Click;
            // 
            // buscarToolStripMenuItem
            // 
            buscarToolStripMenuItem.Name = "buscarToolStripMenuItem";
            buscarToolStripMenuItem.Size = new Size(180, 22);
            buscarToolStripMenuItem.Text = "Escribir:";
            // 
            // txtBuscar
            // 
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(100, 23);
            // 
            // frmPagos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1131, 584);
            Controls.Add(menuStrip1);
            Controls.Add(dataGridPago);
            FormBorderStyle = FormBorderStyle.None;
            Name = "frmPagos";
            Text = "Pagos";
            ((System.ComponentModel.ISupportInitialize)dataGridPago).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridPago;
        private MenuStrip menuStrip1;
        private FontAwesome.Sharp.IconMenuItem pagarMembresia;
        private FontAwesome.Sharp.IconMenuItem pagarPlan;
        private DataGridViewTextBoxColumn colIdAlumno;
        private DataGridViewTextBoxColumn colNombre;
        private DataGridViewTextBoxColumn colApellido;
        private DataGridViewTextBoxColumn colDNI;
        private DataGridViewTextBoxColumn colTelefono;
        private DataGridViewTextBoxColumn colEmail;
        private DataGridViewTextBoxColumn colFechaAlta;
        private DataGridViewTextBoxColumn colEstado;
        private DataGridViewTextBoxColumn colMembresia;
        private DataGridViewTextBoxColumn colPlanes;
        private FontAwesome.Sharp.IconMenuItem iconBuscar;
        private ToolStripMenuItem buscarToolStripMenuItem;
        private ToolStripTextBox txtBuscar;
    }
}