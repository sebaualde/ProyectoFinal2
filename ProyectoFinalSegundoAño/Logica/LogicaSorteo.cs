using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Persistencia;
using EntidadesCompartidas;

namespace Logica
{
    internal class LogicaSorteo : ILogicaSorteo
    {
        private static LogicaSorteo _instancia = null;
        private LogicaSorteo() { }
        public static LogicaSorteo GetInstancia()
        {
            if (_instancia == null)
                _instancia = new LogicaSorteo();

            return _instancia;
        }


        public void GenerarSorteo(Sorteo sorteo)
        {
            FabricaPersistencia.GetPersistenciaSorteo().GenerarSorteo(sorteo);
        }

        public void RealizarSorteo(Sorteo sorteo)
        {
            FabricaPersistencia.GetPersistenciaSorteo().RealizarSorteo(sorteo);
        }

        public Sorteo BuscarSorteo(DateTime pFechaHora)
        {
            return FabricaPersistencia.GetPersistenciaSorteo().BuscarSorteo(pFechaHora);
        }

        public List<Sorteo> ListarSorteosDisponiblesJugador(Jugador jugador)
        {
            return FabricaPersistencia.GetPersistenciaSorteo().ListarSorteosDisponiblesJugador(jugador);
        }

        public List<Sorteo> ListarSorteosDisponibles()
        {
            return FabricaPersistencia.GetPersistenciaSorteo().ListarSorteosDisponibles();
        }

    }
}
