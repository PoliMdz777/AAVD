using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pantallas_alto_volumen_de_datos
{
    public class BDConexion
    {
        public static SqlConnection ObtenerConexion()
        {
            SqlConnection conexion = new SqlConnection("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=MAD_DB;Data Source=LAPTOP-GCOK6133\\SQLEXPRESS");

            //conexion.Open();
            return null;
        }
    }
}
