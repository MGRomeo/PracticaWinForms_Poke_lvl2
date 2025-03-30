using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Negocio
{
    public class AccesoDatos
    {
        private SqlConnection conexion;
        private SqlCommand comando;
        private SqlDataReader lector;

        // Para acceder desde el exterior (desde EjecutarLectura() )
        public SqlDataReader Lector
        {
            get
            {
                return lector;
            }
        }

        // Constructor con instancias de conexion creadas
        public AccesoDatos()
        {
            conexion = new SqlConnection("server =.\\SQLEXPRESS;database=MundoPokemon_DB;integrated security = true");
            comando = new SqlCommand();
        }
        
        public void SeteatConsulta(string consulta)
        {
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = consulta;
        }

        public void EjecutarLectura()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                lector = comando.ExecuteReader();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void CerrarConexion()
        {
            if (lector != null) lector.Close();
            conexion.Close();
        }
    
    }
}
