using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace Pantallas_alto_volumen_de_datos.DAO
{
    public class ReporteVentasDAO
    {
        public static DataTable ObtenerReporteVentas(string pais, string ciudad, string hotel, int? anio)
        {
            using (SqlConnection conn = BDConexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("SP_ReporteVentas", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Pais", (object)pais ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Ciudad", (object)ciudad ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Hotel", (object)hotel ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Anio", (object)anio ?? DBNull.Value);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                // Verifica que existan las columnas necesarias
                if (!dt.Columns.Contains("IngresosHospedaje") || !dt.Columns.Contains("IngresosServiciosAdicionales"))
                    throw new Exception("Faltan columnas requeridas en el resultado del SP");

                // Agrega la columna de ingresos totales
                if (!dt.Columns.Contains("IngresosTotales"))
                    dt.Columns.Add("IngresosTotales", typeof(decimal));

                foreach (DataRow row in dt.Rows)
                {
                    decimal hospedaje = row["IngresosHospedaje"] != DBNull.Value ? Convert.ToDecimal(row["IngresosHospedaje"]) : 0;
                    decimal servicios = row["IngresosServiciosAdicionales"] != DBNull.Value ? Convert.ToDecimal(row["IngresosServiciosAdicionales"]) : 0;
                    row["IngresosTotales"] = hospedaje + servicios;
                }

                return dt;
            }
        }

    }
}
