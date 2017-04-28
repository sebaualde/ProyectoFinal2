using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Messaging;
using System.ServiceModel;
using AplicacionEscritorio.ServicioFinal;


delegate void ArriboMensaje (Jugador _unJugador);

namespace AplicacionEscritorio
{
    public partial class RegistroJugadores : Form
    {
        private Administrador AdminLogueado = null;
        private MessageQueue _ColaDeJugadores;
        private List<Jugador> _ListaDeJugadores;
        private Jugador _unJugador;

        public RegistroJugadores(Administrador pAdminLogeado)
        {
            InitializeComponent();
            AdminLogueado = pAdminLogeado;
            CargarColaMensajes();
            rbAgregar.Enabled = false;
            rbRechazar.Enabled = false;
        }

        private void CargarColaMensajes()
        {
            try
            {
                string _Direccion = ConfigurationManager.AppSettings["ColaUsuarios"];
                _ColaDeJugadores = new MessageQueue(_Direccion);
                _ColaDeJugadores.MessageReadPropertyFilter.SetAll();

                ((XmlMessageFormatter)_ColaDeJugadores.Formatter).TargetTypes = new Type[] { typeof(Jugador) };

                _ListaDeJugadores = new List<Jugador>();

                _unJugador = null;

                btnConfirmar.Enabled = false;

                lblDocumento.Text = "";
                lblNombreCompleto.Text = "";
                lblNombreLogueo.Text = "";
                lblBanco.Text = "";
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

        private void btnConfirmar_Click_1(object sender, EventArgs e)
        {
            try
            {
                _unJugador = new Jugador();
                foreach (Jugador j in _ListaDeJugadores)
                {
                    if (j.Documento.ToString() == gvJugadores.SelectedRows[0].Cells[0].Value.ToString())
                    {
                        _unJugador = j;
                    }
                }

                if (rbAgregar.Checked)
                {
                    _unJugador.UnAdmin = AdminLogueado;

                    new ServicioProyectoFinalClient().AltaUsuario(_unJugador);

                    _ListaDeJugadores.Remove(_unJugador);

                    gvJugadores.DataSource = null;
                    gvJugadores.DataSource = _ListaDeJugadores;

                    lblMensaje.Text = "Jugador agregado correctamente";
                }
                else
                {
                    _ListaDeJugadores.Remove(_unJugador);

                    gvJugadores.DataSource = null;
                    gvJugadores.DataSource = _ListaDeJugadores;

                    lblMensaje.Text = "Jugador rechazado correctamente";
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

        private void RegistroJugadores_Load(object sender, EventArgs e)
        {
             try
            {
                _ColaDeJugadores.BeginReceive(new TimeSpan(1, 0, 0, 0));

                _ColaDeJugadores.ReceiveCompleted += new ReceiveCompletedEventHandler(Recepcion);

                gvJugadores.AutoGenerateColumns = false;
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
                     MessageBox.Show(ex.Message, "Error en MSMQ", MessageBoxButtons.OK, MessageBoxIcon.Error);
             }
        }

        private void RegistroJugadores_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (_ListaDeJugadores.Count > 0)
                {
                    DialogResult _respuesta = MessageBox.Show("Hay jugadores sin aceptar. \nSi sale del formulario los datos de dichos jugadores se perderan. \nEsta seguro que desea salir de todos modos?", "PERDIDA INMINENTE DE DATOS", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);

                    if (_respuesta == System.Windows.Forms.DialogResult.No)
                    {
                        e.Cancel = true;
                    }
                }

                _ColaDeJugadores.Close();
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

        private void Recepcion(object sender, ReceiveCompletedEventArgs args)
        {
            try
            {

                System.Messaging.Message _unMensaje = _ColaDeJugadores.EndReceive(args.AsyncResult);

                Jugador _unJugadorCualquiera = (Jugador)_unMensaje.Body;

                gvJugadores.Invoke(new ArriboMensaje(DepliegoListaJugadores), _unJugadorCualquiera);

                _ColaDeJugadores.BeginReceive(new TimeSpan(1, 0, 0, 0));

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
                { }
            }

        }

        private void DepliegoListaJugadores(Jugador pJugadores)
        {
            try
            {
                bool repetidos = false;

                foreach (Jugador j in _ListaDeJugadores)
                {
                    if (j.Documento == pJugadores.Documento)
                        repetidos = true;
                }

                if (!(repetidos))
                _ListaDeJugadores.Add(pJugadores);
             
                gvJugadores.DataSource = null;
                gvJugadores.DataSource = _ListaDeJugadores;
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
                    MessageBox.Show(ex.Message, "Error en MSMQ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gvJugadores_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (gvJugadores.SelectedRows.Count > 0)
                {
                    lblDocumento.Text = gvJugadores.SelectedRows[0].Cells[0].Value.ToString();
                    lblNombreCompleto.Text = gvJugadores.SelectedRows[0].Cells[1].Value.ToString();
                    lblNombreLogueo.Text = gvJugadores.SelectedRows[0].Cells[2].Value.ToString();
                    lblBanco.Text = gvJugadores.SelectedRows[0].Cells[3].Value.ToString();

                    rbAgregar.Checked = false;
                    rbRechazar.Checked = false;
                    btnConfirmar.Enabled = false;
                }

                rbAgregar.Enabled = true;
                rbRechazar.Enabled = true;
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

        private void rbAgregar_CheckedChanged(object sender, EventArgs e)
        {
            btnConfirmar.Enabled = true;
        }

        private void rbRechazar_CheckedChanged(object sender, EventArgs e)
        {
            btnConfirmar.Enabled = true;
        }
    }
}
