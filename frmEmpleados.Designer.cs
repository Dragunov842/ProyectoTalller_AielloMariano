namespace CapaPresentacion
{
    partial class frmEmpleados
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
            dataGridEmpleado = new DataGridView();
            colIdEmpleado = new DataGridViewTextBoxColumn();
            colNombre = new DataGridViewTextBoxColumn();
            colApellido = new DataGridViewTextBoxColumn();
            colDNI = new DataGridViewTextBoxColumn();
            colTelefono = new DataGridViewTextBoxColumn();
            colEmail = new DataGridViewTextBoxColumn();
            colUsuario = new DataGridViewTextBoxColumn();
            colRol = new DataGridViewTextBoxColumn();
            colFechaAlta = new DataGridViewTextBoxColumn();
            colEstado = new DataGridViewTextBoxColumn();
            menuStrip1 = new MenuStrip();
            añadir = new FontAwesome.Sharp.IconMenuItem();
            editar = new FontAwesome.Sharp.IconMenuItem();
            eliminar = new FontAwesome.Sharp.IconMenuItem();
            activar = new FontAwesome.Sharp.IconMenuItem();
            buscar = new FontAwesome.Sharp.IconMenuItem();
            escribirToolStripMenuItem = new ToolStripMenuItem();
            txtBuscar = new ToolStripTextBox();
            ((System.ComponentModel.ISupportInitialize)dataGridEmpleado).BeginInit();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridEmpleado
            // 
            dataGridEmpleado.AllowUserToAddRows = false;
            dataGridEmpleado.AllowUserToDeleteRows = false;
            dataGridEmpleado.AllowUserToResizeColumns = false;
            dataGridEmpleado.AllowUserToResizeRows = false;
            dataGridEmpleado.BackgroundColor = SystemColors.ControlLightLight;
            dataGridEmpleado.Columns.AddRange(new DataGridViewColumn[] { colIdEmpleado, colNombre, colApellido, colDNI, colTelefono, colEmail, colUsuario, colRol, colFechaAlta, colEstado });
            dataGridEmpleado.Location = new Point(0, 74);
            dataGridEmpleado.Name = "dataGridEmpleado";
            dataGridEmpleado.Size = new Size(1129, 512);
            dataGridEmpleado.TabIndex = 1;
            // 
            // colIdEmpleado
            // 
            colIdEmpleado.HeaderText = "ID";
            colIdEmpleado.Name = "colIdEmpleado";
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
            colTelefono.HeaderText = "Telefono";
            colTelefono.Name = "colTelefono";
            // 
            // colEmail
            // 
            colEmail.HeaderText = "Email";
            colEmail.Name = "colEmail";
            // 
            // colUsuario
            // 
            colUsuario.HeaderText = "Usuario";
            colUsuario.Name = "colUsuario";
            // 
            // colRol
            // 
            colRol.HeaderText = "Rol";
            colRol.Name = "colRol";
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
            // menuStrip1
            // 
            menuStrip1.BackColor = SystemColors.Info;
            menuStrip1.Items.AddRange(new ToolStripItem[] { añadir, editar, eliminar, activar, buscar });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1129, 71);
            menuStrip1.TabIndex = 5;
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
            // frmEmpleados
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1129, 586);
            Controls.Add(menuStrip1);
            Controls.Add(dataGridEmpleado);
            FormBorderStyle = FormBorderStyle.None;
            Name = "frmEmpleados";
            Text = "frmUsuarios";
            ((System.ComponentModel.ISupportInitialize)dataGridEmpleado).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridEmpleado;
        private MenuStrip menuStrip1;
        private FontAwesome.Sharp.IconMenuItem añadir;
        private FontAwesome.Sharp.IconMenuItem editar;
        private FontAwesome.Sharp.IconMenuItem eliminar;
        private FontAwesome.Sharp.IconMenuItem activar;
        private FontAwesome.Sharp.IconMenuItem buscar;
        private DataGridViewTextBoxColumn colIdEmpleado;
        private DataGridViewTextBoxColumn colNombre;
        private DataGridViewTextBoxColumn colApellido;
        private DataGridViewTextBoxColumn colDNI;
        private DataGridViewTextBoxColumn colTelefono;
        private DataGridViewTextBoxColumn colEmail;
        private DataGridViewTextBoxColumn colUsuario;
        private DataGridViewTextBoxColumn colRol;
        private DataGridViewTextBoxColumn colFechaAlta;
        private DataGridViewTextBoxColumn colEstado;
        private ToolStripMenuItem escribirToolStripMenuItem;
        private ToolStripTextBox txtBuscar;
    }
}