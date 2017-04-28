using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntidadesCompartidas;
using System.Xml;

namespace Logica
{
    public interface ILogicaJugada
    {
        void GenerarJugada(Jugada jugada);
        List<Jugada> ListarJugadasDeJugador(Jugador pJugador);
        Jugada BuscarJugada(Jugador jugador, int id);
        string ListarJugadasPremiadas(Jugador jugador);
        List<Jugada> ListarJugadasPremiadasPorSorteo(Sorteo sorteo);
    }
}
