using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ServicioFinal;
using System.ServiceModel;

public partial class GenerarJugada : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                IServicioProyectoFinal servicio = new ServicioProyectoFinalClient();
                Repeater1.DataSource = servicio.ListarSorteosDisponiblesJugador((Jugador)Session["JugadorRegistrado"]);
                Repeater1.DataBind();
                this.MsjJugadaRealizada();

                if (!(Session["JugadorRegistrado"] is Jugador))
                    Response.Redirect("~/Default.aspx");
            }
            this.MsjJugadaRealizada();
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


    protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "RealizarJugada")
        {
            try
            {
                PanelSorteos.Visible = false;
                PanelJugada.Visible = true;

                Sorteo sorteo = new ServicioProyectoFinalClient().BuscarSorteo(Convert.ToDateTime(((Label)(e.Item.Controls[1])).Text));
                Session["Sorteo"] = sorteo;
                lblSorteoSeleccionado.Text = ((Label)(e.Item.Controls[1])).Text;
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
                lblMensaje.CssClass = "mensajeerror";
            }
        }
    }
    protected void btnEnviarJugada_Click(object sender, EventArgs e)
    {
        try
        {
            List<int> numeros = new List<int>();
            try
            {
                numeros.Add(Convert.ToInt32(txtNumero1.Text));
                numeros.Add(Convert.ToInt32(txtNumero2.Text));
                numeros.Add(Convert.ToInt32(txtNumero3.Text));
                numeros.Add(Convert.ToInt32(txtNumero4.Text));
                numeros.Add(Convert.ToInt32(txtNumero5.Text));
                numeros.Add(Convert.ToInt32(txtNumero6.Text));
                numeros.Add(Convert.ToInt32(txtNumero7.Text));
                numeros.Add(Convert.ToInt32(txtNumero8.Text));
                numeros.Add(Convert.ToInt32(txtNumero9.Text));
                numeros.Add(Convert.ToInt32(txtNumero10.Text));
            }
            catch (FormatException)
            {
                throw new Exception("Formato de número incorrecto.");
            }

            Jugada jugada = new Jugada();
            jugada.Id = 1;
            jugada.Jugador = (Jugador)Session["JugadorRegistrado"];
            jugada.unSorteo = (Sorteo)Session["Sorteo"];
            jugada.NumerosJugados = numeros.ToArray();

            IServicioProyectoFinal unServicio = new ServicioProyectoFinalClient(); 
            unServicio.GenerarJugada(jugada);

            PanelJugada.Visible = false;
            PanelSorteos.Visible = true;
            limpiar();
     
            PanelJugada.Visible = false;
            PanelSorteos.Visible = true;
            limpiar();

            Session["JugadaRealizada"] = "Se realizó la jugada para el sorteo con fecha: " + jugada.unSorteo.FechaHora;
            Response.Redirect("~/GenerarJugada.aspx");
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
        try
        {
            PanelJugada.Visible = false;
            PanelSorteos.Visible = true;
            limpiar();
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
    protected void limpiar()
    {
        txtNumero1.Text = "";
        txtNumero2.Text = "";
        txtNumero3.Text = "";
        txtNumero4.Text = "";
        txtNumero5.Text = "";
        txtNumero6.Text = "";
        txtNumero7.Text = "";
        txtNumero8.Text = "";
        txtNumero9.Text = "";
        txtNumero10.Text = "";
    }

    private void MsjJugadaRealizada()
    {
        if ((string)Session["JugadaRealizada"] != "")
        {
            lblMensaje.Text = (string)Session["JugadaRealizada"];
            Session["usuarioActualizado"] = "";
        }
    }
}