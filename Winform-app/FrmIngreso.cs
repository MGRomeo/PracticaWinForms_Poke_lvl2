using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dominio;
using Negocio;

namespace Winform_app
{
    public partial class FrmIngreso : Form
    {
        public FrmIngreso()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            
            Pokemon poke = new Pokemon();
            PokemonNegocio negocio = new PokemonNegocio();
            poke.Numero = int.Parse(txtNumero.Text);
            poke.Nombre = txtNombre.Text;
            poke.Descripcion = txtDescipcion.Text;
            //int max = int.Parse(negocio.IdMax().ToString() + 1);
            negocio.Insert(poke, negocio.IdMax());

        }
    }
}
