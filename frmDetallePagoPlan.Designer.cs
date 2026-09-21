namespace CapaPresentacion
{
    partial class frmDetallePagoPlan
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
            cmbPlan = new ComboBox();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            txtDuracion = new TextBox();
            txtPrecio = new TextBox();
            btnCancelar = new FontAwesome.Sharp.IconButton();
            btnPagar = new FontAwesome.Sharp.IconButton();
            SuspendLayout();
            // 
            // cmbPlan
            // 
            cmbPlan.FormattingEnabled = true;
            cmbPlan.Location = new Point(280, 97);
            cmbPlan.Name = "cmbPlan";
            cmbPlan.Size = new Size(241, 23);
            cmbPlan.TabIndex = 46;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(361, 205);
            label6.Name = "label6";
            label6.Size = new Size(88, 15);
            label6.TabIndex = 45;
            label6.Text = "Duracion (Dias)";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(371, 140);
            label7.Name = "label7";
            label7.Size = new Size(57, 15);
            label7.TabIndex = 44;
            label7.Text = "Precio ($)";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(385, 79);
            label8.Name = "label8";
            label8.Size = new Size(30, 15);
            label8.TabIndex = 43;
            label8.Text = "Plan";
            // 
            // txtDuracion
            // 
            txtDuracion.Location = new Point(280, 223);
            txtDuracion.Name = "txtDuracion";
            txtDuracion.ReadOnly = true;
            txtDuracion.Size = new Size(241, 23);
            txtDuracion.TabIndex = 42;
            // 
            // txtPrecio
            // 
            txtPrecio.Location = new Point(280, 158);
            txtPrecio.Name = "txtPrecio";
            txtPrecio.ReadOnly = true;
            txtPrecio.Size = new Size(241, 23);
            txtPrecio.TabIndex = 41;
            // 
            // btnCancelar
            // 
            btnCancelar.BackColor = Color.FromArgb(255, 128, 128);
            btnCancelar.Cursor = Cursors.Hand;
            btnCancelar.IconChar = FontAwesome.Sharp.IconChar.Cancel;
            btnCancelar.IconColor = Color.Black;
            btnCancelar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnCancelar.ImageAlign = ContentAlignment.TopCenter;
            btnCancelar.Location = new Point(446, 305);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(75, 67);
            btnCancelar.TabIndex = 40;
            btnCancelar.Text = "Cancelar";
            btnCancelar.TextAlign = ContentAlignment.BottomCenter;
            btnCancelar.UseVisualStyleBackColor = false;
            // 
            // btnPagar
            // 
            btnPagar.BackColor = Color.FromArgb(192, 255, 192);
            btnPagar.Cursor = Cursors.Hand;
            btnPagar.IconChar = FontAwesome.Sharp.IconChar.MoneyCheckDollar;
            btnPagar.IconColor = Color.Black;
            btnPagar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnPagar.ImageAlign = ContentAlignment.TopCenter;
            btnPagar.Location = new Point(280, 305);
            btnPagar.Name = "btnPagar";
            btnPagar.Size = new Size(75, 67);
            btnPagar.TabIndex = 39;
            btnPagar.Text = "Pagar";
            btnPagar.TextAlign = ContentAlignment.BottomCenter;
            btnPagar.UseVisualStyleBackColor = false;
            btnPagar.Click += btnPagar_Click;
            // 
            // frmDetallePagoPlan
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Info;
            ClientSize = new Size(800, 450);
            Controls.Add(cmbPlan);
            Controls.Add(label6);
            Controls.Add(label7);
            Controls.Add(label8);
            Controls.Add(txtDuracion);
            Controls.Add(txtPrecio);
            Controls.Add(btnCancelar);
            Controls.Add(btnPagar);
            Name = "frmDetallePagoPlan";
            Text = "frmDetallePagoPlan";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cmbPlan;
        private Label label6;
        private Label label7;
        private Label label8;
        private TextBox txtDuracion;
        private TextBox txtPrecio;
        private FontAwesome.Sharp.IconButton btnCancelar;
        private FontAwesome.Sharp.IconButton btnPagar;
    }
}