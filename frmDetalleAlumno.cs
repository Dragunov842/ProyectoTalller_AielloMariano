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

            ConfigurarValidaciones();

            txtDNI.Leave += (sender, e) => BuscarAlumnoPorDNI();

            toolTipMensaje.SetToolTip(btnGuardar, "Guardar los datos del alumno." +
                Environment.NewLine +
                "Nota: Asegúrese de que todos los campos estén correctamente escritos antes de guardar." +
                "Si ingresa un DNI que ya existe, se completarán los datos automáticamente y no se podra registrar al alumno.");

            toolTipMensaje.SetToolTip(btnCancelar, "Cancelar la operación y cerrar el formulario.");
        }

        public frmDetalleAlumno(Alumno objAlumno)
        {
            InitializeComponent();

            ConfigurarValidaciones();

            txtNombre.Text = objAlumno.oPersona.Nombre;
            txtApellido.Text = objAlumno.oPersona.Apellido;
            txtDNI.Text = objAlumno.oPersona.DNI;
            txtTelefono.Text = objAlumno.oPersona.Telefono;
            txtEmail.Text = objAlumno.oPersona.Email;

            idAlumno = objAlumno.idAlumno;
            idPersona = objAlumno.oPersona.idPersona;
        }

        private void BuscarAlumnoPorDNI()
        {
            string dni = txtDNI.Text.Trim();

           
            btnGuardar.Enabled = true;
            idPersona = 0;

            if (string.IsNullOrWhiteSpace(dni))
            {
                txtNombre.Clear();
                txtApellido.Clear();
                txtTelefono.Clear();
                txtEmail.Clear();
                return;
            }

            CN_Alumno negocio = new CN_Alumno();

            Persona persona = negocio.BuscarPersonaPorDNI(dni);

            
            if (persona == null)
            {
                txtNombre.Clear();
                txtApellido.Clear();
                txtTelefono.Clear();
                txtEmail.Clear();

                return;
            }

            
            idPersona = persona.idPersona;

            txtNombre.Text = persona.Nombre;
            txtApellido.Text = persona.Apellido;
            txtTelefono.Text = persona.Telefono;
            txtEmail.Text = persona.Email;

            
            if (negocio.ExisteAlumnoPorIdPersona(persona.idPersona))
            {
                MessageBox.Show(
                    "Esta persona ya se encuentra registrada como alumno.",
                    "Registro duplicado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                btnGuardar.Enabled = false;
            }
        }

        private bool ValidarDNIEnEdicion()
        {
            // Si estamos registrando un alumno nuevo,
            // esta validación no es necesaria.
            if (idAlumno == 0)
                return true;

            CN_Alumno negocio = new CN_Alumno();

            Persona personaExistente =
                negocio.BuscarPersonaPorDNI(txtDNI.Text.Trim());

            // Si el DNI pertenece a otra persona, se bloquea la edición.
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

        private void ConfigurarValidaciones()
        {
            txtDNI.MaxLength = 16;
            txtTelefono.MaxLength = 20;
            txtNombre.MaxLength = 50;
            txtApellido.MaxLength = 50;
            txtEmail.MaxLength = 50;

            txtDNI.KeyPress += SoloNumeros_KeyPress;
            txtTelefono.KeyPress += SoloNumeros_KeyPress;

            txtNombre.KeyPress += SoloLetras_KeyPress;
            txtApellido.KeyPress += SoloLetras_KeyPress;
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

            string patronNombre = @"^[\p{L}]+(?:[ '-][\p{L}]+)*$";

            if (!System.Text.RegularExpressions.Regex.IsMatch(
                txtNombre.Text.Trim(), patronNombre))
            {
                MessageBox.Show("El nombre contiene caracteres no válidos.");
                txtNombre.Focus();
                return false;
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(
                txtApellido.Text.Trim(), patronNombre))
            {
                MessageBox.Show("El apellido contiene caracteres no válidos.");
                txtApellido.Focus();
                return false;
            }

            return true;
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
                if (!ValidarCampos())
                    return;

                if (!ValidarDNIEnEdicion())
                    return;

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
