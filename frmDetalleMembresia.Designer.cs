namespace CapaPresentacion
{
    partial class frmDetalleMembresia
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
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            btnCancelar = new FontAwesome.Sharp.IconButton();
            btnGuardar = new FontAwesome.Sharp.IconButton();
            txtNombre = new TextBox();
            txtDescripcion = new TextBox();
            txtDuracion = new TextBox();
            txtPrecio = new TextBox();
            label2 = new Label();
            SuspendLayout();
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(359, 240);
            label5.Name = "label5";
            label5.Size = new Size(88, 15);
            label5.TabIndex = 50;
            label5.Text = "Duracion (Dias)";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(379, 175);
            label4.Name = "label4";
            label4.Size = new Size(40, 15);
            label4.TabIndex = 49;
            label4.Text = "Precio";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(369, 108);
            label3.Name = "label3";
            label3.Size = new Size(69, 15);
            label3.TabIndex = 48;
            label3.Text = "Descripcion";
            // 
            // btnCancelar
            // 
            btnCancelar.BackColor = Color.FromArgb(255, 128, 128);
            btnCancelar.Cursor = Cursors.Hand;
            btnCancelar.IconChar = FontAwesome.Sharp.IconChar.Cancel;
            btnCancelar.IconColor = Color.Black;
            btnCancelar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnCancelar.ImageAlign = ContentAlignment.TopCenter;
            btnCancelar.Location = new Point(446, 324);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(75, 67);
            btnCancelar.TabIndex = 47;
            btnCancelar.Text = "Cancelar";
            btnCancelar.TextAlign = ContentAlignment.BottomCenter;
            btnCancelar.UseVisualStyleBackColor = false;
            // 
            // btnGuardar
            // 
            btnGuardar.BackColor = Color.FromArgb(192, 255, 192);
            btnGuardar.Cursor = Cursors.Hand;
            btnGuardar.IconChar = FontAwesome.Sharp.IconChar.Check;
            btnGuardar.IconColor = Color.Black;
            btnGuardar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnGuardar.ImageAlign = ContentAlignment.TopCenter;
            btnGuardar.Location = new Point(280, 324);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(75, 67);
            btnGuardar.TabIndex = 46;
            btnGuardar.Text = "Guardar";
            btnGuardar.TextAlign = ContentAlignment.BottomCenter;
            btnGuardar.UseVisualStyleBackColor = false;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(280, 60);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(241, 23);
            txtNombre.TabIndex = 45;
            // 
            // txtDescripcion
            // 
            txtDescripcion.Location = new Point(280, 126);
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.Size = new Size(241, 23);
            txtDescripcion.TabIndex = 44;
            // 
            // txtDuracion
            // 
            txtDuracion.Location = new Point(280, 258);
            txtDuracion.Name = "txtDuracion";
            txtDuracion.Size = new Size(241, 23);
            txtDuracion.TabIndex = 43;
            // 
            // txtPrecio
            // 
            txtPrecio.Location = new Point(280, 193);
            txtPrecio.Name = "txtPrecio";
            txtPrecio.Size = new Size(241, 23);
            txtPrecio.TabIndex = 42;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(379, 42);
            label2.Name = "label2";
            label2.Size = new Size(51, 15);
            label2.TabIndex = 51;
            label2.Text = "Nombre";
            // 
            // frmDetalleMembresia
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Info;
            ClientSize = new Size(800, 450);
            Controls.Add(label2);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(btnCancelar);
            Controls.Add(btnGuardar);
            Controls.Add(txtNombre);
            Controls.Add(txtDescripcion);
            Controls.Add(txtDuracion);
            Controls.Add(txtPrecio);
            Name = "frmDetalleMembresia";
            Text = "frmDetalleMembresia";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label5;
        private Label label4;
        private Label label3;
        private FontAwesome.Sharp.IconButton btnCancelar;
        private FontAwesome.Sharp.IconButton btnGuardar;
        private TextBox txtNombre;
        private TextBox txtDescripcion;
        private TextBox txtDuracion;
        private TextBox txtPrecio;
        private Label label2;
    }
}