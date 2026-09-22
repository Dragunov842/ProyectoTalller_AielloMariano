using CapaDatos;
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
    public partial class frmDetalleEmpleado : Form
    {

        private int idEmpleado = 0;
        private int idPersona = 0;

        public frmDetalleEmpleado()
        {
            InitializeComponent();

            CargarRoles();
        }

        public frmDetalleEmpleado(Empleado objEmpleado)
        {
            InitializeComponent();

            txtContraseña.UseSystemPasswordChar = true;

            CargarRoles();

            txtNombre.Text = objEmpleado.oPersona.Nombre;
            txtApellido.Text = objEmpleado.oPersona.Apellido;
            txtDNI.Text = objEmpleado.oPersona.DNI;
            txtTelefono.Text = objEmpleado.oPersona.Telefono;
            txtEmail.Text = objEmpleado.oPersona.Email;

            txtUsuario.Text = objEmpleado.Usuario;

            cmbRol.SelectedValue = objEmpleado.oRol.idRol;

            idEmpleado = objEmpleado.idEmpleado;
            idPersona = objEmpleado.oPersona.idPersona;
        }

        private void CargarRoles()
        {
            cmbRol.Items.Clear();

            cmbRol.Items.Add(new Rol()
            {
                idRol = 2,
                Nombre = "Recepcionista"
            });

            cmbRol.Items.Add(new Rol()
            {
                idRol = 3,
                Nombre = "Entrenador"
            });

            cmbRol.DisplayMember = "Nombre";
            cmbRol.ValueMember = "idRol";

            cmbRol.SelectedIndex = 0;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Empleado objEmpleado = new Empleado()
                {
                    idEmpleado = idEmpleado,

                    Usuario = txtUsuario.Text,
                    Contraseña = txtContraseña.Text,

                    oPersona = new Persona()
                    {
                        idPersona = idPersona,
                        Nombre = txtNombre.Text,
                        Apellido = txtApellido.Text,
                        DNI = txtDNI.Text,
                        Telefono = txtTelefono.Text,
                        Email = txtEmail.Text
                    },

                    oRol = (Rol)cmbRol.SelectedItem
                };

                if (idEmpleado == 0)
                {
                    new CN_Empleado().Registrar(objEmpleado);

                    MessageBox.Show(
                        "Empleado registrado correctamente.",
                        "Registro",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else
                {
                    bool respuesta = new CN_Empleado().Editar(objEmpleado);

                    if (respuesta)
                    {
                        MessageBox.Show(
                            "Empleado editado correctamente.",
                            "Edición",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
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
                    MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
