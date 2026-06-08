using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Pantallas_alto_volumen_de_datos.Entidades;


namespace Pantallas_alto_volumen_de_datos.DAO
{
    public class DetalleReservacionDAO
    {
        public static void Insertar(DetalleReservacion detalle, int idReserva)
        {
            using (SqlConnection conn = BDConexion.ObtenerConexion())
            {
                //conn.Open();
                string query = @"
            INSERT INTO DetalleReservacion (ID_Reserva, ID_Habitacion, ID_Tipo, Precio, NumPersonas)
            VALUES (@ID_Reserva, @ID_Habitacion, @ID_Tipo, @Precio, @NumPersonas)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ID_Reserva", idReserva);
                    cmd.Parameters.AddWithValue("@ID_Habitacion", detalle.ID_Habitacion);
                    cmd.Parameters.AddWithValue("@ID_Tipo", detalle.ID_Tipo);
                    cmd.Parameters.AddWithValue("@Precio", detalle.Precio);
                    cmd.Parameters.AddWithValue("@NumPersonas", detalle.NumPersonas);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static decimal ObtenerSubtotalPorReserva(int idReserva)
        {
            decimal subtotal = 0;

            using (SqlConnection conn = BDConexion.ObtenerConexion())
            {
                string query = "SELECT SUM(Precio) FROM DetalleReservacion WHERE ID_Reserva = @ID_Reserva";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ID_Reserva", idReserva);

                    object resultado = cmd.ExecuteScalar();
                    if (resultado != DBNull.Value)
                    {
                        subtotal = Convert.ToDecimal(resultado);
                    }
                }
            }

            return subtotal;
        }

        public static List<int> ObtenerHabitacionesPorReservacion(int idReserva)
        {
            List<int> habitaciones = new List<int>();

            using (SqlConnection conn = BDConexion.ObtenerConexion())
            {
                string query = "SELECT ID_Habitacion FROM DetalleReservacion WHERE ID_Reserva = @ID_Reserva";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ID_Reserva", idReserva);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        habitaciones.Add(Convert.ToInt32(reader["ID_Habitacion"]));
                    }
                }
            }

            return habitaciones;
        }

        public static List<(string habitacion, decimal precio, int noches, decimal total)> ObtenerHabitacionesPorFactura(int idReserva)
        {
            var lista = new List<(string habitacion, decimal precio, int noches, decimal total)>();

            using (SqlConnection conn = BDConexion.ObtenerConexion())
            {
                string query = @"
            SELECT 
                h.NumeroHabitacion,
                d.Precio,
                DATEDIFF(DAY, r.FechaEntrada, r.FechaSalida) AS Noches
            FROM DetalleReservacion d
            INNER JOIN Habitacion h ON d.ID_Habitacion = h.ID_Hab
            INNER JOIN Reservacion r ON r.ID_Reserva = d.ID_Reserva
            WHERE d.ID_Reserva = @ID_Reserva";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ID_Reserva", idReserva);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string numero = reader["NumeroHabitacion"].ToString();
                        decimal precio = Convert.ToDecimal(reader["Precio"]);
                        int noches = Convert.ToInt32(reader["Noches"]);
                        decimal total = precio * noches;

                        lista.Add((numero, precio, noches, total));
                    }
                }
            }

            return lista;
        }



    }
}
