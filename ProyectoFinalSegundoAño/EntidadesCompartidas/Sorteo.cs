using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace EntidadesCompartidas
{
    [DataContract]
    public class Sorteo
    {
        private DateTime _fechaHora;
        private List<int> _numerosSorteados;

        [DataMember]
        public DateTime FechaHora
        {
            get { return _fechaHora; }
            set
            {
                if(value <DateTime.Now)
                    throw new FaultException("Fecha y hora anterior a la actual");

                _fechaHora = value;
            }
        }

        [DataMember]
        public List<int> NumerosSorteados
        {
            get { return _numerosSorteados; }
            set {
                if (value == null)
                {
                    throw new FaultException("La lista de números sorteados no puede ser nula");
                }

                foreach (int nro in value)
                {
                    if (nro < 0 || nro > 50)
                        throw new Exception("El número " + nro + "no se encuentra entre 0 y 50");
                }

                _numerosSorteados = value; }
        }

        public Sorteo()
        {
            FechaHora = DateTime.Now;
            NumerosSorteados = new List<int>();
        }

        public Sorteo(DateTime pFechaHora, List<int> pNumerosSorteados)
        {
            FechaHora = pFechaHora;
            NumerosSorteados = pNumerosSorteados;
        }
    }
}
