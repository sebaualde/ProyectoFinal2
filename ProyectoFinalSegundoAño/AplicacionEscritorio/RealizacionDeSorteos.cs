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
    public partial class RealizacionDeSorteos : Form
    {
        private Administrador _admin;
        private List<Sorteo> _sorteosDisponibles;

        public RealizacionDeSorteos(Administrador admin)
        {
            InitializeComponent();

            try
            {
                _admin = admin;
                IServicioProyectoFinal servicio = new ServicioProyectoFinalClient();

                _sorteosDisponibles = servicio.ListarSorteosDisponibles().ToList();

                gvSorteosDisponibles.DataSource = _sorteosDisponibles;
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

        private void controlRealizarSorteo1_SorteoRealizado(object sender, EventArgs e)
        {
            try
            {
                if (!_admin.EjecutaSoteos)
                {
                    throw new Exception("¡No tienes permisos para realizar sorteos!");
                }
                gvPremiadas.DataSource = null;

                if (gvSorteosDisponibles.SelectedRows.Count == 0)
                {
                    throw new Exception("ERROR: Ningún sorteo seleccionado");
                }
                Sorteo sorteo = (Sorteo)gvSorteosDisponibles.SelectedRows[0].DataBoundItem;
                sorteo.NumerosSorteados = controlRealizarSorteo1.Numeros.ToArray();
                /* NÚMEROS QUE GENERAN UNA JUGADA PREMIADA
                List<int> listahardcodeada = new List<int>();
                listahardcodeada.Add(1);
                listahardcodeada.Add(2);
                listahardcodeada.Add(3);
                listahardcodeada.Add(4);
                listahardcodeada.Add(5);
                listahardcodeada.Add(6);
                listahardcodeada.Add(7);
                listahardcodeada.Add(8);
                listahardcodeada.Add(9);
                listahardcodeada.Add(10);
                listahardcodeada.Add(11);
                listahardcodeada.Add(12);
                listahardcodeada.Add(13);
                listahardcodeada.Add(14);
                listahardcodeada.Add(15);
                sorteo.NumerosSorteados = listahardcodeada.ToArray();*/

                IServicioProyectoFinal servicio = new ServicioProyectoFinalClient();
                servicio.RealizarSorteo(sorteo);

                int indice = gvSorteosDisponibles.SelectedRows[0].Index;
                gvSorteosDisponibles.DataSource = null;
                
                _sorteosDisponibles.RemoveAt(indice);
                gvSorteosDisponibles.DataSource = _sorteosDisponibles;

                List<Jugada> premiadas = servicio.ListarJugadasPremiadasPorSorteo(sorteo).ToList();

                if (premiadas.Count != 0)
                {
                    gvPremiadas.AutoGenerateColumns = false;
                    gvPremiadas.DataSource = premiadas;
                }

                lblMensaje.Text = string.Format("Sorteo Realizado. {0} jugada(s) premiada(s)", premiadas.Count==0?"Ninguna":premiadas.Count.ToString());
            }
            catch (FaultException fe)
            {
                lblMensaje.Text = fe.Message;
            }
            catch(Exception ex)
            {
                lblMensaje.Text = ex.Message;
            }
        }

      
       
    }
}
