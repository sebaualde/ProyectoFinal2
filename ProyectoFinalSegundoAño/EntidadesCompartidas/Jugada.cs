using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;


namespace EntidadesCompartidas
{
    [DataContract]
    public class Jugada
    {
        private int _id;
        private Jugador _jugador;
        private DateTime _fechaHora;
        private Sorteo _sorteo;
        private List<int> _numerosJugados;

        [DataMember]
        public int Id
        {
            get { return _id; }
            set
            {
                if (value < 1)
                {
                    throw new FaultException("El ID de la jugada no es válido");
                }

                _id = value;
            }
        }

        [DataMember]
        public Jugador Jugador
        {
            get { return _jugador; }
            set
            {
                if (value == null)
                {
                    throw new FaultException("El jugador no puede ser nulo");
                }
                _jugador = value;
            }
        }

        [DataMember]
        public DateTime FechaHora
        {
            get { return _fechaHora; }
            set { _fechaHora = value; }
        }

       [DataMember]
        public Sorteo unSorteo
        {
            get { return _sorteo; }
            set
            {
                if (value == null)
                {
                    throw new FaultException("El sorteo no puede ser nulo");
                }
                _sorteo = value;
            }
        }

        [DataMember]
        public DateTime FechaHoraSorteo
        {
            get { return _sorteo.FechaHora; }
            set { }
        }

        [DataMember]
        public List<int> NumerosJugados
        {
            get { return _numerosJugados; }
            set
            {
                if (value == null)
                {
                    throw new FaultException("La lista de números jugados no puede ser nula");
                }

                if (value.Count != 10)
                {
                    throw new FaultException("La jugada debe tener 10 números");
                }

                foreach (int nro in value.ToList())
                {
                    if (nro < 0 || nro > 50)
                        throw new FaultException("El número " + nro + " no se encuentra entre 0 y 50");
                }

                foreach (int nro in value)
                {
                    int duplicado = 0;

                    foreach (int nroBuscado in value)
                    {
                        if (nro == nroBuscado)
                            duplicado += 1;
                    }

                    if (duplicado > 1)
                        throw new FaultException("Error! No pueden haber numeros duplicados en la jugada!");
                }


                _numerosJugados = value;
            }
        }

        [DataMember]
        public int ElJugador
        {
            get { return Jugador.Documento; }
            set { }
        }

        public Jugada(int pId, Jugador pJugador, DateTime pFechaHora, Sorteo pSorteo, List<int> pNumerosJugados)
        {
            Id = pId;
            Jugador = pJugador;
            FechaHora = pFechaHora;
            unSorteo = pSorteo;
            NumerosJugados = pNumerosJugados.ToList();

        }

        public Jugada()
        {
            Id = 1;
            Jugador = new Jugador();
            FechaHora = DateTime.Now;
            unSorteo = new Sorteo();
            List<int> numeros = new List<int>();

            for (int i = 1; i < 11; i++)
                numeros.Add(i);

                NumerosJugados = numeros;
        }
    }
}
