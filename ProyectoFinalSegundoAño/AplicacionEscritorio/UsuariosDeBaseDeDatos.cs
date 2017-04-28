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
    public partial class UsuariosDeBaseDeDatos : Form
    {
        private IServicioProyectoFinal _unServicioWCF = new ServicioProyectoFinalClient();
        private List<Administrador> _administradores = null;
 
        public UsuariosDeBaseDeDatos()
        {      
            InitializeComponent();
        }

        private void UsuariosDeBaseDeDatos_Load(object sender, EventArgs e)
        {
            try
            {
                _administradores = _unServicioWCF.ListarAdministradores().ToList();
                Limpiar();
                dgvAdministradores.AutoGenerateColumns = false;
                dgvAdministradores.DataSource = _administradores;
                dgvAdministradores.ClearSelection();
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

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                Usuario _admin = null;

                foreach (Administrador a in _administradores)
                {
                    if (a.Documento == Convert.ToInt32(dgvAdministradores.SelectedRows[0].Cells[0].Value))
                        _admin = a;
                }

                if (cbRoles.SelectedItem.ToString() == "Seleccione un Rol")
                    throw new Exception("Seleccione un Rol de la lista.");

                string _rol = cbRoles.SelectedItem.ToString();

                if(cbPermisos.SelectedItem.ToString() == "Seleccione un Permiso")
                    throw new Exception("Seleccione un permiso de la lista.");

                string _permiso = cbPermisos.SelectedItem.ToString();

                DialogResult resultado = MessageBox.Show("¿Esta seguro que desea crear el usuario de BD " + _admin.NombreLogueo +"?", "Advertencia!!", MessageBoxButtons.YesNo);

                if (resultado == DialogResult.Yes)
                {
                    _unServicioWCF.AltaUsuarioLogueoYBD(_admin, _rol, _permiso);
                    Limpiar();

                    lblMensaje.Text = "¡Usuario creado con éxito!";
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

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
            lblMensaje.Text = "";
        }

        private void Limpiar()
        {
            btnAgregar.Enabled = false;
            dgvAdministradores.ClearSelection();

            cbRoles.SelectedIndex = 0;
            cbPermisos.SelectedIndex = 0;

            cbRoles.Enabled = false;
            cbPermisos.Enabled = false;
        }

        private void dgvAdministradores_SelectionChanged(object sender, EventArgs e)
        {
            cbRoles.Enabled = true;
            cbPermisos.Enabled = true;
            btnAgregar.Enabled = true;      
        }

       
    }
}
