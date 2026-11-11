namespace CapaPresentacion
{
    partial class frmBackup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBackup));
            menuStrip1 = new MenuStrip();
            subirBackup = new FontAwesome.Sharp.IconMenuItem();
            copiarBackup = new FontAwesome.Sharp.IconMenuItem();
            label2 = new Label();
            Username = new Label();
            txtContraseña2 = new TextBox();
            txtContraseña1 = new TextBox();
            label1 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            txtUsuario1 = new TextBox();
            txtUsuario2 = new TextBox();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { subirBackup, copiarBackup });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.ShowItemToolTips = true;
            menuStrip1.Size = new Size(1035, 71);
            menuStrip1.TabIndex = 4;
            menuStrip1.Text = "menuStrip1";
            // 
            // subirBackup
            // 
            subirBackup.IconChar = FontAwesome.Sharp.IconChar.Upload;
            subirBackup.IconColor = Color.Black;
            subirBackup.IconFont = FontAwesome.Sharp.IconFont.Auto;
            subirBackup.ImageScaling = ToolStripItemImageScaling.None;
            subirBackup.Name = "subirBackup";
            subirBackup.Size = new Size(88, 67);
            subirBackup.Text = "Subir Backup";
            subirBackup.TextImageRelation = TextImageRelation.ImageAboveText;
            subirBackup.Click += subirBackup_Click;
            // 
            // copiarBackup
            // 
            copiarBackup.IconChar = FontAwesome.Sharp.IconChar.TurnDown;
            copiarBackup.IconColor = Color.Black;
            copiarBackup.IconFont = FontAwesome.Sharp.IconFont.Auto;
            copiarBackup.ImageScaling = ToolStripItemImageScaling.None;
            copiarBackup.Name = "copiarBackup";
            copiarBackup.Size = new Size(96, 67);
            copiarBackup.Text = "Cargar Backup";
            copiarBackup.TextImageRelation = TextImageRelation.ImageAboveText;
            copiarBackup.Click += copiarBackup_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = SystemColors.ActiveCaptionText;
            label2.Location = new Point(690, 452);
            label2.Name = "label2";
            label2.Size = new Size(191, 15);
            label2.TabIndex = 8;
            label2.Text = "Ingrese la contraseña de supervisor";
            // 
            // Username
            // 
            Username.AutoSize = true;
            Username.Location = new Point(156, 452);
            Username.Name = "Username";
            Username.Size = new Size(211, 15);
            Username.TabIndex = 7;
            Username.Text = "Ingrese la contraseña de administrador";
            Username.TextAlign = ContentAlignment.TopCenter;
            Username.Click += Username_Click;
            // 
            // txtContraseña2
            // 
            txtContraseña2.Location = new Point(667, 470);
            txtContraseña2.Name = "txtContraseña2";
            txtContraseña2.Size = new Size(239, 23);
            txtContraseña2.TabIndex = 6;
            txtContraseña2.UseSystemPasswordChar = true;
            // 
            // txtContraseña1
            // 
            txtContraseña1.Location = new Point(140, 470);
            txtContraseña1.Name = "txtContraseña1";
            txtContraseña1.Size = new Size(239, 23);
            txtContraseña1.TabIndex = 5;
            txtContraseña1.UseSystemPasswordChar = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 20F);
            label1.ForeColor = Color.Red;
            label1.Location = new Point(424, 91);
            label1.Name = "label1";
            label1.Size = new Size(165, 37);
            label1.TabIndex = 9;
            label1.Text = "¡Importante!";
            label1.TextAlign = ContentAlignment.TopCenter;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F);
            label3.ForeColor = Color.Red;
            label3.Location = new Point(67, 157);
            label3.Name = "label3";
            label3.Size = new Size(893, 84);
            label3.TabIndex = 10;
            label3.Text = resources.GetString("label3.Text");
            label3.TextAlign = ContentAlignment.TopCenter;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(156, 351);
            label4.Name = "label4";
            label4.Size = new Size(208, 15);
            label4.TabIndex = 13;
            label4.Text = "Ingrese el username del administrador\r\n";
            label4.TextAlign = ContentAlignment.TopCenter;
            label4.Click += label4_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(693, 351);
            label5.Name = "label5";
            label5.Size = new Size(188, 15);
            label5.TabIndex = 14;
            label5.Text = "Ingrese el username del supervisor";
            label5.TextAlign = ContentAlignment.TopCenter;
            // 
            // txtUsuario1
            // 
            txtUsuario1.Location = new Point(140, 369);
            txtUsuario1.Name = "txtUsuario1";
            txtUsuario1.Size = new Size(239, 23);
            txtUsuario1.TabIndex = 15;
            // 
            // txtUsuario2
            // 
            txtUsuario2.Location = new Point(667, 369);
            txtUsuario2.Name = "txtUsuario2";
            txtUsuario2.Size = new Size(239, 23);
            txtUsuario2.TabIndex = 16;
            // 
            // frmBackup
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Info;
            ClientSize = new Size(1035, 626);
            Controls.Add(txtUsuario2);
            Controls.Add(txtUsuario1);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label1);
            Controls.Add(label2);
            Controls.Add(Username);
            Controls.Add(txtContraseña2);
            Controls.Add(txtContraseña1);
            Controls.Add(menuStrip1);
            Name = "frmBackup";
            Text = "frmBackup";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private FontAwesome.Sharp.IconMenuItem subirBackup;
        private FontAwesome.Sharp.IconMenuItem copiarBackup;
        private Label label2;
        private Label Username;
        private TextBox txtContraseña2;
        private TextBox txtContraseña1;
        private Label label1;
        private Label label3;
        private Label label4;
        private Label label5;
        private TextBox txtUsuario1;
        private TextBox txtUsuario2;
    }
}