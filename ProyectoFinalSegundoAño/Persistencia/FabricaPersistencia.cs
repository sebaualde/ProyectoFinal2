using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Persistencia
{
    public class FabricaPersistencia
    {
        public static IPersistenciaBanco GetPersistenciaBanco()
        {
            return (PersistenciaBanco.GetInstancia());      
        }

        public static IPersistenciaAdministrador GetPersistenciaAdministrador()
        {
            return (PersistenciaAdministrador.GetInstancia());
        }

        public static IPersistenciaJugador GetPersistenciaJugador()
        {
            return (PersistenciaJugador.GetInstancia());
        }

        public static IPersistenciaJugada GetPersistenciaJugada()
        {
            return (PersistenciaJugada.GetInstancia());
        }

        public static IPersistenciaSorteo GetPersistenciaSorteo()
        {
            return (PersistenciaSorteo.GetInstancia());
        }
    }
}
