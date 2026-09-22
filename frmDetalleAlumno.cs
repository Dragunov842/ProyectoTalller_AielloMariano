using CapaEntidad;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class frmDetalleAlumno : Form
    {

        private int idAlumno = 0;
        private int idPersona = 0;

        public frmDetalleAlumno()
        {
            InitializeComponent();
        }

        public frmDetalleAlumno(Alumno objAlumno)
        {
            InitializeComponent();

            txtNombre.Text = objAlumno.oPersona.Nombre;
            txtApellido.Text = objAlumno.oPersona.Apellido;
            txtDNI.Text = objAlumno.oPersona.DNI;
            txtTelefono.Text = objAlumno.oPersona.Telefono;
            txtEmail.Text = objAlumno.oPersona.Email;

            idAlumno = objAlumno.idAlumno;
            idPersona = objAlumno.oPersona.idPersona;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Alumno objAlumno = new Alumno()
                {
                    idAlumno = idAlumno,

                    oPersona = new Persona()
                    {
                        idPersona = idPersona,
                        Nombre = txtNombre.Text,
                        Apellido = txtApellido.Text,
                        DNI = txtDNI.Text,
                        Telefono = txtTelefono.Text,
                        Email = txtEmail.Text
                    }
                };

                if (idAlumno == 0)
                {
                    new CN_Alumno().Registrar(objAlumno);

                    MessageBox.Show(
                        "Alumno registrado correctamente.",
                        "Registro",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
                else
                {
                    bool respuesta = new CN_Alumno().Editar(objAlumno);

                    if (respuesta)
                    {
                        MessageBox.Show(
                            "Alumno editado correctamente.",
                            "Edición",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );
                    }
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
