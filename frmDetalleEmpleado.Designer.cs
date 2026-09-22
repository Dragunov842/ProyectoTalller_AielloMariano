namespace CapaPresentacion
{
    partial class frmDetalleEmpleado
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
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            btnCancelar = new FontAwesome.Sharp.IconButton();
            btnGuardar = new FontAwesome.Sharp.IconButton();
            txtApellido = new TextBox();
            txtDNI = new TextBox();
            txtNombre = new TextBox();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            txtContraseña = new TextBox();
            txtUsuario = new TextBox();
            cmbRol = new ComboBox();
            label5 = new Label();
            txtEmail = new TextBox();
            label4 = new Label();
            txtTelefono = new TextBox();
            SuspendLayout();
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(383, 153);
            label3.Name = "label3";
            label3.Size = new Size(27, 15);
            label3.TabIndex = 21;
            label3.Text = "DNI";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(371, 87);
            label2.Name = "label2";
            label2.Size = new Size(51, 15);
            label2.TabIndex = 20;
            label2.Text = "Apellido";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(371, 21);
            label1.Name = "label1";
            label1.Size = new Size(51, 15);
            label1.TabIndex = 19;
            label1.Text = "Nombre";
            // 
            // btnCancelar
            // 
            btnCancelar.BackColor = Color.FromArgb(255, 128, 128);
            btnCancelar.Cursor = Cursors.Hand;
            btnCancelar.IconChar = FontAwesome.Sharp.IconChar.Cancel;
            btnCancelar.IconColor = Color.Black;
            btnCancelar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnCancelar.ImageAlign = ContentAlignment.TopCenter;
            btnCancelar.Location = new Point(446, 578);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(75, 67);
            btnCancelar.TabIndex = 18;
            btnCancelar.Text = "Cancelar";
            btnCancelar.TextAlign = ContentAlignment.BottomCenter;
            btnCancelar.UseVisualStyleBackColor = false;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // btnGuardar
            // 
            btnGuardar.BackColor = Color.FromArgb(192, 255, 192);
            btnGuardar.Cursor = Cursors.Hand;
            btnGuardar.IconChar = FontAwesome.Sharp.IconChar.Check;
            btnGuardar.IconColor = Color.Black;
            btnGuardar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnGuardar.ImageAlign = ContentAlignment.TopCenter;
            btnGuardar.Location = new Point(280, 578);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(75, 67);
            btnGuardar.TabIndex = 17;
            btnGuardar.Text = "Guardar";
            btnGuardar.TextAlign = ContentAlignment.BottomCenter;
            btnGuardar.UseVisualStyleBackColor = false;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // txtApellido
            // 
            txtApellido.Location = new Point(280, 105);
            txtApellido.Name = "txtApellido";
            txtApellido.Size = new Size(241, 23);
            txtApellido.TabIndex = 16;
            // 
            // txtDNI
            // 
            txtDNI.Location = new Point(280, 171);
            txtDNI.Name = "txtDNI";
            txtDNI.Size = new Size(241, 23);
            txtDNI.TabIndex = 15;
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(280, 39);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(241, 23);
            txtNombre.TabIndex = 12;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(371, 478);
            label6.Name = "label6";
            label6.Size = new Size(67, 15);
            label6.TabIndex = 29;
            label6.Text = "Contraseña";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(376, 413);
            label7.Name = "label7";
            label7.Size = new Size(47, 15);
            label7.TabIndex = 28;
            label7.Text = "Usuario";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(386, 346);
            label8.Name = "label8";
            label8.Size = new Size(24, 15);
            label8.TabIndex = 27;
            label8.Text = "Rol";
            // 
            // txtContraseña
            // 
            txtContraseña.Location = new Point(280, 496);
            txtContraseña.Name = "txtContraseña";
            txtContraseña.PasswordChar = '*';
            txtContraseña.Size = new Size(241, 23);
            txtContraseña.TabIndex = 25;
            // 
            // txtUsuario
            // 
            txtUsuario.Location = new Point(280, 431);
            txtUsuario.Name = "txtUsuario";
            txtUsuario.Size = new Size(241, 23);
            txtUsuario.TabIndex = 24;
            // 
            // cmbRol
            // 
            cmbRol.FormattingEnabled = true;
            cmbRol.Location = new Point(280, 370);
            cmbRol.Name = "cmbRol";
            cmbRol.Size = new Size(241, 23);
            cmbRol.TabIndex = 30;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(383, 285);
            label5.Name = "label5";
            label5.Size = new Size(36, 15);
            label5.TabIndex = 23;
            label5.Text = "Email";
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(280, 303);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(241, 23);
            txtEmail.TabIndex = 14;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(371, 220);
            label4.Name = "label4";
            label4.Size = new Size(52, 15);
            label4.TabIndex = 22;
            label4.Text = "Telefono";
            // 
            // txtTelefono
            // 
            txtTelefono.Location = new Point(280, 238);
            txtTelefono.Name = "txtTelefono";
            txtTelefono.Size = new Size(241, 23);
            txtTelefono.TabIndex = 13;
            // 
            // frmDetalleEmpleado
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Info;
            ClientSize = new Size(800, 663);
            Controls.Add(cmbRol);
            Controls.Add(label6);
            Controls.Add(label7);
            Controls.Add(label8);
            Controls.Add(txtContraseña);
            Controls.Add(txtUsuario);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnCancelar);
            Controls.Add(btnGuardar);
            Controls.Add(txtApellido);
            Controls.Add(txtDNI);
            Controls.Add(txtEmail);
            Controls.Add(txtTelefono);
            Controls.Add(txtNombre);
            FormBorderStyle = FormBorderStyle.None;
            Name = "frmDetalleEmpleado";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmDetalleUsuario";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label3;
        private Label label2;
        private Label label1;
        private FontAwesome.Sharp.IconButton btnCancelar;
        private FontAwesome.Sharp.IconButton btnGuardar;
        private TextBox txtApellido;
        private TextBox txtDNI;
        private TextBox txtNombre;
        private Label label6;
        private Label label7;
        private Label label8;
        private TextBox txtContraseña;
        private TextBox txtUsuario;
        private ComboBox cmbRol;
        private Label label5;
        private TextBox txtEmail;
        private Label label4;
        private TextBox txtTelefono;
    }
}