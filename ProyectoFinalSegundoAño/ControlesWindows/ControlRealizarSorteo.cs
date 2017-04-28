using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ControlesWindows
{
    public partial class ControlRealizarSorteo : ContainerControl
    {
        public event EventHandler SorteoRealizado;

        private Button _btnSortear;
        private Label _lblMensaje;
        private List<int> _numeros;

        public List<int> Numeros
        {
            get
            {
                return _numeros;
            }
        }

        public ControlRealizarSorteo()
        {
            InitializeComponent();

            _lblMensaje = new Label();
            _lblMensaje.Location = new System.Drawing.Point(0, 30);
            _lblMensaje.Name = "lblMensaje";
            _lblMensaje.Text = "";
            _lblMensaje.Width=350;
            _lblMensaje.Height = 200;
            Controls.Add(_lblMensaje);

            _btnSortear = new Button();
            _btnSortear.Location = new System.Drawing.Point(0, 0);
            _btnSortear.Name = "btnSortear";
            _btnSortear.Text = "Sortear";
            _btnSortear.Visible = true;
            Controls.Add(_btnSortear);
            _btnSortear.Click += new EventHandler(_btnSortear_Click);


        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        private void _btnSortear_Click(object sender, EventArgs e)
        {
            try
            {
                List<int> numeros = new List<int>();

                    Random R = new Random();

                    for (int i = 0; i < 15; i++)
                    {
                        int numero =R.Next(0, 50);
                        
                        bool existe=false;
                        foreach (int nro in numeros)
                        {
                            if (nro == numero)
                                existe = true;
                        }

                        if (!existe)
                            numeros.Add(numero);
                        else
                            i--;
                    }

                    _numeros = numeros;

                    string mensaje = "Los números sorteados son:\n | ";
                    foreach (int Nro in numeros)
                    {
                        mensaje += Nro.ToString() + " | ";
                    }

                    _lblMensaje.Text = mensaje;

                SorteoRealizado(this, new EventArgs());
            }
               
            catch (Exception ex)
            {
                _lblMensaje.Text = ex.Message;
            }
        }
    }
}
