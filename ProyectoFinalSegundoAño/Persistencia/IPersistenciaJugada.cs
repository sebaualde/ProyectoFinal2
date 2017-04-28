using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntidadesCompartidas;

namespace Persistencia
{
    public interface IPersistenciaJugada
    {
        void GenerarJugada(Jugada jugada);
        List<Jugada> ListarJugadasDeJugador(Jugador pJugador);
        Jugada BuscarJugada(Jugador jugador, int id);
        List<Jugada> ListarJugadasPremiadas(Jugador jugador);
        List<Jugada> ListarJugadasPremiadasPorSorteo(Sorteo sorteo);

    }
}
