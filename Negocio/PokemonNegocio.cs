using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dominio;

namespace Negocio

{
     public class PokemonNegocio
    {
        public List<Pokemon> Listar()
        {
            AccesoDatos datos = new AccesoDatos();

            List<Pokemon> lista = new List<Pokemon>();
            try
            {
                datos.SeteatConsulta("select p.Numero, p.Nombre, p.ImagenUrl, e.Descripcion Tipo, d.Descripcion Debilidad from Pokemons p inner join[Pokemons.Tipos] pk on p.Id = pk.IdPokemon inner join Elementos e on pk.IdElemento = e.Id inner join[Pokemons.Debilidades] pd on p.Id = pd.IdPokemon inner join Elementos d on pd.IdElemento = d.Id");
                datos.EjecutarLectura();

                //conexion.ConnectionString = "server =.\\SQLEXPRESS;database=MundoPokemon_DB;integrated security = true";
                //comando.CommandType = System.Data.CommandType.Text;
                //comando.CommandText = "select p.Numero, p.Nombre, p.ImagenUrl, e.Descripcion Tipo, d.Descripcion Debilidad from Pokemons p inner join[Pokemons.Tipos] pk on p.Id = pk.IdPokemon inner join Elementos e on pk.IdElemento = e.Id inner join[Pokemons.Debilidades] pd on p.Id = pd.IdPokemon inner join Elementos d on pd.IdElemento = d.Id";
                //comando.Connection = conexion;
                //conexion.Open();
                //lector = comando.ExecuteReader();

                while (datos.Lector.Read())
                {
                    Pokemon aux = new Pokemon();
                    aux.Numero = datos.Lector.GetInt32(0);
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.UrlImagen = (string)datos.Lector["ImagenUrl"];
                    aux.Tipo = new Elemento();
                    aux.Tipo.Descripcion = (string)datos.Lector["Tipo"];
                    aux.Debilidad = new Elemento();
                    aux.Debilidad.Descripcion = (string)datos.Lector["Debilidad"];

                    lista.Add(aux);
                }
                return lista;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        #region Métodos

        public void Insert(Pokemon poke, int max)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SeteatConsulta("insert into Pokemons (Id, Nombre, Numero, Bio, Altura, Peso, ImagenUrl, IdEvolucion ) values('"+ max +"','" + poke.Nombre + " ','" + poke.Numero + "','" + poke.Descripcion + "','','','','')");
                datos.Subir();
            }
            catch (Exception)
            {

                throw;
            }
            finally { datos.CerrarConexion(); }
        }

        public int IdMax()
        {
            int max;
            AccesoDatos dato = new AccesoDatos();
            try
            {
                dato.SeteatConsulta("select max(id) Id from Pokemons");
                max = dato.IdMax();
                return max;
            }
            catch (Exception)
            {

                throw ;
            }
            finally { dato.CerrarConexion(); }
        }

        #endregion
    }
}
