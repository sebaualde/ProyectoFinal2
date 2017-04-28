using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntidadesCompartidas;
using Persistencia;

namespace Logica
{
    internal class LogicaBanco : ILogicaBanco
    {
        private static LogicaBanco _instancia = null;
        private LogicaBanco() { }
        public static LogicaBanco GetInstancia()
        {
            if (_instancia == null)
                _instancia = new LogicaBanco();

            return _instancia;
        }

        public void AltaBanco(Banco pBanco)
        {
            IPersistenciaBanco FabricaBanco = FabricaPersistencia.GetPersistenciaBanco();
            FabricaBanco.AltaBanco(pBanco);      
        }

        public void BajaBanco(Banco pBanco)
        {
            IPersistenciaBanco FabricaBanco = FabricaPersistencia.GetPersistenciaBanco();
            FabricaBanco.BajaBanco(pBanco);
        }

        public void ModificarBanco(Banco pBanco)
        {
            IPersistenciaBanco FabricaBanco = FabricaPersistencia.GetPersistenciaBanco();
            FabricaBanco.ModificarBanco(pBanco);
        }

        public Banco BuscarBanco(string pRut)
        {
            IPersistenciaBanco FabricaBanco = FabricaPersistencia.GetPersistenciaBanco();
            return (FabricaBanco.BuscarBanco(pRut));
        }

        public List<Banco> ListarBancos()
        {
            IPersistenciaBanco FabricaBanco = FabricaPersistencia.GetPersistenciaBanco();
            return (FabricaBanco.ListarBancos());
        }
    }
}
