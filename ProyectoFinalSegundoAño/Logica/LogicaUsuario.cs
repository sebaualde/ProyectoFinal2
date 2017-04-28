using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntidadesCompartidas;
using Persistencia;

namespace Logica
{
    internal class LogicaUsuario : ILogicaUsuario
    {
        private static LogicaUsuario _instancia = null;
        private LogicaUsuario() { }
        public static LogicaUsuario GetInstancia()
        {
            if (_instancia == null)
            {
                _instancia = new LogicaUsuario();
            }

            return _instancia;
        }

        public void AltaUsuarioLogueoYBD(Usuario pUsuario, string pRol, string pPermiso)
        {
            if (pUsuario is Administrador)
            {
                IPersistenciaAdministrador FAdminisrador = FabricaPersistencia.GetPersistenciaAdministrador();
                FAdminisrador.AltaUsuarioLogueoYBD((Administrador)pUsuario, pRol, pPermiso);
            }
            else
                throw new Exception("El tipo de usuario proporcionado no es correcto.");
        }

        public List<Administrador> ListarAdministradores()
        {
            IPersistenciaAdministrador FAdministrador = FabricaPersistencia.GetPersistenciaAdministrador();
            return (FAdministrador.ListarAdministradores());      
        }

        public Usuario LogueoUsuario(string pNombreUsuario, string pContrasenia)
        {
            Usuario unUsu = null;

            unUsu = FabricaPersistencia.GetPersistenciaJugador().LogueoJugador(pNombreUsuario, pContrasenia);

            if (unUsu == null)
                unUsu = FabricaPersistencia.GetPersistenciaAdministrador().LogueoAdministrador(pNombreUsuario, pContrasenia);

            return unUsu;
        }

        public Usuario BuscarUsuario(int pDocumento)
        {
            Usuario unUsu = null;

            unUsu = FabricaPersistencia.GetPersistenciaAdministrador().BuscarAdministrador(pDocumento);

            if (unUsu == null)
                unUsu = FabricaPersistencia.GetPersistenciaJugador().BuscarJugador(pDocumento);

            return unUsu;
        }

        public void AltaUsuario(Usuario pUsuario)
        {
            if (pUsuario is Administrador)
                FabricaPersistencia.GetPersistenciaAdministrador().AltaAdministrador((Administrador)pUsuario);

            else if (pUsuario is Jugador)
                FabricaPersistencia.GetPersistenciaJugador().AltaJugador((Jugador)pUsuario);

            else
                throw new Exception("El tipo de usuario no es valido");
        }

        public void BajaUsuario(Usuario pUsuario)
        {
            if (pUsuario is Administrador)
                FabricaPersistencia.GetPersistenciaAdministrador().BajaAdministrador((Administrador)pUsuario);

            else if (pUsuario is Jugador)
                FabricaPersistencia.GetPersistenciaJugador().BajaJugador((Jugador)pUsuario);

            else
                throw new Exception("El tipo de usuario no es valido");
        
        }

        public void ModificarUsuario(Usuario pUsuario)
        {
            if (pUsuario is Administrador)
                FabricaPersistencia.GetPersistenciaAdministrador().ModificarAdministrador((Administrador)pUsuario);

            else if (pUsuario is Jugador)
                FabricaPersistencia.GetPersistenciaJugador().ModificarJugador((Jugador)pUsuario);

            else
                throw new Exception("El tipo de usuario no es valido");
        }      
    }
}
