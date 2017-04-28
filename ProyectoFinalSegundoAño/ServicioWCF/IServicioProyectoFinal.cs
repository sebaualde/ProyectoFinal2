using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using EntidadesCompartidas;
using System.Xml;

namespace ServicioWCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IServicioProyectoFinal" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IServicioProyectoFinal
    {
        #region LogicaBanco

        [OperationContract]
        void AltaBanco(Banco pBanco);

        [OperationContract]
        void BajaBanco(Banco pBanco);

        [OperationContract]
        void ModificarBanco(Banco pBanco);

        [OperationContract]
        Banco BuscarBanco(string pRut);

        [OperationContract]
        List<Banco> ListarBancos();

        #endregion

        #region LogicaUsuario

        [OperationContract]
        Usuario LogueoUsuario(string pNombreUsuario, string pContrasenia);

        [OperationContract]
        Usuario BuscarUsuario(int pDocumento);

        [OperationContract]
        void AltaUsuario(Usuario pUsuario);

        [OperationContract]
        void BajaUsuario(Usuario pUsuario);
        
        [OperationContract]
        void ModificarUsuario(Usuario pUsuario);

        [OperationContract]
        void AltaUsuarioLogueoYBD(Usuario pUsuario, string pRol, string pPermiso);

        [OperationContract]
        List<Administrador> ListarAdministradores();

        #endregion

         #region LogicaJugada

        [OperationContract]
        List<Jugada> ListarJugadasDeJugador(Jugador pJugador);

        [OperationContract]
        void GenerarJugada(Jugada jugada);

        [OperationContract]
        Jugada BuscarJugada(Jugador jugador, int id);

        [OperationContract]
        string ListarJugadasPremiadas(Jugador jugador);

        [OperationContract]
        List<Jugada> ListarJugadasPremiadasPorSorteo(Sorteo sorteo);
        #endregion

        #region LogicaSorteos

        [OperationContract]
        void GenerarSorteo(Sorteo sorteo);

        [OperationContract]
        void RealizarSorteo(Sorteo sorteo);

        [OperationContract]
        Sorteo BuscarSorteo(DateTime pFechaHora);

        [OperationContract]
        List<Sorteo> ListarSorteosDisponibles();

        [OperationContract]
        List<Sorteo> ListarSorteosDisponiblesJugador(Jugador jugador);

        #endregion
    }
}
