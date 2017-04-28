using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace EntidadesCompartidas
{
    [DataContract]
    public class Banco
    {
        private string _rut;
        private string _nombre;
        private string _direccion;

        [DataMember]
        public string Rut
        {
            get { return _rut; }
            set 
            { 
                if (value.Length != 12)
                    throw new FaultException("El RUT del banco debe tener 12 números.");

                for (int i = 0; i < value.Length; i++)
                {
                    if (!(char.IsNumber(value[i])))
                        throw new FaultException("El RUT solo puede contener números.");
                }

                if (Convert.ToInt64(value) < 0)
                    throw new FaultException("El RUT no puede ser menor que 1.");

                _rut = value; 
            }
        }

        [DataMember]
        public string Nombre
        {
            get { return _nombre; }
            set 
            {
                if (value.Length > 20 || value.Trim() == string.Empty)
                    throw new FaultException("El nombre no puede quedar vacio ni tener más de 20 caracteres.");
                
                _nombre = value; 
            }
        }

        [DataMember]
        public string Direccion
        {
            get { return _direccion; }
            set { _direccion = value; }
        }

        public Banco(string pRut, string pNombre, string pDireccion)
        {
            Rut = pRut;
            Nombre = pNombre;
            Direccion = pDireccion;      
        }

        public Banco()
        {
            Rut = "000000000000";
            Nombre = "N/D";
            Direccion = "N/D";
        }

    }
}
