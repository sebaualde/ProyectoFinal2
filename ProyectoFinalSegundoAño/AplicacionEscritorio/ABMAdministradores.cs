using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using AplicacionEscritorio.ServicioFinal;
using System.ServiceModel;

namespace AplicacionEscritorio
{
    public partial class ABMAdministradores : Form
    {
        private Administrador _AdminLogueado = null;
        private Administrador _UsuarioBuscado = null;

        public ABMAdministradores(Administrador pAdmin)
        {
            InitializeComponent();
            txtDocumento.Focus();
            _AdminLogueado = pAdmin;
            this.DeshabilitarBotones();
        }

        private void btnAlta_Click(object sender, EventArgs e)
        {
            try
            {
                _UsuarioBuscado = new Administrador();

                _UsuarioBuscado.Documento = Convert.ToInt32(txtDocumento.Text.Trim());
                _UsuarioBuscado.NombreCompleto = txtNomCompleto.Text;
                _UsuarioBuscado.NombreLogueo = txtNomLogueo.Text;
                if (txtContrasenia.Text.Trim() == txtConfContrasenia.Text.Trim())
                    _UsuarioBuscado.Contrasenia = txtContrasenia.Text;
                else
                    lblMensaje.Text = "Las contraseñas no coinciden";
                _UsuarioBuscado.EjecutaSoteos = cbSorteo.Checked;

                new ServicioProyectoFinalClient().AltaUsuario(_UsuarioBuscado);

                DeshabilitarBotones();
                LimpiarCampos();

                lblMensaje.Text = "Se agrego el administrador correctamente.";
            }

            catch (FaultException Fe)
            {
                lblMensaje.Text = Fe.Reason.ToString();
            }

            catch (Exception ex)
            {
                if (ex.Message.Length > 120)
                    lblMensaje.Text = "Error en la comunicacion con el servicio.";
                else
                {
                    if (_UsuarioBuscado == null)
                    {
                        DesactivarBotones();
                    }

                    lblMensaje.Text = "¡Error! " + ex.Message;
                }

            }
        }

        private void btnBaja_Click(object sender, EventArgs e)
        {
            try
            {
                if (_UsuarioBuscado is Administrador)
                {
                    DialogResult resultado = MessageBox.Show("¿Esta seguro que desea Eliminar este Administrador?", "Advertencia!!", MessageBoxButtons.YesNo);

                    if (resultado == DialogResult.Yes)
                    {
                        new ServicioProyectoFinalClient().BajaUsuario(_UsuarioBuscado);
                        DeshabilitarBotones();
                        LimpiarCampos();
                        lblMensaje.Text = "¡Administrador Eliminado exitosamente!";
                    }
                }
            }

            catch (FaultException Fe)
            {
                lblMensaje.Text = Fe.Reason.ToString();
            }

            catch (Exception ex)
            {
                if (ex.Message.Length > 120)
                    lblMensaje.Text = "Error en la comunicacion con el servicio.";
                else
                    lblMensaje.Text = ex.Message;
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                _UsuarioBuscado.NombreCompleto = txtNomCompleto.Text;
                _UsuarioBuscado.NombreLogueo = txtNomLogueo.Text;
                if (txtContrasenia.Text != string.Empty)
                {
                    if (txtContrasenia.Text.Length != 7)
                        throw new Exception("La contraseña debe contener 7 caracteres");
                    else if (txtContrasenia.Text.Trim() != txtConfContrasenia.Text.Trim())
                        throw new Exception("Las contraseñas no coinciden");
                    else
                        _UsuarioBuscado.Contrasenia = txtContrasenia.Text.Trim();
                }

                if (_AdminLogueado.Documento == Convert.ToInt32(txtDocumento.Text))
                    cbSorteo.Enabled = false;
                else
                    _UsuarioBuscado.EjecutaSoteos = cbSorteo.Checked;

                DialogResult resultado = MessageBox.Show("¿Esta seguro que desea Modificar esta administrador?", "Advertencia!!", MessageBoxButtons.YesNo);

                if (resultado == DialogResult.Yes)
                {
                    new ServicioProyectoFinalClient().ModificarUsuario(_UsuarioBuscado);

                    DeshabilitarBotones();
                    LimpiarCampos();

                    lblMensaje.Text = "¡Administrador modificado corectamente!";
                }
            }

            catch (FaultException Fe)
            {
                lblMensaje.Text = Fe.Reason.ToString();
            }

            catch (Exception ex)
            {
                if (ex.Message.Length > 120)
                    lblMensaje.Text = "Error en la comunicacion con el servicio.";
                else
                {
                    txtContrasenia.Enabled = true;
                    txtNomCompleto.Enabled = true;
                    txtNomLogueo.Enabled = true;
                    if (_AdminLogueado.Documento == Convert.ToInt32(txtDocumento.Text))
                        cbSorteo.Enabled = false;
                    else
                    {
                        cbSorteo.Enabled = true;
                        btnModificar.Enabled = true;
                        btnBaja.Enabled = true;

                        txtDocumento.Enabled = false;
                        lblMensaje.Text = ex.Message;
                    }
                 }
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            DeshabilitarBotones();
            LimpiarCampos();

            lblMensaje.Text = "";

            txtNomCompleto.Enabled = true;
            txtNomLogueo.Enabled = true;
            txtContrasenia.Enabled = true;
            txtConfContrasenia.Enabled = true;
            cbSorteo.Enabled = true;
            epError.Clear();
        }

        private void txtDocumento_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (txtDocumento.Text.Trim() == string.Empty)
                    throw new Exception("Por favor ingrese una CI");

                int _documento = Convert.ToInt32(txtDocumento.Text.Trim());

                if (_documento.ToString().Length != 8)
                    throw new Exception("La cedula debe tener 8 caracteres");

                if (_documento < 0)
                    throw new Exception("La cedula no puede ser negativa");

                epError.Clear();
                lblMensaje.Text = "";

                Usuario _usuario = new ServicioProyectoFinalClient().BuscarUsuario(_documento);

                if (_usuario == null)
                {
                    lblMensaje.Text = "No se encontro ningun administrador con CI: " + _documento + " Desea agreagarlo?";
                    btnAlta.Enabled = true;
                    txtDocumento.Enabled = false;
                }

                else if (_usuario is Administrador)
                {
                    txtDocumento.Enabled = false;

                    if (_AdminLogueado.Documento == Convert.ToInt32(txtDocumento.Text))
                        cbSorteo.Enabled = false;
                    else
                        cbSorteo.Enabled = true;

                    _UsuarioBuscado = new Administrador();

                    _UsuarioBuscado.Documento = _usuario.Documento;
                    _UsuarioBuscado.NombreCompleto = _usuario.NombreCompleto;
                    _UsuarioBuscado.NombreLogueo = _usuario.NombreLogueo;
                    _UsuarioBuscado.Contrasenia = _usuario.Contrasenia;
                    _UsuarioBuscado.EjecutaSoteos = ((Administrador)_usuario).EjecutaSoteos;

                    txtDocumento.Text = _usuario.Documento.ToString();
                    txtNomCompleto.Text = _usuario.NombreCompleto;
                    txtNomLogueo.Text = _usuario.NombreLogueo;
                    cbSorteo.Checked = ((Administrador)_usuario).EjecutaSoteos;

                    ActivarBotonA();

                    if (_AdminLogueado.Documento == Convert.ToInt32(txtDocumento.Text))
                        btnBaja.Enabled = false;
                }

                else
                {
                    lblMensaje.Text = "Ya existe un usuario con esa cedula asociada";
                    txtNomCompleto.Enabled = false;
                    txtNomLogueo.Enabled = false;
                    txtContrasenia.Enabled = false;
                    txtConfContrasenia.Enabled = false;
                    cbSorteo.Enabled = false;
                }
            }

