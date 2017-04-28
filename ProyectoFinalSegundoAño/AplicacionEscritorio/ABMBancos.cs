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
    public partial class ABMBancos : Form
    {
        private Banco _unBanco;
        private IServicioProyectoFinal _unServicioWCF = new ServicioProyectoFinalClient();

        public ABMBancos()
        {
            InitializeComponent();
        }

        private void txtRut_Validating(object sender, CancelEventArgs e)
        {
            string _rut = txtRut.Text.Trim();

            try
            {
                if (_rut.Length != 12)
                    throw new Exception("El RUT debe tener 12 caracteres");

                for (int i = 0; i < _rut.Length; i++)
                {
                    if (!(char.IsNumber(_rut[i])))
                        throw new Exception("El RUT solo puede contener números.");              
                }

                if (Convert.ToInt64(_rut) < 1)
                    throw new Exception("El RUT no puede ser menor que 1.");

                epError.Clear();

                _unBanco = _unServicioWCF.BuscarBanco(_rut);

                if (_unBanco == null)
                {
                    btnAlta.Enabled = true;
                    txtRut.Enabled = false;

                    lblMensaje.Text = "No se encontro ningún banco con el RUT " + _rut + ", puede agrgarlo.";

                    _unBanco = new Banco();
                    _unBanco.Rut = _rut;
                }
                else
                {
                    txtRut.Text = _unBanco.Rut;
                    txtNombre.Text = _unBanco.Nombre;
                    txtDireccion.Text = _unBanco.Direccion;

                    txtRut.Enabled = false;
                    btnEliminar.Enabled = true;
                    btnModificar.Enabled = true;
                    btnAlta.Enabled = false;
                    lblMensaje.Text = "¡Banco encontrado! puede modificarlo o eliminarlo si desea.";
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
                epError.SetError(txtRut, ex.Message);
                e.Cancel = true;
                lblMensaje.Text = ex.Message;
            }
            }  
        }

        #region Botones

        private void btnAlta_Click(object sender, EventArgs e)
        {
            try
            {
                if (_unBanco == null)
                    throw new Exception("No hay un Banco en memoria para agregarlo.");

                if (txtNombre.Text.Trim().Length > 20 || txtNombre.Text.Trim() == string.Empty)
                    throw new Exception("El nombre no puede quedar vacio ni tener más de 20 caracteres.");
                else
                    _unBanco.Nombre = txtNombre.Text.Trim();

                _unBanco.Direccion = txtDireccion.Text.Trim();

                _unServicioWCF.AltaBanco(_unBanco);

                Limpiar();
                lblMensaje.Text = "¡Banco agregado con éxito!";
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

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (_unBanco == null)
                    throw new Exception("No hay un Banco en memoria para Eliminar.");

                DialogResult resultado = MessageBox.Show("¿Esta seguro que desea Eliminar éste Banco?", "¡¡Advertencia!!", MessageBoxButtons.YesNo);

                if (resultado == DialogResult.Yes)
                {
                    _unServicioWCF.BajaBanco(_unBanco);

                    Limpiar();
                    lblMensaje.Text = "¡Banco Eliminado con éxito!";
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
                if (_unBanco == null)
                    throw new Exception("No hay un Banco en memoria para Eliminar.");

                _unBanco.Nombre = txtNombre.Text.Trim();
                _unBanco.Direccion = txtDireccion.Text.Trim();

                DialogResult resultado = MessageBox.Show("¿Esta seguro que desea Modificar éste Banco?", "¡¡Advertencia!!", MessageBoxButtons.YesNo);

                if (resultado == DialogResult.Yes)
                {
                    _unServicioWCF.ModificarBanco(_unBanco);

                    Limpiar();
                    lblMensaje.Text = "¡Banco Modificado con éxito!";
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

        #endregion

        #region Mantenimiento

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
            lblMensaje.Text = "";
        }

        private void Limpiar()
        {
            txtRut.Text = "";
            txtNombre.Text = "";
            txtDireccion.Text = "";

            txtRut.Enabled = true;
            btnAlta.Enabled = false;
            btnEliminar.Enabled = false;
            btnModificar.Enabled = false;

            _unBanco = null;
            epError.Clear();
            txtRut.Focus();
        }

        private void ABMBancos_Load(object sender, EventArgs e)
        {
            Limpiar();
            lblMensaje.Text = "";
        }

        #endregion

        
    }
}
