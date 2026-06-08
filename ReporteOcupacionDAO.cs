using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace Pantallas_alto_volumen_de_datos.DAO
{
    public class ReporteOcupacionDAO
    {

        public static DataTable ObtenerReporteOcupacionHotel(string pais, string ciudad, string hotel, int? anio)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = BDConexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("SP_ReporteOcupacionHotel", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                // Parámetros con manejo de nulls
                cmd.Parameters.AddWithValue("@Pais", (object)pais ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Ciudad", (object)ciudad ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Hotel", (object)hotel ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Anio", (object)anio ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Mes", DBNull.Value); // si no usas mes, envíalo como NULL

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }

            return dt;
        }


    }
}
