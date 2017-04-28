using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logica
{
    public class FabricaLogica
    {
        public static ILogicaBanco GetLogicaBanco()
        {
            return (LogicaBanco.GetInstancia());
        }

        public static ILogicaUsuario GetLogicaUsuario()
        {
            return (LogicaUsuario.GetInstancia());
        }

        public static ILogicaJugada GetLogicaJugada()
        {
            return (LogicaJugada.GetInstancia());
        }

        public static ILogicaSorteo GetLogicaSorteo()
        {
            return (LogicaSorteo.GetInstancia());
        }
    }
}
