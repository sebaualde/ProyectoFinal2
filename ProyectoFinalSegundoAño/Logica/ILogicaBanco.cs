using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntidadesCompartidas;

namespace Logica
{
    public interface ILogicaBanco
    {
        void AltaBanco(Banco pBanco);
        void BajaBanco(Banco pBanco);
        void ModificarBanco(Banco pBanco);
        Banco BuscarBanco(string pRut);
        List<Banco> ListarBancos();
    }
}
