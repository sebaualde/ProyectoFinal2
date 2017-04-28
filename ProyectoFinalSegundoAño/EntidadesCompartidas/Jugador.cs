using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace EntidadesCompartidas
{
    [DataContract]
    public class Jugador : Usuario
    {
        private int _numeroCuenta;
        private Banco _unBanco;
        private Administrador _agregadoPor;

        [DataMember]
        public int NumeroCuenta
        {
            get { return _numeroCuenta; }
            set 
            {
                if (value < 0 || value.ToString() == string.Empty)
                    throw new FaultException("El numero de cuenta no puede estar vacio o ser negativo");
                
                _numeroCuenta = value; 
            }
        }

        [DataMember]
        public Banco UnBanco
        {
            get { return _unBanco; }
            set 
            { 
                if (value == null)
                    throw new FaultException("Debe seleccionar un banco");

                _unBanco = value; 
            }
        }

        [DataMember]
        public string NombreBanco
        {
            get { return _unBanco.Nombre; }
            set {  }
        }

        [DataMember]
        public Administrador UnAdmin
        {
            get { return _agregadoPor; }
            set 
            {
                //if (value == null)
                //    throw new FaultException("Error con el administrador");

                _agregadoPor = value; 
            }
        }
            
        public Jugador()
        {
            NumeroCuenta = 0;
            UnBanco = new Banco();
            UnAdmin = new Administrador();
            NombreBanco = "N/D";
        }

        public Jugador(int pDocumento, string pNombreCompleto, string pNombreLogueo, string pContrasenia, int pNumeroCuenta, Banco pBanco, Administrador pAgregadoPor)
            :base (pDocumento, pNombreCompleto, pNombreLogueo, pContrasenia)
        {
            NumeroCuenta = pNumeroCuenta;
            UnBanco = pBanco;
            UnAdmin = pAgregadoPor;
        }

    }
}
