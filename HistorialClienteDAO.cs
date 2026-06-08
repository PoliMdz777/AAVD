using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Pantallas_alto_volumen_de_datos.DAO
{
    public class HistorialClienteDAO
    {

        public static DataTable ObtenerHistorialCliente(string rfc, string anioTexto)
        {
            using (SqlConnection conn = BDConexion.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand("SP_HistorialCliente", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@RFCCliente", rfc);

                    // Manejo del año como opcional
                    if (anioTexto == "Todos" || string.IsNullOrEmpty(anioTexto))
                        cmd.Parameters.AddWithValue("@Anio", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@Anio", Convert.ToInt32(anioTexto));

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    return dt;
                }
            }
        }

    }
}
