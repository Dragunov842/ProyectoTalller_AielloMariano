namespace CapaPresentacion
{
    partial class Inicio
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel1 = new Panel();
            lblusuario = new Label();
            Usuario = new Label();
            label1 = new Label();
            menuStrip1 = new MenuStrip();
            menuAlumnos = new FontAwesome.Sharp.IconMenuItem();
            menuAsistencias = new FontAwesome.Sharp.IconMenuItem();
            menuMembresias = new FontAwesome.Sharp.IconMenuItem();
            menuPlanes = new FontAwesome.Sharp.IconMenuItem();
            menuPagos = new FontAwesome.Sharp.IconMenuItem();
            menuReportes = new FontAwesome.Sharp.IconMenuItem();
            menuUsuarios = new FontAwesome.Sharp.IconMenuItem();
            menuSalir = new FontAwesome.Sharp.IconMenuItem();
            contenedor = new Panel();
            panel1.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ActiveCaption;
            panel1.Controls.Add(lblusuario);
            panel1.Controls.Add(Usuario);
            panel1.Controls.Add(label1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 71);
            panel1.Name = "panel1";
            panel1.Size = new Size(1022, 74);
            panel1.TabIndex = 0;
            panel1.Paint += panel1_Paint;
            // 
            // lblusuario
            // 
            lblusuario.AutoSize = true;
            lblusuario.ForeColor = Color.FromArgb(192, 0, 0);
            lblusuario.Location = new Point(851, 35);
            lblusuario.Name = "lblusuario";
            lblusuario.Size = new Size(59, 15);
            lblusuario.TabIndex = 2;
            lblusuario.Text = "lblusuario";
            // 
            // Usuario
            // 
            Usuario.AutoSize = true;
            Usuario.Font = new Font("Segoe UI", 10F);
            Usuario.Location = new Point(795, 33);
            Usuario.Name = "Usuario";
            Usuario.Size = new Size(59, 19);
            Usuario.TabIndex = 1;
            Usuario.Text = "Usuario:";
            Usuario.Click += label2_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 25F);
            label1.ForeColor = Color.Gold;
            label1.Location = new Point(415, 8);
            label1.Name = "label1";
            label1.Size = new Size(204, 46);
            label1.TabIndex = 0;
            label1.Text = "SportXpresS";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            label1.Click += label1_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { menuAlumnos, menuAsistencias, menuMembresias, menuPlanes, menuPagos, menuReportes, menuUsuarios, menuSalir });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1022, 71);
            menuStrip1.TabIndex = 3;
            menuStrip1.Text = "menuStrip1";
            // 
            // menuAlumnos
            // 
            menuAlumnos.IconChar = FontAwesome.Sharp.IconChar.Dumbbell;
            menuAlumnos.IconColor = Color.Black;
            menuAlumnos.IconFont = FontAwesome.Sharp.IconFont.Auto;
            menuAlumnos.ImageScaling = ToolStripItemImageScaling.None;
            menuAlumnos.Name = "menuAlumnos";
            menuAlumnos.Size = new Size(67, 67);
            menuAlumnos.Text = "Alumnos";
            menuAlumnos.TextImageRelation = TextImageRelation.ImageAboveText;
            menuAlumnos.Click += menuAlumnos_Click;
            // 
            // menuAsistencias
            // 
            menuAsistencias.IconChar = FontAwesome.Sharp.IconChar.Check;
            menuAsistencias.IconColor = Color.Black;
            menuAsistencias.IconFont = FontAwesome.Sharp.IconFont.Auto;
            menuAsistencias.ImageScaling = ToolStripItemImageScaling.None;
            menuAsistencias.Name = "menuAsistencias";
            menuAsistencias.Size = new Size(77, 67);
            menuAsistencias.Text = "Asistencias";
            menuAsistencias.TextImageRelation = TextImageRelation.ImageAboveText;
            menuAsistencias.Click += menuAsistencias_Click;
            // 
            // menuMembresias
            // 
            menuMembresias.IconChar = FontAwesome.Sharp.IconChar.IdCardClip;
            menuMembresias.IconColor = Color.Black;
            menuMembresias.IconFont = FontAwesome.Sharp.IconFont.Auto;
            menuMembresias.ImageScaling = ToolStripItemImageScaling.None;
            menuMembresias.Name = "menuMembresias";
            menuMembresias.Size = new Size(83, 67);
            menuMembresias.Text = "Membresias";
            menuMembresias.TextImageRelation = TextImageRelation.ImageAboveText;
            menuMembresias.Click += menuMembresias_Click;
            // 
            // menuPlanes
            // 
            menuPlanes.IconChar = FontAwesome.Sharp.IconChar.Newspaper;
            menuPlanes.IconColor = Color.Black;
            menuPlanes.IconFont = FontAwesome.Sharp.IconFont.Auto;
            menuPlanes.ImageScaling = ToolStripItemImageScaling.None;
            menuPlanes.Name = "menuPlanes";
            menuPlanes.Size = new Size(60, 67);
            menuPlanes.Text = "Planes";
            menuPlanes.TextImageRelation = TextImageRelation.ImageAboveText;
            menuPlanes.Click += menuPlanes_Click;
            // 
            // menuPagos
            // 
            menuPagos.IconChar = FontAwesome.Sharp.IconChar.MoneyBill1Wave;
            menuPagos.IconColor = Color.Black;
            menuPagos.IconFont = FontAwesome.Sharp.IconFont.Auto;
            menuPagos.ImageScaling = ToolStripItemImageScaling.None;
            menuPagos.Name = "menuPagos";
            menuPagos.Size = new Size(60, 67);
            menuPagos.Text = "Pagos";
            menuPagos.TextImageRelation = TextImageRelation.ImageAboveText;
            menuPagos.Click += menuPagos_Click;
            // 
            // menuReportes
            // 
            menuReportes.IconChar = FontAwesome.Sharp.IconChar.Ruler;
            menuReportes.IconColor = Color.Black;
            menuReportes.IconFont = FontAwesome.Sharp.IconFont.Auto;
            menuReportes.ImageScaling = ToolStripItemImageScaling.None;
            menuReportes.Name = "menuReportes";
            menuReportes.Size = new Size(65, 67);
            menuReportes.Text = "Reportes";
            menuReportes.TextImageRelation = TextImageRelation.ImageAboveText;
            menuReportes.Click += menuReportes_Click;
            // 
            // menuUsuarios
            // 
            menuUsuarios.IconChar = FontAwesome.Sharp.IconChar.User;
            menuUsuarios.IconColor = Color.Black;
            menuUsuarios.IconFont = FontAwesome.Sharp.IconFont.Auto;
            menuUsuarios.ImageScaling = ToolStripItemImageScaling.None;
            menuUsuarios.Name = "menuUsuarios";
            menuUsuarios.Size = new Size(64, 67);
            menuUsuarios.Text = "Usuarios";
            menuUsuarios.TextImageRelation = TextImageRelation.ImageAboveText;
            menuUsuarios.Click += menuUsuarios_Click;
            // 
            // menuSalir
            // 
            menuSalir.IconChar = FontAwesome.Sharp.IconChar.PersonWalkingDashedLineArrowRight;
            menuSalir.IconColor = Color.Black;
            menuSalir.IconFont = FontAwesome.Sharp.IconFont.Auto;
            menuSalir.ImageScaling = ToolStripItemImageScaling.None;
            menuSalir.Name = "menuSalir";
            menuSalir.Size = new Size(88, 67);
            menuSalir.Text = "Cerrar Sesion";
            menuSalir.TextImageRelation = TextImageRelation.ImageAboveText;
            menuSalir.Click += menuSalir_Click;
            // 
            // contenedor
            // 
            contenedor.BackColor = Color.Cornsilk;
            contenedor.Location = new Point(0, 143);
            contenedor.Name = "contenedor";
            contenedor.Size = new Size(1022, 551);
            contenedor.TabIndex = 4;
            contenedor.Paint += contenedor_Paint;
            // 
            // Inicio
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(1022, 691);
            Controls.Add(contenedor);
            Controls.Add(panel1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Inicio";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Menu";
            Load += Inicio_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Label label1;
        private MenuStrip menuStrip1;
        private FontAwesome.Sharp.IconMenuItem menuReportes;
        private FontAwesome.Sharp.IconMenuItem menuAsistencias;
        private FontAwesome.Sharp.IconMenuItem menuMembresias;
        private FontAwesome.Sharp.IconMenuItem menuPlanes;
        private FontAwesome.Sharp.IconMenuItem menuPagos;
        private FontAwesome.Sharp.IconMenuItem menuUsuarios;
        private FontAwesome.Sharp.IconMenuItem menuSalir;
        private FontAwesome.Sharp.IconMenuItem menuAlumnos;
        private Label lblusuario;
        private Label Usuario;
        private Panel contenedor;
    }
}