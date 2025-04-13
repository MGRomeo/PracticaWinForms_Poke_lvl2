﻿using System;
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
            FiltroDB();
        }

        private void dgvPokemon_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvGeneral.CurrentRow != null)
            {
                ListarElemento();

            }
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
                ListarElemento();
                
            }
        }

        private void btnEliminarFisico_Click(object sender, EventArgs e)
        {
            PokemonNegocio negocio = new PokemonNegocio();
            try
            {
                DialogResult respuesta = MessageBox.Show("¿Realmente quieres Eliminar permanentemente este Pokemon?","Eliminando", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (respuesta == DialogResult.Yes)
                {
                    Pokemon borrar = (Pokemon)dgvGeneral.CurrentRow.DataBoundItem;
                    negocio.Delete(borrar.Id);
                    ListarPokemon();
                    MessageBox.Show("Eliminado físico realizado");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void btnFiltro_Click(object sender, EventArgs e)
        {
            Filtrar();
        }

        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            Filtrar();
        }

        private void Filtrar()
        {
            if ( txtFiltro.Text == "" )
            {
                dgvGeneral.DataSource = listaPokemon;
            }
            else
            {
                List<Pokemon> listaFiltrada;
                listaFiltrada = listaPokemon.FindAll(x => x.Nombre.ToUpper().Contains(txtFiltro.Text.ToUpper()));
                dgvGeneral.DataSource = listaFiltrada;
            }
        }

        private void FiltroDB()
        {
            cbxCampo.Items.Add("Numero");
            cbxCampo.Items.Add("Nombre");
            cbxCampo.Items.Add("Tipo");
        }

        private void cbxCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbxCampo.Text)
            {
                case "Numero":
                    cbxCriterio.Items.Clear();
                    cbxCriterio.Items.Add("Igual a");
                    cbxCriterio.Items.Add("Mayor a");
                    cbxCriterio.Items.Add("Menor a");
                    break;
                case "Nombre":
                    cbxCriterio.Items.Clear();
                    cbxCriterio.Items.Add("Inicie con");
                    cbxCriterio.Items.Add("Termine con");
                    cbxCriterio.Items.Add("Contenga");
                    break;
                case "Tipo":
                    cbxCriterio.Items.Clear();
                    cbxCriterio.Items.Add("Inicie con");
                    cbxCriterio.Items.Add("Termine con");
                    cbxCriterio.Items.Add("Contenga");
                    break;
                default:
                    break;
            }
        }

        private void btnFiltroDB_Click(object sender, EventArgs e)
        {
            PokemonNegocio negocio = new PokemonNegocio();
            try
            {
                string campo = cbxCampo.Text.ToString();
                string criterio = cbxCriterio.Text.ToString();
                string filtro = txtFiltroDB.Text.ToString();

                dgvGeneral.DataSource = negocio.Filtrar(campo, criterio, filtro);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
    }
}
