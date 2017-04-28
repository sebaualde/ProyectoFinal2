using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ServicioFinal;

public partial class MasterPageGeneral : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!(Session["JugadorRegistrado"] is Jugador))
        {
            Menu1.Visible = false;
            Menu0.Visible = true;
            Panel1.Visible = false;
            btnDesloguearse.Visible = false;
        }
        else
        {
            Panel1.Visible = true;
            lblUsuario.Text = ((Jugador)Session["JugadorRegistrado"]).NombreLogueo;
            Menu1.Visible = true;
            Menu0.Visible = false;
            btnDesloguearse.Visible = true;
        }
    }
    protected void btnDesloguearse_Click(object sender, EventArgs e)
    {
        Session.Remove("JugadorRegistrado");
        Response.Redirect("~/Default.Aspx");
    }
}
