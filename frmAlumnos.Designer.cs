namespace CapaPresentacion
{
    partial class frmAlumnos
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
            iconToolStripButton1 = new FontAwesome.Sharp.IconToolStripButton();
            iconToolStripButton2 = new FontAwesome.Sharp.IconToolStripButton();
            menuStrip1 = new MenuStrip();
            añadir = new FontAwesome.Sharp.IconMenuItem();
            editar = new FontAwesome.Sharp.IconMenuItem();
            eliminar = new FontAwesome.Sharp.IconMenuItem();
            activar = new FontAwesome.Sharp.IconMenuItem();
            asistencia = new FontAwesome.Sharp.IconMenuItem();
            buscar = new FontAwesome.Sharp.IconMenuItem();
            buscarToolStripMenuItem = new ToolStripMenuItem();
            txtBuscar = new ToolStripTextBox();
            dataGridAlumno = new DataGridView();
            colIDAlumno = new DataGridViewTextBoxColumn();
            colNombre = new DataGridViewTextBoxColumn();
            colApellido = new DataGridViewTextBoxColumn();
            colDNI = new DataGridViewTextBoxColumn();
            colTelefono = new DataGridViewTextBoxColumn();
            colEmail = new DataGridViewTextBoxColumn();
            colFechaAlta = new DataGridViewTextBoxColumn();
            colEstado = new DataGridViewTextBoxColumn();
            colMembresia = new DataGridViewTextBoxColumn();
            colPlanes = new DataGridViewTextBoxColumn();
            colAsistencia = new DataGridViewTextBoxColumn();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridAlumno).BeginInit();
            SuspendLayout();
            // 
            // iconToolStripButton1
            // 
            iconToolStripButton1.IconChar = FontAwesome.Sharp.IconChar.None;
            iconToolStripButton1.IconColor = Color.Black;
            iconToolStripButton1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconToolStripButton1.Name = "iconToolStripButton1";
            iconToolStripButton1.Size = new Size(23, 23);
            iconToolStripButton1.Text = "iconToolStripButton1";
            // 
            // iconToolStripButton2
            // 
            iconToolStripButton2.IconChar = FontAwesome.Sharp.IconChar.None;
            iconToolStripButton2.IconColor = Color.Black;
            iconToolStripButton2.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconToolStripButton2.Name = "iconToolStripButton2";
            iconToolStripButton2.Size = new Size(23, 23);
            iconToolStripButton2.Text = "iconToolStripButton2";
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = SystemColors.Info;
            menuStrip1.Items.AddRange(new ToolStripItem[] { añadir, editar, eliminar, activar, asistencia, buscar });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1128, 71);
            menuStrip1.TabIndex = 4;
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
            // asistencia
            // 
            asistencia.IconChar = FontAwesome.Sharp.IconChar.Certificate;
            asistencia.IconColor = Color.Black;
            asistencia.IconFont = FontAwesome.Sharp.IconFont.Auto;
            asistencia.ImageScaling = ToolStripItemImageScaling.None;
            asistencia.Name = "asistencia";
            asistencia.Size = new Size(72, 67);
            asistencia.Text = "Asistencia";
            asistencia.TextImageRelation = TextImageRelation.ImageAboveText;
            asistencia.Click += asistencia_Click;
            // 
            // buscar
            // 
            buscar.DropDownItems.AddRange(new ToolStripItem[] { buscarToolStripMenuItem, txtBuscar });
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
            // buscarToolStripMenuItem
            // 
            buscarToolStripMenuItem.Name = "buscarToolStripMenuItem";
            buscarToolStripMenuItem.Size = new Size(160, 22);
            buscarToolStripMenuItem.Text = "Escribir:";
            // 
            // txtBuscar
            // 
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(100, 23);
            // 
            // dataGridAlumno
            // 
            dataGridAlumno.AllowUserToAddRows = false;
            dataGridAlumno.AllowUserToDeleteRows = false;
            dataGridAlumno.AllowUserToResizeColumns = false;
            dataGridAlumno.AllowUserToResizeRows = false;
            dataGridAlumno.BackgroundColor = SystemColors.ControlLightLight;
            dataGridAlumno.Columns.AddRange(new DataGridViewColumn[] { colIDAlumno, colNombre, colApellido, colDNI, colTelefono, colEmail, colFechaAlta, colEstado, colMembresia, colPlanes, colAsistencia });
            dataGridAlumno.Location = new Point(0, 74);
            dataGridAlumno.Name = "dataGridAlumno";
            dataGridAlumno.Size = new Size(1128, 509);
            dataGridAlumno.TabIndex = 5;
            // 
            // colIDAlumno
            // 
            colIDAlumno.FillWeight = 50F;
            colIDAlumno.HeaderText = "ID";
            colIDAlumno.Name = "colIDAlumno";
            colIDAlumno.Width = 50;
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
            colEstado.Width = 50;
            // 
            // colMembresia
            // 
            colMembresia.HeaderText = "Membresia";
            colMembresia.Name = "colMembresia";
            colMembresia.Width = 200;
            // 
            // colPlanes
            // 
            colPlanes.HeaderText = "Planes";
            colPlanes.Name = "colPlanes";
            // 
            // colAsistencia
            // 
            colAsistencia.HeaderText = "Presente";
            colAsistencia.Name = "colAsistencia";
            colAsistencia.Width = 75;
            // 
            // frmAlumnos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1128, 584);
            Controls.Add(dataGridAlumno);
            Controls.Add(menuStrip1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "frmAlumnos";
            Text = "frmAlumno";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridAlumno).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private FontAwesome.Sharp.IconToolStripButton iconToolStripButton1;
        private FontAwesome.Sharp.IconToolStripButton iconToolStripButton2;
        private MenuStrip menuStrip1;
        private FontAwesome.Sharp.IconMenuItem añadir;
        private FontAwesome.Sharp.IconMenuItem editar;
        private FontAwesome.Sharp.IconMenuItem eliminar;
        private FontAwesome.Sharp.IconMenuItem activar;
        private FontAwesome.Sharp.IconMenuItem buscar;
        private FontAwesome.Sharp.IconMenuItem asistencia;
        private DataGridView dataGridAlumno;
        private ToolStripMenuItem buscarToolStripMenuItem;
        private ToolStripTextBox txtBuscar;
        private DataGridViewTextBoxColumn colIDAlumno;
        private DataGridViewTextBoxColumn colNombre;
        private DataGridViewTextBoxColumn colApellido;
        private DataGridViewTextBoxColumn colDNI;
        private DataGridViewTextBoxColumn colTelefono;
        private DataGridViewTextBoxColumn colEmail;
        private DataGridViewTextBoxColumn colFechaAlta;
        private DataGridViewTextBoxColumn colEstado;
        private DataGridViewTextBoxColumn colMembresia;
        private DataGridViewTextBoxColumn colPlanes;
        private DataGridViewTextBoxColumn colAsistencia;
    }
}