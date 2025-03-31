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
    public partial class Form1 : Form
    {
        private List<Pokemon> listaPokemon;
        private List<Elemento> listaElemento;
        public Form1()
        {
            InitializeComponent();
        }

        private void dgvPokemon_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvGeneral.CurrentRow.DataBoundItem  is Pokemon)
            {
            Pokemon seleccionado = (Pokemon)dgvGeneral.CurrentRow.DataBoundItem;
                CargarImagen(seleccionado.UrlImagen);

            }    
        }
        private void CargarImagen ( string imagen)
        {
            try
            {
                pbxPokemon.Load(imagen);
            }
            catch (Exception)
            {
                pbxPokemon.Load("https://media.istockphoto.com/id/2173059563/vector/coming-soon-image-on-white-background-no-photo-available.jpg?s=612x612&w=0&k=20&c=v0a_B58wPFNDPULSiw_BmPyhSNCyrP_d17i2BPPyDTk=");
            }
        }

        private void btnPokemon_Click(object sender, EventArgs e)
        {
            PokemonNegocio negocio = new PokemonNegocio();
            listaPokemon = negocio.Listar();
            dgvGeneral.DataSource = listaPokemon;
            dgvGeneral.Columns["Descripcion"].Visible = false;
            dgvGeneral.Columns["UrlImagen"].Visible = false;
            CargarImagen(listaPokemon[0].UrlImagen);
            pbxPokemon.Visible = true;
        }

        private void btnElemento_Click(object sender, EventArgs e)
        {
           
            pbxPokemon.Visible = false;
            ElementoNegocio negocio = new ElementoNegocio();
            listaElemento = negocio.listar();
            dgvGeneral.DataSource = listaElemento;
            dgvGeneral.Columns["Descripcion"].Visible = true;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            FrmIngreso frmIngreso = new FrmIngreso();
            frmIngreso.ShowDialog();
        }
    }
}
