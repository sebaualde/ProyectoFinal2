using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntidadesCompartidas;
using Persistencia;
using System.Xml;

namespace Logica
{
    internal class LogicaJugada : ILogicaJugada
    {
        private static LogicaJugada _instancia = null;
        private LogicaJugada() { }
        public static LogicaJugada GetInstancia()
        {
            if (_instancia == null)
                _instancia = new LogicaJugada();

            return _instancia;
        }

        public void GenerarJugada(Jugada jugada)
        {
            FabricaPersistencia.GetPersistenciaJugada().GenerarJugada(jugada);
        }

        public Jugada BuscarJugada(Jugador jugador, int id)
        {
            return FabricaPersistencia.GetPersistenciaJugada().BuscarJugada(jugador, id);
        }

        public List<Jugada> ListarJugadasDeJugador(Jugador pJugador)
        {
            IPersistenciaJugada FJugada = FabricaPersistencia.GetPersistenciaJugada();
            return (FJugada.ListarJugadasDeJugador(pJugador));
        }

        public string ListarJugadasPremiadas(Jugador jugador)
        {
            try
            {
                XmlDocument _DocumentoPremiadas = new XmlDocument();
                _DocumentoPremiadas.LoadXml("<?xml version='1.0' encoding='utf-8' ?> <JugadasPremiadas> </JugadasPremiadas>");
                XmlNode nodoRaiz = _DocumentoPremiadas.DocumentElement;

                List<Jugada> jugadasPremiadas = FabricaPersistencia.GetPersistenciaJugada().ListarJugadasPremiadas(jugador);

                foreach (Jugada JP in jugadasPremiadas)
                {

                    XmlElement idJugada = _DocumentoPremiadas.CreateElement("Id");
                    idJugada.InnerText = JP.Id.ToString();

                    //XmlElement eljugador = _DocumentoPremiadas.CreateElement("Jugador");
                    //eljugador.InnerText = JP.Jugador.Documento.ToString();

                    XmlElement fechaHoraJugada = _DocumentoPremiadas.CreateElement("FechaYHora");
                    fechaHoraJugada.InnerText = JP.FechaHora.ToString();

                    XmlElement sorteo = _DocumentoPremiadas.CreateElement("Sorteo");
                    sorteo.InnerText = JP.FechaHoraSorteo.ToString();

                    XmlElement sorteofiltro = _DocumentoPremiadas.CreateElement("SorteoFiltro");
                    sorteofiltro.InnerText = JP.FechaHoraSorteo.ToShortDateString();

                    XmlElement numerosJugados = _DocumentoPremiadas.CreateElement("Numeros");

                    foreach (int NJ in JP.NumerosJugados)
                    {
                        XmlElement numero = _DocumentoPremiadas.CreateElement("Numero");
                        numero.InnerText = NJ.ToString();

                        numerosJugados.AppendChild(numero);
                    }

                    XmlElement nodoJugada = _DocumentoPremiadas.CreateElement("Jugada");

                    nodoJugada.AppendChild(idJugada);
                    //nodoJugada.AppendChild(eljugador);
                    nodoJugada.AppendChild(fechaHoraJugada);
                    nodoJugada.AppendChild(sorteo);
                    nodoJugada.AppendChild(sorteofiltro);
                    nodoJugada.AppendChild(numerosJugados);

                    nodoRaiz.AppendChild(nodoJugada);

                }
                return _DocumentoPremiadas.OuterXml;
            }
            catch(Exception ex)
            {
                throw new Exception("Error al listar las jugadas: " + ex.Message);
            }
        }

        public List<Jugada> ListarJugadasPremiadasPorSorteo(Sorteo sorteo)
        {
            try
            {
                return FabricaPersistencia.GetPersistenciaJugada().ListarJugadasPremiadasPorSorteo(sorteo);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar las jugadas: " + ex.Message);
            }
        }

        
    }
}
