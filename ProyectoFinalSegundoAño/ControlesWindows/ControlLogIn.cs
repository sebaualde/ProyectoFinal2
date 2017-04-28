using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ControlesWindow
{
    public partial class ControlLogIn : UserControl
    {
        public ControlLogIn()
        {
            InitializeComponent();
        }

        public string NombreUsuario
        {
            get { return (txtNombreUsuario.Text.Trim()); }
        }

        public string Contraseña
        {
            get {
                if (txtContrasenia.Text.Length == 7)
                    return (txtContrasenia.Text.Trim());
                else
                    throw new Exception("La contraseña debe contener 7 caracteres!");
            }
        }


        //defino evento para logueo
        public event EventHandler AutenticarUsuario;

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            AutenticarUsuario(this, new EventArgs());
        }
    }
}
