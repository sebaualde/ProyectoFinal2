using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ServiceModel;
using AplicacionEscritorio.ServicioFinal;

namespace AplicacionEscritorio
{
    public partial class GeneracionDeSorteos : Form
    {
        public GeneracionDeSorteos()
        {
            InitializeComponent();

            dtpFecha.MinDate = DateTime.Today;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime fechaHora;

                if (Convert.ToInt32(txtHora.Text) < 0 || Convert.ToInt32(txtHora.Text) > 23)
                {
                    throw new Exception("EL campo \"hora\" debe ser un número entre 0 y 23");
                }

                if (Convert.ToInt32(txtHora.Text) < 0 || Convert.ToInt32(txtMinutos.Text) > 59)
                {
                    throw new Exception("EL campo \"minutos\" debe ser un número entre 0 y 59");
                }

                fechaHora = Convert.ToDateTime(string.Format("{0} {1}:{2}", dtpFecha.Value.ToShortDateString(), txtHora.Text, txtMinutos.Text));

                IServicioProyectoFinal servicio = new ServicioProyectoFinalClient();

                Sorteo sorteo = new Sorteo();
                sorteo.FechaHora = fechaHora;
                sorteo.NumerosSorteados = new List<int>().ToArray();
                servicio.GenerarSorteo(sorteo);


                lblMensaje.Text = "Se agregó el sorteo con fecha y hora" + fechaHora.ToString(); ;

            }
            catch (FaultException Fe)
            {
                lblMensaje.Text = Fe.Reason.ToString();
            }
            catch (FormatException)
            {
                lblMensaje.Text = "Formato de hora incorrecto.";
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
