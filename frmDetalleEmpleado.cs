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

            ConfigurarValidaciones();

            txtDNI.Leave += (sender, e) => BuscarEmpleadoPorDNI();

            toolTipMensaje.SetToolTip(btnGuardar, "Guardar los datos del empleado." +
            Environment.NewLine +
            "Nota: Asegúrese de que todos los campos estén correctamente escritos antes de guardar." +
            "Si ingresa un DNI que ya existe, se completarán los datos automáticamente y no se podra registrar al empleado.");

            toolTipMensaje.SetToolTip(btnCancelar, "Cancelar la operación y cerrar el formulario.");
        }

        public frmDetalleEmpleado(Empleado objEmpleado)
        {
            InitializeComponent();

            txtContraseña.UseSystemPasswordChar = true;

            CargarRoles();

            ConfigurarValidaciones();

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

        private void ConfigurarValidaciones()
        {
            txtDNI.MaxLength = 16;
            txtTelefono.MaxLength = 20;
            txtNombre.MaxLength = 50;
            txtApellido.MaxLength = 50;
            txtEmail.MaxLength = 50;
            txtUsuario.MaxLength = 50;
            txtContraseña.MaxLength = 100;

            txtDNI.KeyPress += SoloNumeros_KeyPress;
            txtTelefono.KeyPress += SoloNumeros_KeyPress;

            txtNombre.KeyPress += SoloLetras_KeyPress;
            txtApellido.KeyPress += SoloLetras_KeyPress;
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtDNI.Text))
            {
                MessageBox.Show("Debe ingresar el DNI.");
                txtDNI.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Debe ingresar el nombre.");
                txtNombre.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtApellido.Text))
            {
                MessageBox.Show("Debe ingresar el apellido.");
                txtApellido.Focus();
                return false;
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(
                txtDNI.Text.Trim(), @"^[0-9]+$"))
            {
                MessageBox.Show("El DNI solamente puede contener números.");
                txtDNI.Focus();
                return false;
            }

            if (!string.IsNullOrWhiteSpace(txtTelefono.Text) &&
                !System.Text.RegularExpressions.Regex.IsMatch(
                    txtTelefono.Text.Trim(), @"^[0-9]+$"))
            {
                MessageBox.Show("El teléfono solamente puede contener números.");
                txtTelefono.Focus();
                return false;
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(
                txtNombre.Text.Trim(), @"^[\p{L}\s'-]+$"))
            {
                MessageBox.Show("El nombre contiene caracteres no válidos.");
                txtNombre.Focus();
                return false;
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(
                txtApellido.Text.Trim(), @"^[\p{L}\s'-]+$"))
            {
                MessageBox.Show("El apellido contiene caracteres no válidos.");
                txtApellido.Focus();
                return false;
            }

            return true;
        }

        private void SoloNumeros_KeyPress(
            object sender,
            KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) &&
                !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void SoloLetras_KeyPress(
            object sender,
            KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) &&
                !char.IsLetter(e.KeyChar) &&
                !char.IsWhiteSpace(e.KeyChar) &&
                e.KeyChar != '-' &&
                e.KeyChar != '\'')
            {
                e.Handled = true;
            }
        }

        private bool ValidarDNIEnEdicion()
        {
            // Esta validación solamente corresponde a la edición.
            if (idEmpleado == 0)
                return true;

            CN_Empleado negocio = new CN_Empleado();

            Persona personaExistente =
                negocio.BuscarPersonaPorDNI(txtDNI.Text.Trim());

            // Si el DNI pertenece a otra persona, no permitimos continuar.
            if (personaExistente != null &&
                personaExistente.idPersona != idPersona)
            {
                MessageBox.Show(
                    "El DNI ingresado ya se encuentra registrado por otra persona. " +
                    "Ingrese un DNI diferente.",
                    "DNI duplicado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtDNI.Focus();
                txtDNI.SelectAll();

                return false;
            }

            return true;
        }


        private void BuscarEmpleadoPorDNI()
        {
            string dni = txtDNI.Text.Trim();

            if (string.IsNullOrWhiteSpace(dni))
                return;

            CN_Empleado negocio = new CN_Empleado();

            Persona persona = negocio.BuscarPersonaPorDNI(dni);

            if (persona == null)
            {
                // No existe: dejamos el formulario preparado
                // para registrar una persona nueva.
                idPersona = 0;

                txtNombre.Clear();
                txtApellido.Clear();
                txtTelefono.Clear();
                txtEmail.Clear();

                return;
            }

            // La persona ya existe: recuperamos sus datos.
            idPersona = persona.idPersona;

            txtNombre.Text = persona.Nombre;
            txtApellido.Text = persona.Apellido;
            txtTelefono.Text = persona.Telefono;
            txtEmail.Text = persona.Email;

            // Comprobamos si ya está registrada como empleado.
            if (negocio.ExisteEmpleadoPorIdPersona(persona.idPersona))
            {
                MessageBox.Show(
                    "Esta persona ya se encuentra registrada como empleado.",
                    "Registro duplicado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                btnGuardar.Enabled = false;
            }
            else
            {
                btnGuardar.Enabled = true;
            }
        }


        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos())
                return;

            if (!ValidarDNIEnEdicion())
                return;

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
