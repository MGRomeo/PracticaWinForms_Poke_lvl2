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
        //  pueda diferenciar si es un alta o una modificacion
        private Pokemon pokemon = null;
        private Pokemon tipo = null;
        private int bandera; // para saber si modifico un pokemon o Tipo/debilidad
        public FrmIngreso()
        {
            InitializeComponent();
        }

        // Constructores para modificacion

        //Pokemon
        public FrmIngreso(Pokemon pokemon)
        {
            InitializeComponent();
            Text = "Modificar Pokemon";
            this.pokemon = pokemon;
        } 

        //Tipo y debilidad
        public FrmIngreso(Pokemon pokemon, Pokemon tipo, int bandera)
        {
            InitializeComponent();
            this.pokemon = pokemon;
            this.tipo = tipo;
            this.bandera = bandera;
            Text = "Modificar Tipo/Habilidad";
        }

        #region Metodos

        private void FrmIngreso_Load(object sender, EventArgs e)  // seleccion de formulario vacio o precargado
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
                // lo de arriba no lo uso pq es diferente la db y como muestro los dgv tambien

                if (pokemon != null) // viene un pokemon para modificar, sino formulario vacio
                {
                    switch (bandera)
                    {
                        case 1: // cargo para modificar tipo/Debilidad
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
                        default: // cargo para modificar Pokemon
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

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                if (pokemon == null) //si pokemon es null es un Pokemon nuevo a cargar
                {
                    pokemon = new Pokemon();
                    PokemonNegocio negocio = new PokemonNegocio();
                    pokemon.Numero = int.Parse(txtNumero.Text);
                    pokemon.Nombre = txtNombre.Text;
                    pokemon.Descripcion = txtDescipcion.Text;
                    pokemon.UrlImagen = txtUrlImagen.Text;
                    pokemon.Tipo = (Elemento)cbxTipo.SelectedItem;
                    pokemon.Debilidad = (Elemento)cbxDebilidad.SelectedItem;
                    negocio.Insert(pokemon, negocio.IdMax());
                    MessageBox.Show("Pokemon agregado exitosamente");

                }
                else // no es null, existe pokemon cargado
                {
                    PokemonNegocio negocio = new PokemonNegocio();
                    if (tipo == null) // si tipo es null quiere decir que estoy trabajando con un Pokemon

                    {
                        pokemon.Numero = int.Parse(txtNumero.Text);
                        pokemon.Nombre = txtNombre.Text;
                        pokemon.Descripcion = txtDescipcion.Text;
                        pokemon.UrlImagen = txtUrlImagen.Text;
                        negocio.UpdatePokemon(pokemon.Numero, pokemon.Nombre, pokemon.Descripcion, pokemon.UrlImagen,pokemon.Id);
                    }
                    else // si no es null estoy trabajando con elementos guardados en un objeto pokemon
                    {
                        ElementoNegocio elemento = new ElementoNegocio();
                        pokemon.Numero = int.Parse(txtNumero.Text);
                        pokemon.Nombre = txtNombre.Text;
                        pokemon.Descripcion = txtDescipcion.Text;
                        pokemon.UrlImagen = txtUrlImagen.Text;
                        pokemon.Tipo = (Elemento)cbxTipo.SelectedItem;
                        pokemon.Debilidad = (Elemento)cbxDebilidad.SelectedItem;
                        negocio.UpdateElemento(pokemon.Numero, pokemon.Nombre, pokemon.Descripcion, pokemon.UrlImagen, pokemon.Id, elemento.BuscarId(tipo.Tipo), elemento.BuscarId(pokemon.Tipo), elemento.BuscarId(tipo.Debilidad), elemento.BuscarId(pokemon.Debilidad));
                        MessageBox.Show("Modificación exitosa");
                    }
                    
                    Close();
                }
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

        #endregion
    }
}
