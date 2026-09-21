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
            txtBuscar = new ToolStripTextBox();
            dataGridAlumno = new DataGridView();
            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn5 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn6 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn7 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn8 = new DataGridViewTextBoxColumn();
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
            menuStrip1.Size = new Size(1113, 71);
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
            buscar.DropDownItems.AddRange(new ToolStripItem[] { txtBuscar });
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
            // txtBuscar
            // 
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(100, 23);
            txtBuscar.Text = "Buscar";
            // 
            // dataGridAlumno
            // 
            dataGridAlumno.AllowUserToAddRows = false;
            dataGridAlumno.AllowUserToDeleteRows = false;
            dataGridAlumno.AllowUserToResizeColumns = false;
            dataGridAlumno.AllowUserToResizeRows = false;
            dataGridAlumno.BackgroundColor = SystemColors.ControlLightLight;
            dataGridAlumno.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn1, dataGridViewTextBoxColumn2, dataGridViewTextBoxColumn3, dataGridViewTextBoxColumn4, dataGridViewTextBoxColumn5, dataGridViewTextBoxColumn6, dataGridViewTextBoxColumn7, dataGridViewTextBoxColumn8, colMembresia, colPlanes, colAsistencia });
            dataGridAlumno.Location = new Point(0, 74);
            dataGridAlumno.Name = "dataGridAlumno";
            dataGridAlumno.Size = new Size(1111, 377);
            dataGridAlumno.TabIndex = 5;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewTextBoxColumn1.FillWeight = 50F;
            dataGridViewTextBoxColumn1.HeaderText = "ID";
            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            dataGridViewTextBoxColumn1.Width = 50;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewTextBoxColumn2.HeaderText = "Nombre";
            dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewTextBoxColumn3.HeaderText = "Apellido";
            dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridViewTextBoxColumn4
            // 
            dataGridViewTextBoxColumn4.HeaderText = "DNI";
            dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // dataGridViewTextBoxColumn5
            // 
            dataGridViewTextBoxColumn5.HeaderText = "Teléfono";
            dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            // 
            // dataGridViewTextBoxColumn6
            // 
            dataGridViewTextBoxColumn6.HeaderText = "Email";
            dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            // 
            // dataGridViewTextBoxColumn7
            // 
            dataGridViewTextBoxColumn7.HeaderText = "Fecha Alta";
            dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            // 
            // dataGridViewTextBoxColumn8
            // 
            dataGridViewTextBoxColumn8.HeaderText = "Estado";
            dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            dataGridViewTextBoxColumn8.Width = 50;
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
            ClientSize = new Size(1113, 449);
            Controls.Add(dataGridAlumno);
            Controls.Add(menuStrip1);
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
        private ToolStripTextBox txtBuscar;
        private FontAwesome.Sharp.IconMenuItem asistencia;
        private DataGridView dataGridAlumno;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private DataGridViewTextBoxColumn colMembresia;
        private DataGridViewTextBoxColumn colPlanes;
        private DataGridViewTextBoxColumn colAsistencia;
    }
}