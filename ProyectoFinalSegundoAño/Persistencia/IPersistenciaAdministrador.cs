using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntidadesCompartidas;

namespace Persistencia
{
    public interface IPersistenciaAdministrador
    {
        void AltaUsuarioLogueoYBD(Administrador pAdmin, string pRol, string pPermiso);
        List<Administrador> ListarAdministradores();

        Administrador LogueoAdministrador(string pNombreUsuario, string pContrasenia);
        Administrador BuscarAdministrador(int pDocumento);
        void AltaAdministrador(Administrador pAdministrador);
        void BajaAdministrador (Administrador pAdministrador);
        void ModificarAdministrador(Administrador pAdministrador);
    }
}
