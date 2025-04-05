using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class ElementoNegocio
    {
        public List<Elemento> listar()
        {
            List<Elemento> lista = new List<Elemento>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SeteatConsulta("Select Id, Descripcion From Elementos");
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {
                    Elemento aux = new Elemento();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];

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

        public int BuscarId(Elemento elemento)
        {
            List<Elemento> lista = new List<Elemento>();
            int Id = -1; // valor aleatorio para que no chille
            lista = listar();
            foreach (var item in lista)
            {
                if (item.Descripcion == elemento.Descripcion)
                {
                    Id = item.Id;
                    break;
                }
            }
            return Id;
        }
    }
}
