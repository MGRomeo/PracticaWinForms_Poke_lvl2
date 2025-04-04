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

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            ListarPokemon();

        }

        private void dgvPokemon_SelectionChanged(object sender, EventArgs e)
        {
            ListarElemento();
        }

        private void ListarPokemon()
        {
            PokemonNegocio negocio = new PokemonNegocio();
            try
            {
                listaPokemon = negocio.Listar();
                dgvGeneral.DataSource = listaPokemon;
                dgvGeneral.Columns["Id"].Visible = false;
                dgvGeneral.Columns["Descripcion"].Visible = false;
                dgvGeneral.Columns["UrlImagen"].Visible = false;
                dgvGeneral.Columns["Tipo"].Visible = false;
                dgvGeneral.Columns["Debilidad"].Visible = false;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void ListarElemento()
        {

            Pokemon seleccionado = (Pokemon)dgvGeneral.CurrentRow.DataBoundItem;
            List<Pokemon> poke = new List<Pokemon>();
            PokemonNegocio negocio = new PokemonNegocio();
            poke = negocio.ListarTipoDebilidad(seleccionado.Numero);
            CargarImagen(seleccionado.UrlImagen);
            pbxPokemon.Visible = true;
            dgvElemento.DataSource = poke;
            txtDescipcion.Text = seleccionado.Descripcion;
            dgvElemento.Columns["Id"].Visible = false;
            dgvElemento.Columns["Numero"].Visible = false;
            dgvElemento.Columns["Nombre"].Visible = false;
            dgvElemento.Columns["Descripcion"].Visible = false;
            dgvElemento.Columns["UrlImagen"].Visible = false;
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

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            FrmIngreso frmIngreso = new FrmIngreso();
            frmIngreso.ShowDialog();
            ListarPokemon();
            ListarElemento();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if(dgvGeneral.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un Pokemon primero");
            }
            else
            {
                Pokemon pokemon = (Pokemon)dgvGeneral.CurrentRow.DataBoundItem;
                FrmIngreso frmIngreso = new FrmIngreso(pokemon);
                frmIngreso.ShowDialog();
            }
            
        }

        private void btnModifTipoDebilidad_Click(object sender, EventArgs e)
        {
            if (dgvElemento.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un Tipo primero");
            }
            else if (dgvGeneral.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un Pokemon primero");
            }
            else
            {
                Pokemon pokemon = (Pokemon)dgvGeneral.CurrentRow.DataBoundItem;
                Pokemon tipo = (Pokemon)dgvElemento.CurrentRow.DataBoundItem;
                FrmIngreso frmIngreso = new FrmIngreso(pokemon,tipo,1);
                frmIngreso.ShowDialog();
                
            }
        }

    }
}
