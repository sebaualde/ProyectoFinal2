using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Messaging;
using System.Collections;
using System.Configuration;
using ServicioFinal;
using System.ServiceModel;

public partial class RegistroJugador : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btnBuscar.Enabled = true;
            btnAgregar.Enabled = false;

            this.DesactivarCampos();
            CargarDDL();

            if (Session["JugadorRegistrado"] is Jugador)
                Response.Redirect("~/GenerarJugada.aspx");
        }
    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        try
        {
            int _documento = Convert.ToInt32(txtCI.Text.Trim());

            if (_documento.ToString().Length != 8)
                throw new Exception("La cedula debe tener 8 caracteres");

            if ((Convert.ToInt32(txtCI.Text)) < 0)
                throw new Exception("La cedula no puede ser negativa");

            Usuario _usuario = new ServicioFinal.ServicioProyectoFinalClient().BuscarUsuario(_documento);

            if (_usuario == null)
            {
                this.ActivarCamposyBtns();
                lblMensaje.Text = "No se encontro ningun cliente con CI: " + _documento + " Desea agreagarlo?";
            }
            else
            {
                this.DesactivarCampos();
                throw new Exception("Documento ya asignado a un usuario");
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
                lblMensaje.CssClass = "mensajeerror";
                lblMensaje.Text = ex.Message;
            }
        }
    }

    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        try
        {
           
            ServicioFinal.ServicioProyectoFinalClient _miServicio = new ServicioFinal.ServicioProyectoFinalClient();
            
            List<Banco> BancosEncontrados = (List<Banco>)Session["BancosEncontrados"];
            
            Banco unBanco = null;

            this.ValidarCampos();

            Jugador nuevoJugador = new Jugador();
            nuevoJugador.Documento = Convert.ToInt32(txtCI.Text.Trim());
            nuevoJugador.NombreCompleto = txtNomCompleto.Text;
            nuevoJugador.NombreLogueo = txtNomLogueo.Text;
            nuevoJugador.Contrasenia = txtContrasenia.Text;

            if ((Convert.ToInt32(txtCuentaBancaria.Text)) < 0)
                throw new Exception("El numero de cuenta bancaria no puede ser negativo");
            else
                nuevoJugador.NumeroCuenta = Convert.ToInt32(txtCuentaBancaria.Text.Trim());

            if (ddlBancos.SelectedIndex == 0)
                throw new Exception("Seleccione un banco valido.");
            else
            {
                unBanco = BancosEncontrados[ddlBancos.SelectedIndex - 1];// -1 debido a la opcion por defecto

                nuevoJugador.UnBanco = unBanco;
            }
            nuevoJugador.NombreBanco = unBanco.Nombre;
            nuevoJugador.UnAdmin = null;

            //------------------------------------------
            //--------envio a la cola de mensajes-------
            //------------------------------------------

            MessageQueue _ColaDeJugadores = new MessageQueue(ConfigurationManager.AppSettings["ColaUsuarios"]);

            _ColaDeJugadores.MessageReadPropertyFilter.SetAll();

            ((XmlMessageFormatter)_ColaDeJugadores.Formatter).TargetTypes = new Type[] { typeof(Jugador) };

            Message _MensajeEnviar = new Message(nuevoJugador);

            MessageQueueTransaction _Transaccion = new MessageQueueTransaction();
            _Transaccion.Begin();
            _ColaDeJugadores.Send(_MensajeEnviar, _Transaccion);
            _Transaccion.Commit();

            lblMensaje.Text = "Sus datos se enviaron correctamente, en breve seran vistos por un administrador";

            this.LimpiarTxt();
            ddlBancos.SelectedIndex = 0;
            this.DesactivarCampos();


            //limpiar session!!
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
                    lblMensaje.Text = ex.Message;
                    this.ActivarCamposyBtns();
                }      
            }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        txtCI.Enabled = true;
        lblMensaje.Text = "";
        this.LimpiarTxt();
        this.DesactivarCampos();
    }

    private void CargarDDL()
    {
        try
        {
            List<Banco> Bancos = new ServicioFinal.ServicioProyectoFinalClient().ListarBancos().ToList();
            List<string> _nombresBancos = new List<string>();

            //opcion por defecto para el ddl
            _nombresBancos.Add("Seleccione un Banco");

            foreach (Banco B in Bancos)
            {
                _nombresBancos.Add(B.Nombre);
            }

            ddlBancos.DataSource = _nombresBancos;
            ddlBancos.DataBind();
            Session["BancosEncontrados"] = Bancos;
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

    private void ValidarCampos()
    {
        if(txtNomCompleto.Text.Length == 0) 
            throw new Exception ("El nombre completo no puede estar vacio");

        if(txtNomLogueo.Text.Length == 0) 
            throw new Exception ("El nombre de logueo no puede estar vacio");
   
        if (txtContrasenia.Text.Length != 7)
            throw new Exception ("La contraseña debe contener 7 caracteres");
    }

    #region Mantenimiento

    protected void LimpiarTxt()
    {
        txtCI.Text = "";
        txtContrasenia.Text = "";
        txtNomCompleto.Text = "";
        txtNomLogueo.Text = "";
        txtCuentaBancaria.Text = "";
        btnBuscar.Enabled = true;
    }

    protected void DesactivarCampos()
    {
        txtCI.Enabled = true;
        txtContrasenia.Enabled = false;
        txtNomCompleto.Enabled = false;
        txtNomLogueo.Enabled = false;
        txtCuentaBancaria.Enabled = false;
        ddlBancos.Enabled = false;
    }

    protected void ActivarCamposyBtns()
    {
        btnBuscar.Enabled = false;
        btnAgregar.Enabled = true;

        txtCI.Enabled = false;
        txtContrasenia.Enabled = true;
        txtNomCompleto.Enabled = true;
        txtNomLogueo.Enabled = true;
        txtCuentaBancaria.Enabled = true;
        ddlBancos.Enabled = true;
    }

    #endregion
    
}