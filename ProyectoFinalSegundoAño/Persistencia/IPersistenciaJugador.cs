using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntidadesCompartidas;

namespace Persistencia
{
    public interface IPersistenciaJugador
    {
        Jugador LogueoJugador(string pNombreUsuario, string pContrasenia);
        Jugador BuscarJugador(int pDocumento);
        void AltaJugador(Jugador pJugador);
        void BajaJugador(Jugador pJugador);
        void ModificarJugador(Jugador pJugador);
    }
}