            catch (FaultException Fe)
            {
                lblMensaje.Text = Fe.Reason.ToString();
            }

            catch (Exception ex)
            {
                if (ex.Message.Length > 80)
                    lblMensaje.Text = "Error en la comunicacion con el servicio.";
                else
                {
                    epError.SetError(txtDocumento, ex.Message);
                    e.Cancel = true;
                    lblMensaje.Text = ex.Message;
                }
            }
        }

        private void txtContrasenia_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (txtContrasenia.Text.Trim().ToString().Length != 7)
                    throw new Exception("La contraseña tiene que tener 7 caracteres!");
                else
                {
                    epError.Clear();
                    lblMensaje.Text = "";
                }
            }
            catch (FaultException Fe)
            {
                lblMensaje.Text = Fe.Reason.ToString();
            }

            catch (Exception ex)
            {
                epError.SetError(txtContrasenia, ex.Message);
                e.Cancel = false;
                lblMensaje.Text = ex.Message;
            }
        }

        private void txtConfContrasenia_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (txtContrasenia.Text.Trim().ToString().Length != 7)
                    throw new Exception("La contraseña tiene que tener 7 caracteres!");

                if (txtContrasenia.Text != txtConfContrasenia.Text)
                    throw new Exception("Las contraseñas no coinciden!!");
                else
                {
                    epError.Clear();
                    lblMensaje.Text = "";
                }
            }

            catch (FaultException Fe)
            {
                lblMensaje.Text = Fe.Reason.ToString();
                epError.SetError(txtConfContrasenia, Fe.Reason.ToString());
                e.Cancel = false;
            }

            catch (Exception ex)
            {
                if (ex.Message.Length > 120)
                    lblMensaje.Text = "Error en la comunicacion con el servicio.";
                else
                {
                    epError.SetError(txtConfContrasenia, ex.Message);
                    e.Cancel = false;
                    lblMensaje.Text = ex.Message;
                }
            }            
        }

        #region Mantenimiento
        private void LimpiarCampos()
        {
            txtDocumento.Enabled = true;
            txtDocumento.Focus();
            txtDocumento.Text = "";
            txtNomCompleto.Text = "";
            txtNomLogueo.Text = "";
            txtContrasenia.Text = "";
            txtConfContrasenia.Text = "";
            cbSorteo.Checked = false;
        }

        private void ActivarBotonA()
        {
            btnAlta.Enabled = false;
            btnModificar.Enabled = true;
            btnBaja.Enabled = true;
        }

        private void DesactivarBotones()
        {
            btnAlta.Enabled = true;
            btnModificar.Enabled = false;
            btnBaja.Enabled = false;
        }

        private void DeshabilitarBotones()
        {
            btnAlta.Enabled = false;
            btnModificar.Enabled = false;
            btnBaja.Enabled = false;
        }

        #endregion
    }
}
