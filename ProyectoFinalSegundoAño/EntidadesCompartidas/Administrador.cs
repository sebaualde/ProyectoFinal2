using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace EntidadesCompartidas
{
    [DataContract]
    public class Administrador : Usuario
    {
        private bool _ejecutaSoteos;

        [DataMember]
        public bool EjecutaSoteos
        {
            get { return _ejecutaSoteos; }
            set { _ejecutaSoteos = value; }
        }

        public Administrador()
        {
            EjecutaSoteos = false;        
        }

        public Administrador(int pDocumento, string pNombreCompleto, string pNombreLogueo, string pContrasenia, bool pEjecutaSoteos)
            :base (pDocumento, pNombreCompleto, pNombreLogueo, pContrasenia)
        {
            EjecutaSoteos = pEjecutaSoteos;        
        }
    }
}
