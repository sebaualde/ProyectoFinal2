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
    public partial class Logueo : Form
    {
        public Logueo()
        {
            InitializeComponent();

            ControlLogIn.AutenticarUsuario += new EventHandler(VerificoIngreso);
        }

        public void VerificoIngreso(object sender, EventArgs e)
        {
            try
            {
                Usuario _AdminLogueado = new ServicioProyectoFinalClient().LogueoUsuario(ControlLogIn.NombreUsuario, ControlLogIn.Contraseña);

                if (!(_AdminLogueado is Administrador))
                    lblMensaje.Text = "Nombre de usuario o contraseña incorrecta";
                else
                {
                    this.Hide();
                    Form _unForm = new FrmPrincipal((Administrador)_AdminLogueado);
                    _unForm.ShowDialog();
                    this.Close();
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
    }
}
