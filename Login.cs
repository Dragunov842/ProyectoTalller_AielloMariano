using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using CapaNegocio;  
using CapaEntidad;

namespace CapaPresentacion
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();

            this.ControlBox = false;

        }

        private void iconButton1_Click(object sender, EventArgs e)
        {

            List<Empleado> lista = new CN_Empleado().Listar();


            Empleado oempleado = new CN_Empleado().Listar().Where(x => x.Usuario == txtUsuario.Text && x.Contraseña == txtContraseña.Text).FirstOrDefault();

            if (oempleado != null)
            {
                Inicio form = new Inicio(oempleado);

                form.Show();
                this.Hide();

                form.FormClosing += frm_closing;

            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos");
            }
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void frm_closing(object sender, FormClosingEventArgs e)
        {
            txtUsuario.Text = "";
            txtContraseña.Text = "";
            this.Show();
        }

        private void DNI_Click(object sender, EventArgs e)
        {

        }
    }
}