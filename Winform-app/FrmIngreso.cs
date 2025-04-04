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
        // Declaro el atributo pokemon = null para cuando cargue la ventana
        //  pueda diferenciar si es una alta o una modificacion
        private Pokemon pokemon = null;
        private Pokemon tipo = null;
        private int bandera;
        public FrmIngreso()
        {
            InitializeComponent();
        }

        // Constructor para modificacion

        public FrmIngreso(Pokemon pokemon)
        {
            InitializeComponent();
            this.pokemon = pokemon;
        }
        public FrmIngreso(Pokemon pokemon, Pokemon tipo, int bandera)
        {
            InitializeComponent();
            this.pokemon = pokemon;
            this.tipo = tipo;
            this.bandera = bandera;
        }



        private void FrmIngreso_Load(object sender, EventArgs e)
        {

            ElementoNegocio elemento = new ElementoNegocio();
            try
            {   
                cbxTipo.DataSource = elemento.listar();
                // Acá realizo un manejo del objeto Elemento,
                // uso [clave,valor], ya que el cbx  permite funcionar asi por detras,
                // la clave está hiden y el valor se muestra en texto
                // puede ser cualquier atributo de la clase, [numero,nombre], [nombre,descripcion], etc
                cbxTipo.ValueMember = "Id"; //clave
                cbxTipo.DisplayMember = "Descripcion"; //valor
                cbxDebilidad.DataSource = elemento.listar();
                cbxDebilidad.ValueMember = "Id";
                cbxDebilidad.DisplayMember = "Descripcion";

                if (pokemon != null)
                {
                    switch (bandera)
                    {
                        case 1:
                            {
                                cbxTipo.Text = tipo.Tipo.ToString();
                                cbxDebilidad.Text = tipo.Debilidad.ToString();
                                txtNumero.Text = pokemon.Numero.ToString();
                                txtNumero.Enabled = false;
                                txtNombre.Text = pokemon.Nombre;
                                txtNombre.Enabled = false;
                                txtDescipcion.Text = pokemon.Descripcion;
                                txtDescipcion.Enabled = false;
                                txtUrlImagen.Text = pokemon.UrlImagen;
                                txtUrlImagen.Enabled = false;
                                CargarImagen(pokemon.UrlImagen);
                                break;
                            }
                        default:
                            txtNumero.Text = pokemon.Numero.ToString();
                            txtNombre.Text = pokemon.Nombre;
                            txtDescipcion.Text = pokemon.Descripcion;
                            txtUrlImagen.Text = pokemon.UrlImagen;
                            cbxTipo.Enabled = false;
                            cbxDebilidad.Enabled = false;
                            CargarImagen(pokemon.UrlImagen);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        // FIXME: este metodo al cargar el id y el numero pueden no coincidir y no matchea los elementos luego

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                Pokemon poke = new Pokemon();
                PokemonNegocio negocio = new PokemonNegocio();
                poke.Numero = int.Parse(txtNumero.Text);
                poke.Nombre = txtNombre.Text;
                poke.Descripcion = txtDescipcion.Text;
                poke.UrlImagen = txtUrlImagen.Text;
                poke.Tipo = (Elemento)cbxTipo.SelectedItem;
                poke.Debilidad = (Elemento)cbxDebilidad.SelectedItem;
                negocio.Insert(poke, negocio.IdMax());
                MessageBox.Show("Pokemon agregado exitosamente");
                Close();
            }
            catch (Exception )
            {
                throw;
            }


        }

        private void txtUrlImagen_Leave(object sender, EventArgs e)
        {
            CargarImagen(txtUrlImagen.Text);
        }

        private void CargarImagen(string imagen)
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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
