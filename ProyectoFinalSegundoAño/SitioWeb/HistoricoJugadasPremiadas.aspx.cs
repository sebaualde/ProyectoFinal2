using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using ServicioFinal;
using System.Xml.XPath;
using System.ServiceModel;

public partial class HistoricoJugadasPremiadas : System.Web.UI.Page
{ 
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                XmlDocument _documento = new XmlDocument();
                _documento.LoadXml(new ServicioProyectoFinalClient().ListarJugadasPremiadas((Jugador)Session["JugadorRegistrado"]));
                Session["Documento"] = _documento;

                xmlPremiadas.DocumentContent = _documento.OuterXml;

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
    protected void btnFiltrarPorFecha_Click(object sender, EventArgs e)
    {
        try
        {
            XmlDocument _documento = (XmlDocument)Session["Documento"];

            string _fechaBuscada = (Convert.ToDateTime(txtDia.Text.Trim())).ToShortDateString();

            XmlDocument documentofiltrado = new XmlDocument();
            documentofiltrado.LoadXml("<?xml version='1.0' encoding='utf-8' ?> <JugadasPremiadas> </JugadasPremiadas>");
            XmlNode _raiz = documentofiltrado.DocumentElement;

            XPathNavigator navegador = _documento.CreateNavigator();
            XPathNodeIterator resultado = navegador.Select("/JugadasPremiadas/Jugada[SorteoFiltro='" + _fechaBuscada + "']");

            if (resultado.Count > 0)
            {
                while (resultado.MoveNext())
                {
                    XmlElement nodo = documentofiltrado.CreateElement("Jugada");

                    XmlElement id = documentofiltrado.CreateElement("Id");
                    id.InnerText = resultado.Current.SelectSingleNode("Id").ToString();

                    nodo.AppendChild(id);

                    XmlElement fechaHora = documentofiltrado.CreateElement("FechaYHora");
                    fechaHora.InnerText = resultado.Current.SelectSingleNode("FechaYHora").ToString();
                    nodo.AppendChild(fechaHora);


                    XmlElement sorteo = documentofiltrado.CreateElement("Sorteo");
                    sorteo.InnerText = resultado.Current.SelectSingleNode("Sorteo").ToString();
                    nodo.AppendChild(sorteo);

                    XmlElement nodoNumeros = documentofiltrado.CreateElement("Numeros");

                    resultado.Current.MoveToChild("Numeros", "");
                    resultado.Current.MoveToChild("Numero", "");

                    XmlElement unNumero1 = documentofiltrado.CreateElement("Numero");
                    unNumero1.InnerText = resultado.Current.Value;
                    nodoNumeros.AppendChild(unNumero1);

                    while (resultado.Current.MoveToNext())
                    {
                        XmlElement unNumero = documentofiltrado.CreateElement("Numero");
                        unNumero.InnerText = resultado.Current.Value;
                        nodoNumeros.AppendChild(unNumero);
                    }

                    nodo.AppendChild(nodoNumeros);

                    _raiz.AppendChild(nodo);
                }

                xmlPremiadas.DocumentContent = documentofiltrado.OuterXml;
            }
            else
            {
                lblMensaje.Text = "No hay jugadas con premio para la fecha ingresada";
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
    protected void btnQuitarFiltro_Click(object sender, EventArgs e)
    {
        try
        {
            xmlPremiadas.DocumentContent = ((XmlDocument)Session["Documento"]).OuterXml;
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