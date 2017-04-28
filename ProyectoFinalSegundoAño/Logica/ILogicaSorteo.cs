using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntidadesCompartidas;


namespace Logica
{
    public interface ILogicaSorteo
    {
        void GenerarSorteo(Sorteo sorteo);
        void RealizarSorteo(Sorteo sorteo);
        Sorteo BuscarSorteo(DateTime pFechaHora);
        List<Sorteo> ListarSorteosDisponibles();
        List<Sorteo> ListarSorteosDisponiblesJugador(Jugador jugador);
    }
}
