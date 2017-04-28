using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Persistencia
{
    internal class Conexion
    {
        private static string _cnn = "Data Source=.; Initial Catalog=Final2015; Integrated Security=true";
        private static string _cnn2 = "Data Source=.; Initial Catalog=Final2015; user id=UsuarioLogueo; Password=123";
        
        internal static string Cnn
        {
            get { return _cnn; }
        }

        internal static string Cnn2
        {
            get { return _cnn2; }
        }
    }
}
