using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntidadesCompartidas;

namespace Logica
{
    public interface ILogicaUsuario
    {
        void AltaUsuarioLogueoYBD(Usuario pUsuario, string pRol, string pPermiso);
        List<Administrador> ListarAdministradores();

        Usuario LogueoUsuario(string pNombreUsuario, string pContrasenia);
        Usuario BuscarUsuario(int pDocumento);
        void AltaUsuario(Usuario pUsuario);
        void BajaUsuario(Usuario pUsuario); 
        void ModificarUsuario(Usuario pUsuario);
    }
}
