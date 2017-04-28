using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntidadesCompartidas;

namespace Persistencia
{
    public interface IPersistenciaSorteo
    {
        void GenerarSorteo(Sorteo sorteo);
        void RealizarSorteo(Sorteo sorteo);
        Sorteo BuscarSorteo(DateTime pFechaHora);
        List<Sorteo> ListarSorteosDisponiblesJugador(Jugador jugador);
        List<Sorteo> ListarSorteosDisponibles();

    }
}
