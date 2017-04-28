using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ServiceModel;

using ServicioFinal;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Si hay un jugador registrado se pasa a la pagina realizar jugada que es la por defecto de registrados
        if (Session["JugadorRegistrado"] is Jugador)  
            Response.Redirect("~/GenerarJugada.aspx");

        if (Session["MensajeEliminado"] != "")
        {
            lblMensaje.Text = (string)Session["MensajeEliminado"];
            Session.Remove("MensajeEliminado");
        }
    }

    protected void LoginJugador_Authenticate(object sender, AuthenticateEventArgs e)
    {
        try
        {
            string _nombreLogueo = LoginJugador.UserName.Trim();
            string _contrasenia = LoginJugador.Password.Trim();

            if (_contrasenia.Length != 7)
                throw new Exception("La contraseña debe tener 7 caracteres");

            Usuario _usuario = new ServicioFinal.ServicioProyectoFinalClient().LogueoUsuario(_nombreLogueo, _contrasenia);

            if (_usuario == null || !(_usuario is Jugador))
                LoginJugador.FailureText = "Nombre de usuario o contraseña incorrecta";
            else
            {
                Session["JugadorRegistrado"] = _usuario;
                Response.Redirect("~/GenerarJugada.aspx");
            }
        }
        catch (FaultException Fe)
        {
            LoginJugador.FailureText = Fe.Reason.ToString();
        }

        catch (Exception ex)
        {
            if (ex.Message.Length > 120)
                LoginJugador.FailureText = "Error en la comunicacion con el servicio.";
            else
                LoginJugador.FailureText = ex.Message;
        }
    }
}