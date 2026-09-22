namespace CapaPresentacion
{
    partial class frmDetallePlanes
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
            txtNombre = new TextBox();
            txtPrecio = new TextBox();
            txtDescripcion = new TextBox();
            btnGuardar = new FontAwesome.Sharp.IconButton();
            btnCancelar = new FontAwesome.Sharp.IconButton();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            txtDuracion = new TextBox();
            label5 = new Label();
            SuspendLayout();
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(270, 45);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(241, 23);
            txtNombre.TabIndex = 35;
            // 
            // txtPrecio
            // 
            txtPrecio.Location = new Point(270, 178);
            txtPrecio.Name = "txtPrecio";
            txtPrecio.Size = new Size(241, 23);
            txtPrecio.TabIndex = 32;
            // 
            // txtDescripcion
            // 
            txtDescripcion.Location = new Point(270, 111);
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.Size = new Size(241, 23);
            txtDescripcion.TabIndex = 34;
            // 
            // btnGuardar
            // 
            btnGuardar.BackColor = Color.FromArgb(192, 255, 192);
            btnGuardar.Cursor = Cursors.Hand;
            btnGuardar.IconChar = FontAwesome.Sharp.IconChar.Check;
            btnGuardar.IconColor = Color.Black;
            btnGuardar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnGuardar.ImageAlign = ContentAlignment.TopCenter;
            btnGuardar.Location = new Point(270, 309);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(75, 67);
            btnGuardar.TabIndex = 36;
            btnGuardar.Text = "Guardar";
            btnGuardar.TextAlign = ContentAlignment.BottomCenter;
            btnGuardar.UseVisualStyleBackColor = false;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.BackColor = Color.FromArgb(255, 128, 128);
            btnCancelar.Cursor = Cursors.Hand;
            btnCancelar.IconChar = FontAwesome.Sharp.IconChar.Cancel;
            btnCancelar.IconColor = Color.Black;
            btnCancelar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnCancelar.ImageAlign = ContentAlignment.TopCenter;
            btnCancelar.Location = new Point(436, 309);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(75, 67);
            btnCancelar.TabIndex = 37;
            btnCancelar.Text = "Cancelar";
            btnCancelar.TextAlign = ContentAlignment.BottomCenter;
            btnCancelar.UseVisualStyleBackColor = false;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(361, 27);
            label2.Name = "label2";
            label2.Size = new Size(51, 15);
            label2.TabIndex = 38;
            label2.Text = "Nombre";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(359, 93);
            label3.Name = "label3";
            label3.Size = new Size(69, 15);
            label3.TabIndex = 39;
            label3.Text = "Descripcion";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(369, 160);
            label4.Name = "label4";
            label4.Size = new Size(40, 15);
            label4.TabIndex = 40;
            label4.Text = "Precio";
            label4.Click += label4_Click;
            // 
            // txtDuracion
            // 
            txtDuracion.Location = new Point(270, 243);
            txtDuracion.Name = "txtDuracion";
            txtDuracion.Size = new Size(241, 23);
            txtDuracion.TabIndex = 33;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(349, 225);
            label5.Name = "label5";
            label5.Size = new Size(88, 15);
            label5.TabIndex = 41;
            label5.Text = "Duracion (Dias)";
            // 
            // frmDetallePlanes
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Info;
            ClientSize = new Size(800, 409);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(btnCancelar);
            Controls.Add(btnGuardar);
            Controls.Add(txtNombre);
            Controls.Add(txtDescripcion);
            Controls.Add(txtDuracion);
            Controls.Add(txtPrecio);
            FormBorderStyle = FormBorderStyle.None;
            Name = "frmDetallePlanes";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmDetallePlanes";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtNombre;
        private TextBox txtPrecio;
        private TextBox txtDescripcion;
        private FontAwesome.Sharp.IconButton btnGuardar;
        private FontAwesome.Sharp.IconButton btnCancelar;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextBox txtDuracion;
        private Label label5;
    }
}