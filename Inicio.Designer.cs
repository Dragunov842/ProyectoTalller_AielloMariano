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
            label1 = new Label();
            menuStrip2 = new MenuStrip();
            menuStrip1 = new MenuStrip();
            iconMenuItem5 = new FontAwesome.Sharp.IconMenuItem();
            iconMenuItem4 = new FontAwesome.Sharp.IconMenuItem();
            iconMenuItem3 = new FontAwesome.Sharp.IconMenuItem();
            iconMenuItem2 = new FontAwesome.Sharp.IconMenuItem();
            iconMenuItem1 = new FontAwesome.Sharp.IconMenuItem();
            iconMenuItem6 = new FontAwesome.Sharp.IconMenuItem();
            iconMenuItem7 = new FontAwesome.Sharp.IconMenuItem();
            iconMenuItem8 = new FontAwesome.Sharp.IconMenuItem();
            panel1.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.Info;
            panel1.Controls.Add(label1);
            panel1.Location = new Point(0, -2);
            panel1.Name = "panel1";
            panel1.Size = new Size(993, 66);
            panel1.TabIndex = 0;
            panel1.Paint += panel1_Paint;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 25F);
            label1.ForeColor = SystemColors.ControlText;
            label1.Location = new Point(12, 11);
            label1.Name = "label1";
            label1.Size = new Size(204, 46);
            label1.TabIndex = 0;
            label1.Text = "SportXpresS";
            label1.Click += label1_Click;
            // 
            // menuStrip2
            // 
            menuStrip2.Dock = DockStyle.None;
            menuStrip2.Location = new Point(-216, -129);
            menuStrip2.Name = "menuStrip2";
            menuStrip2.Size = new Size(202, 24);
            menuStrip2.TabIndex = 2;
            menuStrip2.Text = "menuStrip2";
            // 
            // menuStrip1
            // 
            menuStrip1.Dock = DockStyle.None;
            menuStrip1.Items.AddRange(new ToolStripItem[] { iconMenuItem8, iconMenuItem5, iconMenuItem4, iconMenuItem3, iconMenuItem2, iconMenuItem1, iconMenuItem6, iconMenuItem7 });
            menuStrip1.Location = new Point(0, 67);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(572, 71);
            menuStrip1.TabIndex = 3;
            menuStrip1.Text = "menuStrip1";
            // 
            // iconMenuItem5
            // 
            iconMenuItem5.IconChar = FontAwesome.Sharp.IconChar.Check;
            iconMenuItem5.IconColor = Color.Black;
            iconMenuItem5.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconMenuItem5.ImageScaling = ToolStripItemImageScaling.None;
            iconMenuItem5.Name = "iconMenuItem5";
            iconMenuItem5.Size = new Size(77, 67);
            iconMenuItem5.Text = "Asistencias";
            iconMenuItem5.TextImageRelation = TextImageRelation.ImageAboveText;
            iconMenuItem5.Click += iconMenuItem5_Click;
            // 
            // iconMenuItem4
            // 
            iconMenuItem4.IconChar = FontAwesome.Sharp.IconChar.IdCardClip;
            iconMenuItem4.IconColor = Color.Black;
            iconMenuItem4.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconMenuItem4.ImageScaling = ToolStripItemImageScaling.None;
            iconMenuItem4.Name = "iconMenuItem4";
            iconMenuItem4.Size = new Size(83, 67);
            iconMenuItem4.Text = "Membresias";
            iconMenuItem4.TextImageRelation = TextImageRelation.ImageAboveText;
            // 
            // iconMenuItem3
            // 
            iconMenuItem3.IconChar = FontAwesome.Sharp.IconChar.Newspaper;
            iconMenuItem3.IconColor = Color.Black;
            iconMenuItem3.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconMenuItem3.ImageScaling = ToolStripItemImageScaling.None;
            iconMenuItem3.Name = "iconMenuItem3";
            iconMenuItem3.Size = new Size(60, 67);
            iconMenuItem3.Text = "Planes";
            iconMenuItem3.TextImageRelation = TextImageRelation.ImageAboveText;
            // 
            // iconMenuItem2
            // 
            iconMenuItem2.IconChar = FontAwesome.Sharp.IconChar.MoneyBill1Wave;
            iconMenuItem2.IconColor = Color.Black;
            iconMenuItem2.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconMenuItem2.ImageScaling = ToolStripItemImageScaling.None;
            iconMenuItem2.Name = "iconMenuItem2";
            iconMenuItem2.Size = new Size(60, 67);
            iconMenuItem2.Text = "Pagos";
            iconMenuItem2.TextImageRelation = TextImageRelation.ImageAboveText;
            iconMenuItem2.Click += iconMenuItem2_Click;
            // 
            // iconMenuItem1
            // 
            iconMenuItem1.IconChar = FontAwesome.Sharp.IconChar.Ruler;
            iconMenuItem1.IconColor = Color.Black;
            iconMenuItem1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconMenuItem1.ImageScaling = ToolStripItemImageScaling.None;
            iconMenuItem1.Name = "iconMenuItem1";
            iconMenuItem1.Size = new Size(65, 67);
            iconMenuItem1.Text = "Reportes";
            iconMenuItem1.TextImageRelation = TextImageRelation.ImageAboveText;
            // 
            // iconMenuItem6
            // 
            iconMenuItem6.IconChar = FontAwesome.Sharp.IconChar.User;
            iconMenuItem6.IconColor = Color.Black;
            iconMenuItem6.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconMenuItem6.ImageScaling = ToolStripItemImageScaling.None;
            iconMenuItem6.Name = "iconMenuItem6";
            iconMenuItem6.Size = new Size(64, 67);
            iconMenuItem6.Text = "Usuarios";
            iconMenuItem6.TextImageRelation = TextImageRelation.ImageAboveText;
            // 
            // iconMenuItem7
            // 
            iconMenuItem7.IconChar = FontAwesome.Sharp.IconChar.PersonWalkingDashedLineArrowRight;
            iconMenuItem7.IconColor = Color.Black;
            iconMenuItem7.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconMenuItem7.ImageScaling = ToolStripItemImageScaling.None;
            iconMenuItem7.Name = "iconMenuItem7";
            iconMenuItem7.Size = new Size(88, 67);
            iconMenuItem7.Text = "Cerrar Sesion";
            iconMenuItem7.TextImageRelation = TextImageRelation.ImageAboveText;
            // 
            // iconMenuItem8
            // 
            iconMenuItem8.IconChar = FontAwesome.Sharp.IconChar.Dumbbell;
            iconMenuItem8.IconColor = Color.Black;
            iconMenuItem8.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconMenuItem8.ImageScaling = ToolStripItemImageScaling.None;
            iconMenuItem8.Name = "iconMenuItem8";
            iconMenuItem8.Size = new Size(67, 67);
            iconMenuItem8.Text = "Alumnos";
            iconMenuItem8.TextImageRelation = TextImageRelation.ImageAboveText;
            // 
            // Inicio
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(990, 549);
            Controls.Add(panel1);
            Controls.Add(menuStrip2);
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
        private MenuStrip menuStrip2;
        private MenuStrip menuStrip1;
        private FontAwesome.Sharp.IconMenuItem iconMenuItem1;
        private FontAwesome.Sharp.IconMenuItem iconMenuItem5;
        private FontAwesome.Sharp.IconMenuItem iconMenuItem4;
        private FontAwesome.Sharp.IconMenuItem iconMenuItem3;
        private FontAwesome.Sharp.IconMenuItem iconMenuItem2;
        private FontAwesome.Sharp.IconMenuItem iconMenuItem6;
        private FontAwesome.Sharp.IconMenuItem iconMenuItem7;
        private FontAwesome.Sharp.IconMenuItem iconMenuItem8;
    }
}