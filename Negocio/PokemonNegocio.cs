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
        #region Métodos

        // !!! Existen 2 metodos de listar para que no me traiga varias veces el mismo pokemon debido a
        //  que hay pokemon con mas de un tipo
        public List<Pokemon> Listar()
        {
            AccesoDatos datos = new AccesoDatos();
            List<Pokemon> lista = new List<Pokemon>();
            try
            {
                datos.SetearConsulta("select distinct p.Id, p.Numero, p.Nombre, p.Bio, p.ImagenUrl from Pokemons p inner join[Pokemons.Tipos] pk on p.Id = pk.IdPokemon inner join Elementos e on pk.IdElemento = e.Id inner join[Pokemons.Debilidades] pd on p.Id = pd.IdPokemon inner join Elementos d on pd.IdElemento = d.Id");
                datos.EjecutarLectura();
                while (datos.Lector.Read())
                {
                    Pokemon aux = new Pokemon();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Numero = datos.Lector.GetInt32(1);
                    aux.UrlImagen = (string)datos.Lector["ImagenUrl"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Bio"];
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

        // Solo me trae los tipos y debilidades
        public List<Pokemon> ListarTipoDebilidad(int numero)
        {
            AccesoDatos datos = new AccesoDatos();
            List<Pokemon> lista = new List<Pokemon>();
            try
            {
                datos.SetearConsulta($"select e.Descripcion Tipo, d.Descripcion Debilidad from Pokemons p inner join[Pokemons.Tipos] pk on p.Id = pk.IdPokemon inner join Elementos e on pk.IdElemento = e.Id inner join[Pokemons.Debilidades] pd on p.Id = pd.IdPokemon inner join Elementos d on pd.IdElemento = d.Id where p.Numero = {numero} order by e.Descripcion");
                datos.EjecutarLectura();


                while (datos.Lector.Read())
                {
                    Pokemon aux = new Pokemon();
                    aux.Tipo = new Elemento();
                    aux.Tipo.Descripcion = (string)datos.Lector["Tipo"];
                    aux.Debilidad = new Elemento();
                    aux.Debilidad.Descripcion = (string)datos.Lector["Debilidad"];
                    lista.Add(aux);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        // Traigo el Pokemon cargado y el numero de Id maximo desde la DB
        public void Insert(Pokemon poke, int max)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta("insert into Pokemons (Id, Nombre, Numero, Bio, Altura, Peso, ImagenUrl, IdEvolucion ) values('"+ max +"','" + poke.Nombre + " ','" + poke.Numero + "','" + poke.Descripcion + "','','','" + poke.UrlImagen + "','') Insert into [Pokemons.Tipos] (IdPokemon,IdElemento) values(" + max + ",@IdElemento) Insert into[Pokemons.Debilidades](IdPokemon, IdElemento) values(" + max + ",@IdDebilidad)");
                ElementoNegocio elemento = new ElementoNegocio();
                datos.SetearParametros("@IdElemento", elemento.BuscarId(poke.Tipo));
                datos.SetearParametros("@IdDEbilidad", elemento.BuscarId(poke.Debilidad));
                datos.EjecutarAccion();
            }
            catch (Exception)
            {

                throw;
            }
            finally { datos.CerrarConexion(); }
        }

        public void UpdatePokemon(int numero, string nombre, string bio, string url, int idPokemon)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta($"update Pokemons set Numero= {numero} ,Nombre= '{nombre}', Bio='{bio}', ImagenUrl='{url}' where id= {idPokemon}");
                datos.EjecutarAccion();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void UpdateElemento(int numero, string nombre,string bio, string url, int idPokemon, int idTipoViejo,int idTipoNuevo, int idDebViejo, int idDebNuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetearConsulta($"update Pokemons set Numero= {numero} ,Nombre= '{nombre}', Bio='{bio}', ImagenUrl='{url}' where id= {idPokemon} update [Pokemons.Tipos] set IdElemento = {idTipoNuevo} where IdPokemon = {idPokemon} and IdElemento = {idTipoViejo} update [Pokemons.Debilidades] set IdElemento = {idDebNuevo} where IdPokemon = {idPokemon} and IdElemento = {idDebViejo}");
                datos.EjecutarAccion();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Delete(int id)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.SetearConsulta("delete from [Pokemons.Debilidades] where IdPokemon = @id delete from [Pokemons.Tipos] where IdPokemon = @id delete from [Pokemons.Habilidades] where IdPokemon = @id delete from [Evoluciones.Variables] where IdPokemon = @id delete from [Entrenadores.Pokemons] where IdPokemon = @id delete from Pokemons where id = @id");
                datos.SetearParametros("@Id",id);
                datos.EjecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int IdMax()
        {
            int max;
            AccesoDatos dato = new AccesoDatos();
            try
            {
                dato.SetearConsulta("select max(id) Id from Pokemons");
                max = dato.IdMax();
                return max;
            }
            catch (Exception)
            {

                throw ;
            }
            finally { dato.CerrarConexion(); }
        }

        public List<Pokemon> Filtrar(string campo, string criterio, string filtro)
        {
            List<Pokemon> lista = new List<Pokemon>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consultaPokemon = "select Id, Numero, Nombre, Bio, ImagenUrl from Pokemons where ";
                string consultaElemento = "select p.Id, p.Numero ,p.Nombre, p.Bio, p.ImagenUrl from Pokemons p inner join [Pokemons.Tipos] pt on p.id= pt.IdPokemon inner join Elementos e on pt.IdElemento = e.Id where e.";
                if (campo == "Numero")
                {
                    switch (criterio)
                    {
                        case "Igual a":
                            consultaPokemon += "Numero = " + filtro;
                            datos.SetearConsulta(consultaPokemon);
                            break;
                        case "Menor a":
                            consultaPokemon += "Numero < " + filtro;
                            datos.SetearConsulta(consultaPokemon);
                            break;
                        case "Mayor a":
                            consultaPokemon += "Numero > " + filtro;
                            datos.SetearConsulta(consultaPokemon);
                            break;
                    }
                }
                else if (campo == "Nombre")
                {
                    switch (criterio)
                    {
                        case "Inicie con":
                            consultaPokemon += "Nombre like '" + filtro + "%'";
                            datos.SetearConsulta(consultaPokemon);
                            break;
                        case "Termine con":
                            consultaPokemon += "Nombre like '%" + filtro + "'";
                            datos.SetearConsulta(consultaPokemon);
                            break;
                        case "Contenga":
                            consultaPokemon += "Nombre like '%" + filtro + "%'";
                            datos.SetearConsulta(consultaPokemon);
                            break;
                    }
                }
                else
                {
                    switch (criterio)
                    {
                        case "Inicie con":
                            consultaElemento += "Descripcion like '" + filtro + "%'";
                            datos.SetearConsulta(consultaElemento);
                            break;
                        case "Termine con":
                            consultaElemento += "Descripcion like '%" + filtro + "'";
                            datos.SetearConsulta(consultaElemento);
                            break;
                        case "Contenga":
                            consultaElemento += "Descripcion like '%" + filtro + "%'";
                            datos.SetearConsulta(consultaElemento);
                            break;
                    }
                }
                datos.EjecutarLectura();
                while (datos.Lector.Read())
                {
                    Pokemon aux = new Pokemon();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Numero = datos.Lector.GetInt32(1);
                    aux.UrlImagen = (string)datos.Lector["ImagenUrl"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Bio"];
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

        #endregion
    }
}
