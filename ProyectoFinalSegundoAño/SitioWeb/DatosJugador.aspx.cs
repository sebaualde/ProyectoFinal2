using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ServicioFinal;
using System.ServiceModel;


public partial class DatosJugador : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CargarDDL();
            UsuarioLogueado();
            MensajeDeActualizacionParaElLabelDeMensajeQueAvisaSiSeModificoCorrectamenteElJugadorLogeado();

            if (!(Session["JugadorRegistrado"] is Jugador))
                Response.Redirect("~/Default.aspx");
        }

        this.MensajeDeActualizacionParaElLabelDeMensajeQueAvisaSiSeModificoCorrectamenteElJugadorLogeado();

            txtCI.Enabled = false;
    }
    protected void btnModificar_Click(object sender, EventArgs e)
    {
        try
        {
            Usuario JugadorModificado = (Jugador)Session["JugadorRegistrado"];

            if (txtNomCompleto.Text.Trim() != JugadorModificado.NombreCompleto)
                JugadorModificado.NombreCompleto = txtNomCompleto.Text.Trim();

            if (txtNomLogueo.Text.Trim() != JugadorModificado.NombreLogueo)
                JugadorModificado.NombreLogueo = txtNomLogueo.Text.Trim();

            if (txtContrasenia.Text.Trim() != JugadorModificado.Contrasenia)
                JugadorModificado.Contrasenia = txtContrasenia.Text.Trim();

            if (txtCuentaBancaria.Text.Trim() != ((Jugador)JugadorModificado).NumeroCuenta.ToString())
                ((Jugador)JugadorModificado).NumeroCuenta = Convert.ToInt32(txtCuentaBancaria.Text.Trim());

            if (ddlBanco.SelectedValue != ((Jugador)JugadorModificado).UnBanco.Nombre)
            {
                List<Banco> BancosEncontrados = (List<Banco>)Session["BancosEncontrados"];
                Banco Banco = null;

                foreach (Banco B in BancosEncontrados)
                {
                    if (B.Nombre == ddlBanco.SelectedValue)
                    {
                        Banco = B;
                    }
                }
                ((Jugador)JugadorModificado).UnBanco = Banco;
            }

            new ServicioProyectoFinalClient().ModificarUsuario(JugadorModificado);

            Session["JugadorRegistrado"] = JugadorModificado;
            Session["usuarioActualizado"] = "Sus datos fueron actualizados correctamente!";
            Response.Redirect("~/DatosJugador.aspx");
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
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        lblMensaje.Text = "";
        this.UsuarioLogueado();
    }

    private void UsuarioLogueado()
    {
        try
        {
            Jugador jugadorLogueado = (Jugador)Session["JugadorRegistrado"];
            txtCI.Text = jugadorLogueado.Documento.ToString();
            txtNomCompleto.Text = jugadorLogueado.NombreCompleto;
            txtNomLogueo.Text = jugadorLogueado.NombreLogueo;
            txtContrasenia.Text = jugadorLogueado.Contrasenia;
            txtCuentaBancaria.Text = jugadorLogueado.NumeroCuenta.ToString();
            ddlBanco.SelectedValue = jugadorLogueado.UnBanco.Nombre;
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

    private void CargarDDL()
    {
        try
        {
            List<Banco> Bancos = new ServicioFinal.ServicioProyectoFinalClient().ListarBancos().ToList();
            List<string> _nombresBancos = new List<string>();

            foreach (Banco B in Bancos)
            {
                _nombresBancos.Add(B.Nombre);
            }

            ddlBanco.DataSource = _nombresBancos;
            ddlBanco.DataBind();
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

    private void MensajeDeActualizacionParaElLabelDeMensajeQueAvisaSiSeModificoCorrectamenteElJugadorLogeado()
    {
        //jaja leyó el nombre?
        if ((string)Session["usuarioActualizado"] != "")
        {
            lblMensaje.Text = "Sus datos fueron actualizados correctamente!";
            Session["usuarioActualizado"] = "";
        }
    }
    protected void btnEliminar_Click(object sender, EventArgs e)
    {

        try
        {
            Jugador jugadorPorEliminar = (Jugador)Session["JugadorRegistrado"];

            new ServicioFinal.ServicioProyectoFinalClient().BajaUsuario(jugadorPorEliminar);

            Session.Remove("JugadorRegistrado");

            Session["MensajeEliminado"] = "Su usuario a sido eliminado correctamente del sistema";

            Response.Redirect("~/Default.aspx");
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
}