using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dominio;

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

        #region Métodos

        public void SeteatConsulta(string consulta)
        {
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = consulta;
        }

        //Obtengo el maximo Id desde DB
        public int IdMax()
        {
            int max;
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                //Esto me trae solamente un Numero
                max = Convert.ToInt32(comando.ExecuteScalar()) +1 ;
                return max;
            }

            catch (Exception)
            {

                throw;
            }
        }

        //Este comando agrega al comando el nombre de un parametro, ej. @Id tipo,
        // y le inserta un valor
        public void SetearParametros(string nombreParametro, object valor)
        {
            comando.Parameters.AddWithValue(nombreParametro, valor);
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

        public void Subir()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                comando.ExecuteNonQuery();
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
        #endregion

    }
}
