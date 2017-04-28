using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ServicioFinal;
using System.ServiceModel;

public partial class ListarSorteosParticipados : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                IServicioProyectoFinal _unServicioWCF = new ServicioProyectoFinalClient();

                List<Jugada> _jugadas = _unServicioWCF.ListarJugadasDeJugador((Jugador)Session["JugadorRegistrado"]).ToList();
                Session["ListaSorteos"] = _jugadas;

                CargarGV(_jugadas);

                if (!(Session["JugadorRegistrado"] is Jugador))
                    Response.Redirect("~/Default.aspx");
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

    protected void ibtnFiltrar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            lblMensaje.Text = "";

            DateTime _fechaBuscada;

            if (ddlTipoFiltro.SelectedValue == "MesYAnio")
            { 
                _fechaBuscada = Convert.ToDateTime("1/" + txtMes.Text.Trim() + "/" + txtAnio.Text.Trim());

                List<Jugada> _resultado = (from unaJ in ((List<Jugada>)Session["ListaSorteos"])
                                           where unaJ.unSorteo.FechaHora.Month == _fechaBuscada.Month && unaJ.unSorteo.FechaHora.Year == _fechaBuscada.Year
                                           select unaJ).ToList<Jugada>();

                if (_resultado.Count > 0)
                    CargarGV(_resultado);
                else
                    lblMensaje.Text = "No se encontraron sorteos en los que participe en esa fecha.";
            }
            else if (ddlTipoFiltro.SelectedValue == "FechaConcreta")
            {
                _fechaBuscada = Convert.ToDateTime(txtDia.Text.Trim() + "/" + txtMes.Text.Trim() + "/" + txtAnio.Text.Trim());

                List<Jugada> _resultado = (from unaJ in ((List<Jugada>)Session["ListaSorteos"])
                                           where unaJ.unSorteo.FechaHora.Date == _fechaBuscada.Date
                                           select unaJ).ToList<Jugada>();

                if (_resultado.Count > 0)
                    CargarGV(_resultado);
                else
                    lblMensaje.Text = "No se encontraron sorteos en los que participe en esa fecha.";
            }
            else
            {
                lblMensaje.Text = "Seleccione una opción de filtro por favor.";
            
            }
        }
        catch (FaultException Fe)
        {
            lblMensaje.Text = Fe.Reason.ToString();
        }
        catch (FormatException)
        {
            lblMensaje.Text = "Formato de Frcha incorrecto.";
        }
        catch (Exception ex)
        {
            if (ex.Message.Length > 120)
                lblMensaje.Text = "Error en la comunicacion con el servicio.";
            else
                lblMensaje.Text = ex.Message;
        }

    }

    protected void gvSorteos_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            List<Jugada> _jugadas = (List<Jugada>)Session["ListaSorteos"];
            string _FechaSeleccionada = gvSorteos.SelectedRow.Cells[0].Text;
            string _numerosJugados = "";

            foreach (Jugada j in _jugadas)
            {
                if (j.FechaHoraSorteo == Convert.ToDateTime(_FechaSeleccionada))
                {
                    foreach (int i in j.NumerosJugados)
                        _numerosJugados += " | " + i.ToString() + " | ";
                }
            }

            lblMensaje.ForeColor = System.Drawing.Color.Red;
            lblMensaje.Text = _numerosJugados;
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

    #region Mantenimiento
 
    protected void ibtnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        Limpiar();
        lblMensaje.Text = "";
    }

    private void Limpiar()
    {
        txtDia.Text = "";
        txtMes.Text = "";
        txtAnio.Text = "";
        ddlTipoFiltro.SelectedIndex = 0;

        CargarGV((List<Jugada>)Session["ListaSorteos"]);
    }

    private void CargarGV(List<Jugada> pSorteos)
    {
        gvSorteos.DataSource = pSorteos;
        gvSorteos.DataBind();
        gvSorteos.SelectedIndex = -1;    
    }

    #endregion
}