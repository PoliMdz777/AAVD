using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Pantallas_alto_volumen_de_datos.DAO
{
    public class FacturasDAO
    {
        public static int InsertarFactura(
         int idReserva, int idCliente, string nombreFiscal, string rfc, string domicilioFiscal,
          string usoCFDI, string formaPago, decimal total, bool emitida, int idUsuario,
          decimal anticipo, string rutaPDF, string rutaXML, string foliointerno)
        {
            using (SqlConnection conn = BDConexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("SP_InsertarFactura", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                decimal subtotal = total / 1.18m;
                decimal iva = total - subtotal;
                string folioFiscal = Guid.NewGuid().ToString();

                cmd.Parameters.AddWithValue("@ID_Reserva", idReserva);
                cmd.Parameters.AddWithValue("@ID_Cliente", idCliente);
                cmd.Parameters.AddWithValue("@NombreFiscal", nombreFiscal);
                cmd.Parameters.AddWithValue("@RFC", rfc);
                cmd.Parameters.AddWithValue("@DomicilioFiscal", domicilioFiscal);
                cmd.Parameters.AddWithValue("@UsoCFDI", usoCFDI);
                cmd.Parameters.AddWithValue("@MetodoPago", formaPago);
                cmd.Parameters.AddWithValue("@Total", total);
                cmd.Parameters.AddWithValue("@Subtotal", subtotal);
                cmd.Parameters.AddWithValue("@IVA", iva);
                cmd.Parameters.AddWithValue("@Emitida", emitida);
                cmd.Parameters.AddWithValue("@ID_Usuario", idUsuario);
                cmd.Parameters.AddWithValue("@Anticipo", anticipo);
                cmd.Parameters.AddWithValue("@FolioFiscal", folioFiscal);
                cmd.Parameters.AddWithValue("@RutaPDF", rutaPDF);
                cmd.Parameters.AddWithValue("@RutaXML", rutaXML);
                cmd.Parameters.AddWithValue("@FolioInterno", foliointerno);

                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }






        public static void InsertarServicioFactura(int idFactura, int idServicio, decimal precio)
        {
            using (SqlConnection conn = BDConexion.ObtenerConexion())
            {
                string query = "INSERT INTO ServicioAdicionalFactura (ID_Factura, ID_Servicio, Precio) VALUES (@ID_Factura, @ID_Servicio, @Precio)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ID_Factura", idFactura);
                cmd.Parameters.AddWithValue("@ID_Servicio", idServicio);
                cmd.Parameters.AddWithValue("@Precio", precio);
                cmd.ExecuteNonQuery();
            }
        }


        public static DataTable ObtenerFacturasFiltradas(string cliente, int? idHotel, DateTime? fechaInicio, DateTime? fechaFin)
        {
            DataTable tabla = new DataTable();

            using (SqlConnection conn = BDConexion.ObtenerConexion())
            {
                StringBuilder query = new StringBuilder(@"
            SELECT F.ID_Factura, F.Fecha, F.NombreFiscal, F.RFC, F.Total, F.MetodoPago, F.UsoCFDI, H.Nombre AS Hotel
            FROM Factura F
            INNER JOIN Reservacion R ON F.ID_Reserva = R.ID_Reserva
            INNER JOIN Hotel H ON R.ID_Hotel = H.ID_Hotel
            WHERE 1 = 1");

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                if (!string.IsNullOrEmpty(cliente))
                {
                    query.Append(" AND (F.NombreFiscal LIKE @Cliente OR F.RFC LIKE @Cliente) ");
                    cmd.Parameters.AddWithValue("@Cliente", "%" + cliente + "%");
                }

                if (idHotel.HasValue)
                {
                    query.Append(" AND H.ID_Hotel = @IDHotel ");
                    cmd.Parameters.AddWithValue("@IDHotel", idHotel.Value);
                }

                if (fechaInicio.HasValue && fechaFin.HasValue)
                {
                    query.Append(" AND F.Fecha BETWEEN @FechaInicio AND @FechaFin ");
                    cmd.Parameters.AddWithValue("@FechaInicio", fechaInicio.Value.Date);
                    cmd.Parameters.AddWithValue("@FechaFin", fechaFin.Value.Date.AddDays(1).AddTicks(-1)); // hasta el final del día
                }

                cmd.CommandText = query.ToString();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tabla);
            }

            return tabla;
        }




    }
}
