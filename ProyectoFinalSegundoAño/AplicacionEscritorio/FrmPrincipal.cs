using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AplicacionEscritorio.ServicioFinal;

namespace AplicacionEscritorio
{
    public partial class FrmPrincipal : Form
    {
        private Administrador _AdminLogueado = null;
        
        public FrmPrincipal(Administrador pAdmin)
        {
            InitializeComponent();
            _AdminLogueado = pAdmin;
        }

        private void ABMAdministradoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ABMAdministradores _unForm = new ABMAdministradores(_AdminLogueado);
            _unForm.ShowDialog();
        }

        private void RegistroDeJugadoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RegistroJugadores _unForm = new RegistroJugadores(_AdminLogueado);
            _unForm.ShowDialog();
        }

        private void ABMBancosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ABMBancos _unForm = new ABMBancos();
            _unForm.ShowDialog();
        }

        private void GeneracionDeSorteosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GeneracionDeSorteos form = new GeneracionDeSorteos();

            form.ShowDialog();
        }

        private void RealizaciónDeSorteosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RealizacionDeSorteos form = new RealizacionDeSorteos(_AdminLogueado);

            form.ShowDialog();
        }

        private void UsuarioDeBDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UsuariosDeBaseDeDatos _unForm = new UsuariosDeBaseDeDatos();
            _unForm.ShowDialog();
        }
    }
}
