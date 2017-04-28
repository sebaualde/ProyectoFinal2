using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using EntidadesCompartidas;
using Logica;
using System.Xml;
using System.Web.Services.Protocols;

namespace ServicioWCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "ServicioProyectoFinal" en el código, en svc y en el archivo de configuración a la vez.
    public class ServicioProyectoFinal : IServicioProyectoFinal
    {
        #region LogicaBanco

        void IServicioProyectoFinal.AltaBanco(Banco pBanco)
        {
            try
            {
                FabricaLogica.GetLogicaBanco().AltaBanco(pBanco);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        void IServicioProyectoFinal.BajaBanco(Banco pBanco)
        {
            try
            {
                FabricaLogica.GetLogicaBanco().BajaBanco(pBanco);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        void IServicioProyectoFinal.ModificarBanco(Banco pBanco)
        {           
            try
            {
                FabricaLogica.GetLogicaBanco().ModificarBanco(pBanco);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        Banco IServicioProyectoFinal.BuscarBanco(string pRut)
        {
            try
            {
                return (FabricaLogica.GetLogicaBanco().BuscarBanco(pRut));
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
            
        }

        List<Banco> IServicioProyectoFinal.ListarBancos()
        {
            try
            {
                return (FabricaLogica.GetLogicaBanco().ListarBancos());
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
            
        }

        #endregion

        #region LogicaUsuario

       void IServicioProyectoFinal.AltaUsuarioLogueoYBD(Usuario pUsuario, string pRol, string pPermiso)
        {
            try
            {
                FabricaLogica.GetLogicaUsuario().AltaUsuarioLogueoYBD(pUsuario, pRol, pPermiso);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
            
        }

        Usuario IServicioProyectoFinal.LogueoUsuario(string pNombreUsuario, string pContrasenia)
        {
            try
            { 
                return (FabricaLogica.GetLogicaUsuario().LogueoUsuario(pNombreUsuario, pContrasenia));
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        Usuario IServicioProyectoFinal.BuscarUsuario(int pDocumento)
        {
            try
            {
                return (FabricaLogica.GetLogicaUsuario().BuscarUsuario(pDocumento));
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }

        }

        void IServicioProyectoFinal.AltaUsuario(Usuario pUsuario)
        {
            try
            {
                FabricaLogica.GetLogicaUsuario().AltaUsuario(pUsuario);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        void IServicioProyectoFinal.BajaUsuario(Usuario pUsuario)
        {
            try
            {
                FabricaLogica.GetLogicaUsuario().BajaUsuario(pUsuario);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        void IServicioProyectoFinal.ModificarUsuario(Usuario pUsuario)
        {
            try
            {
                FabricaLogica.GetLogicaUsuario().ModificarUsuario(pUsuario);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        List<Administrador> IServicioProyectoFinal.ListarAdministradores()
        {
            try
            {
                return (FabricaLogica.GetLogicaUsuario().ListarAdministradores());
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
            
        }

        #endregion

        #region LogicaJugada

        List<Jugada> IServicioProyectoFinal.ListarJugadasDeJugador(Jugador pJugador) 
        {
            try
            {
                return (FabricaLogica.GetLogicaJugada().ListarJugadasDeJugador(pJugador));
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }

        }

        void IServicioProyectoFinal.GenerarJugada(Jugada jugada)
        {
            try
            {
                FabricaLogica.GetLogicaJugada().GenerarJugada(jugada);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }

        }

        Jugada IServicioProyectoFinal.BuscarJugada(Jugador jugador, int id)
        {
            try
            {
                return FabricaLogica.GetLogicaJugada().BuscarJugada(jugador, id);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        string IServicioProyectoFinal.ListarJugadasPremiadas(Jugador jugador)
        {
            try
            {
                return FabricaLogica.GetLogicaJugada().ListarJugadasPremiadas(jugador);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        List<Jugada> IServicioProyectoFinal.ListarJugadasPremiadasPorSorteo(Sorteo sorteo)
        {
            try
            {
                return FabricaLogica.GetLogicaJugada().ListarJugadasPremiadasPorSorteo(sorteo);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        #endregion

        #region LogicaSorteo

        void IServicioProyectoFinal.GenerarSorteo(Sorteo sorteo)
        {
            try
            {
                FabricaLogica.GetLogicaSorteo().GenerarSorteo(sorteo);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        void IServicioProyectoFinal.RealizarSorteo(Sorteo sorteo)
        {
            try
            {
                FabricaLogica.GetLogicaSorteo().RealizarSorteo(sorteo);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        Sorteo IServicioProyectoFinal.BuscarSorteo(DateTime pFechaHora)
        {
            try
            {
                return FabricaLogica.GetLogicaSorteo().BuscarSorteo(pFechaHora);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        List<Sorteo> IServicioProyectoFinal.ListarSorteosDisponiblesJugador(Jugador jugador)
        {
            try
            {
                return FabricaLogica.GetLogicaSorteo().ListarSorteosDisponiblesJugador(jugador);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        List<Sorteo> IServicioProyectoFinal.ListarSorteosDisponibles()
        {
            try
            {
                return FabricaLogica.GetLogicaSorteo().ListarSorteosDisponibles();
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
     
        #endregion
    }
}
