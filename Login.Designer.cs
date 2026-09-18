namespace CapaPresentacion
{
    partial class Login
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
            panel1 = new Panel();
            Titulo = new Label();
            iconPictureBox1 = new FontAwesome.Sharp.IconPictureBox();
            txtUsuario = new TextBox();
            txtContraseña = new TextBox();
            Username = new Label();
            label2 = new Label();
            Ingresar = new FontAwesome.Sharp.IconButton();
            Salir = new FontAwesome.Sharp.IconButton();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panel1.BackColor = SystemColors.Info;
            panel1.Controls.Add(Titulo);
            panel1.Controls.Add(iconPictureBox1);
            panel1.Location = new Point(2, 1);
            panel1.Name = "panel1";
            panel1.Size = new Size(301, 449);
            panel1.TabIndex = 0;
            // 
            // Titulo
            // 
            Titulo.Font = new Font("Segoe UI", 25F);
            Titulo.Location = new Point(45, 270);
            Titulo.Name = "Titulo";
            Titulo.Size = new Size(210, 43);
            Titulo.TabIndex = 1;
            Titulo.Text = "SportXpress";
            // 
            // iconPictureBox1
            // 
            iconPictureBox1.BackColor = SystemColors.Info;
            iconPictureBox1.Dock = DockStyle.Fill;
            iconPictureBox1.ForeColor = SystemColors.ControlText;
            iconPictureBox1.IconChar = FontAwesome.Sharp.IconChar.Dumbbell;
            iconPictureBox1.IconColor = SystemColors.ControlText;
            iconPictureBox1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconPictureBox1.IconSize = 301;
            iconPictureBox1.Location = new Point(0, 0);
            iconPictureBox1.Name = "iconPictureBox1";
            iconPictureBox1.Size = new Size(301, 449);
            iconPictureBox1.TabIndex = 0;
            iconPictureBox1.TabStop = false;
            // 
            // txtUsuario
            // 
            txtUsuario.Location = new Point(429, 113);
            txtUsuario.Name = "txtUsuario";
            txtUsuario.Size = new Size(239, 23);
            txtUsuario.TabIndex = 1;
            txtUsuario.TextChanged += textBox1_TextChanged;
            // 
            // txtContraseña
            // 
            txtContraseña.Location = new Point(429, 215);
            txtContraseña.Name = "txtContraseña";
            txtContraseña.PasswordChar = '*';
            txtContraseña.Size = new Size(239, 23);
            txtContraseña.TabIndex = 2;
            txtContraseña.TextChanged += textBox2_TextChanged;
            // 
            // Username
            // 
            Username.AutoSize = true;
            Username.Location = new Point(504, 95);
            Username.Name = "Username";
            Username.Size = new Size(103, 15);
            Username.TabIndex = 3;
            Username.Text = "Ingrese su Usuario";
            Username.TextAlign = ContentAlignment.TopCenter;
            Username.Click += DNI_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = SystemColors.ActiveCaptionText;
            label2.Location = new Point(485, 197);
            label2.Name = "label2";
            label2.Size = new Size(123, 15);
            label2.TabIndex = 4;
            label2.Text = "Ingrese su Contraseña";
            // 
            // Ingresar
            // 
            Ingresar.BackColor = Color.FromArgb(192, 255, 192);
            Ingresar.Cursor = Cursors.Hand;
            Ingresar.IconChar = FontAwesome.Sharp.IconChar.DoorOpen;
            Ingresar.IconColor = Color.Black;
            Ingresar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            Ingresar.ImageAlign = ContentAlignment.MiddleLeft;
            Ingresar.Location = new Point(429, 310);
            Ingresar.Name = "Ingresar";
            Ingresar.Size = new Size(102, 36);
            Ingresar.TabIndex = 5;
            Ingresar.Text = "Ingresar";
            Ingresar.TextAlign = ContentAlignment.MiddleRight;
            Ingresar.UseVisualStyleBackColor = false;
            Ingresar.Click += iconButton1_Click;
            // 
            // Salir
            // 
            Salir.BackColor = Color.FromArgb(255, 128, 128);
            Salir.Cursor = Cursors.Hand;
            Salir.IconChar = FontAwesome.Sharp.IconChar.SquareXmark;
            Salir.IconColor = Color.Black;
            Salir.IconFont = FontAwesome.Sharp.IconFont.Auto;
            Salir.IconSize = 35;
            Salir.ImageAlign = ContentAlignment.MiddleLeft;
            Salir.Location = new Point(566, 310);
            Salir.Name = "Salir";
            Salir.Size = new Size(102, 36);
            Salir.TabIndex = 6;
            Salir.Text = "Salir";
            Salir.TextAlign = ContentAlignment.MiddleRight;
            Salir.UseVisualStyleBackColor = false;
            Salir.Click += iconButton2_Click;
            // 
            // Login
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(800, 450);
            Controls.Add(Salir);
            Controls.Add(Ingresar);
            Controls.Add(label2);
            Controls.Add(Username);
            Controls.Add(txtContraseña);
            Controls.Add(txtUsuario);
            Controls.Add(panel1);
            Name = "Login";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Login";
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)iconPictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Label Titulo;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox1;
        private TextBox txtUsuario;
        private TextBox txtContraseña;
        private Label Username;
        private Label label2;
        private FontAwesome.Sharp.IconButton Ingresar;
        private FontAwesome.Sharp.IconButton Salir;
    }
}