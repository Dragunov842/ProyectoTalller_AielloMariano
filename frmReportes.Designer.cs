namespace CapaPresentacion
{
    partial class frmReportes
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
            dataGridMembresias = new DataGridView();
            dataGridAsistencias = new DataGridView();
            dataGridPlanes = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dataGridMembresias).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridAsistencias).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridPlanes).BeginInit();
            SuspendLayout();
            // 
            // dataGridMembresias
            // 
            dataGridMembresias.AllowUserToAddRows = false;
            dataGridMembresias.BackgroundColor = SystemColors.ButtonHighlight;
            dataGridMembresias.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridMembresias.Location = new Point(12, 52);
            dataGridMembresias.Name = "dataGridMembresias";
            dataGridMembresias.Size = new Size(551, 150);
            dataGridMembresias.TabIndex = 1;
            // 
            // dataGridAsistencias
            // 
            dataGridAsistencias.AllowUserToAddRows = false;
            dataGridAsistencias.BackgroundColor = SystemColors.ButtonHighlight;
            dataGridAsistencias.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridAsistencias.Location = new Point(683, 52);
            dataGridAsistencias.Name = "dataGridAsistencias";
            dataGridAsistencias.Size = new Size(244, 498);
            dataGridAsistencias.TabIndex = 3;
            // 
            // dataGridPlanes
            // 
            dataGridPlanes.AllowUserToAddRows = false;
            dataGridPlanes.AllowUserToDeleteRows = false;
            dataGridPlanes.BackgroundColor = SystemColors.ButtonHighlight;
            dataGridPlanes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridPlanes.Location = new Point(12, 400);
            dataGridPlanes.Name = "dataGridPlanes";
            dataGridPlanes.Size = new Size(551, 150);
            dataGridPlanes.TabIndex = 4;
            // 
            // frmReportes
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Info;
            ClientSize = new Size(959, 593);
            Controls.Add(dataGridPlanes);
            Controls.Add(dataGridAsistencias);
            Controls.Add(dataGridMembresias);
            FormBorderStyle = FormBorderStyle.None;
            Name = "frmReportes";
            Text = "frmReportes";
            ((System.ComponentModel.ISupportInitialize)dataGridMembresias).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridAsistencias).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridPlanes).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridMembresias;
        private DataGridView dataGridAsistencias;
        private DataGridView dataGridPlanes;
    }
}