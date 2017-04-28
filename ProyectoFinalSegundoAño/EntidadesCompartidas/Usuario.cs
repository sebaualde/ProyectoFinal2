using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace EntidadesCompartidas
{
    [KnownType(typeof(Jugador))]
    [KnownType(typeof(Administrador))]  
    [DataContract]
    public abstract class Usuario
    {
        private int _documento;
        private string _nombreCompleto;
        private string _nombreLogueo;
        private string _contrasenia;

        [DataMember]
        public int Documento
        {
            get { return _documento; }
            set 
            {
                if (value.ToString().Length != 8)
                    throw new FaultException("El Documento debe tener 8 números.");

                if (value < 0)
                    throw new FaultException("La cedula no puede ser negativa");
                
                _documento = value; 
            }
        }

        [DataMember]
        public string NombreCompleto
        {
            get { return _nombreCompleto; }
            set 
            {
                if (value.Length > 50 || value.Trim() == string.Empty)
                    throw new FaultException("El Nombre no puede quedar vacio ni tener mas de 50 caracteres.");

                _nombreCompleto = value; }
        }

        [DataMember]
        public string NombreLogueo
        {
            get { return _nombreLogueo; }
            set 
            {
                if (value.Length > 20 || value.Trim() == string.Empty)
                    throw new FaultException("El Nombre de logueo no puede quedar vacio ni tener mas de 20 caracteres.");

                _nombreLogueo = value; }
        }

        [DataMember]
        public string Contrasenia
        {
            get { return _contrasenia; }
            set 
            {
                if (value.Length != 7)
                    throw new FaultException("La contraseña debe tener 7 caracteres.");

                _contrasenia = value; }
        }

        public Usuario()
        {
            Documento = 12345678;
            NombreCompleto = "N/D";
            NombreLogueo = "N/D";
            Contrasenia = "Nnn/ddD";       
        }

        public Usuario(int pDocumento, string pNombreCompleto, string pNombreLogueo, string pContrasenia)
        {
            Documento = pDocumento;
            NombreCompleto = pNombreCompleto;
            NombreLogueo = pNombreLogueo;
            Contrasenia = pContrasenia;       
        }
    }
}
