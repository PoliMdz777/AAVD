using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Pantallas_alto_volumen_de_datos.Entidades;
using System.Data;
using System.Windows.Forms;

namespace Pantallas_alto_volumen_de_datos.DAO
{
    public class ReservacionesDAO
    {
        public static int InsertarReservacion(Reservacion reservacion)
        {
            int resultado = 0;

            using (SqlConnection conexion = BDConexion.ObtenerConexion())
            {
                SqlCommand comando = new SqlCommand("SP_InsertarReservacion", conexion);
                comando.CommandType = System.Data.CommandType.StoredProcedure;

                comando.Parameters.AddWithValue("@FormaPago", reservacion.FormaPago);
                comando.Parameters.AddWithValue("@ID_Hotel", reservacion.ID_Hotel);
                comando.Parameters.AddWithValue("@ID_Habitacion", reservacion.ID_Habitacion);
                comando.Parameters.AddWithValue("@FechaEntrada", reservacion.FechaEntrada);
                comando.Parameters.AddWithValue("@FechaSalida", reservacion.FechaSalida);
                comando.Parameters.AddWithValue("@ClienteRFC", reservacion.ClienteRFC);
                comando.Parameters.AddWithValue("@NomCli", reservacion.NomCli);
                comando.Parameters.AddWithValue("@Total", reservacion.Total);
                comando.Parameters.AddWithValue("@Anticipo", reservacion.Anticipo);
                comando.Parameters.AddWithValue("@CodigoReservacion", reservacion.CodigoReservacion);

                resultado = comando.ExecuteNonQuery();
            }

            return resultado;
        }

        public static int InsertarYRetornarID(Reservacion reserva)
        {
            int idGenerado = 0;
            using (SqlConnection conn = BDConexion.ObtenerConexion())
            {
                string query = @"INSERT INTO Reservacion 
                         (FormaPago, ID_Hotel, FechaEntrada, FechaSalida, ClienteRFC, NomCli, Total, Anticipo, CodigoReservacion, FechaRegistro, UsuarioRegistro)
                         VALUES (@FormaPago, @ID_Hotel, @FechaEntrada, @FechaSalida, @ClienteRFC, @NomCli, @Total, @Anticipo, @CodigoReservacion, @FechaRegistro, @UsuarioRegistro);
                         SELECT SCOPE_IDENTITY();";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@FormaPago", reserva.FormaPago);
                cmd.Parameters.AddWithValue("@ID_Hotel", reserva.ID_Hotel);
                cmd.Parameters.AddWithValue("@FechaEntrada", reserva.FechaEntrada);
                cmd.Parameters.AddWithValue("@FechaSalida", reserva.FechaSalida);
                cmd.Parameters.AddWithValue("@ClienteRFC", reserva.ClienteRFC);
                cmd.Parameters.AddWithValue("@NomCli", reserva.NomCli);
                cmd.Parameters.AddWithValue("@Total", reserva.Total);
                cmd.Parameters.AddWithValue("@Anticipo", reserva.Anticipo);
                cmd.Parameters.AddWithValue("@CodigoReservacion", reserva.CodigoReservacion);
                cmd.Parameters.AddWithValue("@FechaRegistro", reserva.FechaRegistro);
                cmd.Parameters.AddWithValue("@UsuarioRegistro", reserva.UsuarioRegistro);

                //conn.Open();
                idGenerado = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return idGenerado;
        }

        //public static Reservacion ObtenerReservacionPorCodigo(Guid codigoReservacion, out string nombreHotel)
        //{
        //    Reservacion reserva = null;
        //    nombreHotel = "";

        //    using (SqlConnection conn = BDConexion.ObtenerConexion())
        //    {
        //        string query = @"
        //    SELECT R.*, H.Nombre
        //    FROM Reservacion R
        //    INNER JOIN Hotel H ON R.ID_Hotel = H.ID_Hotel
        //    WHERE R.CodigoReservacion = @CodigoReservacion";

        //        SqlCommand cmd = new SqlCommand(query, conn);
        //        cmd.Parameters.AddWithValue("@CodigoReservacion", codigoReservacion);

        //        using (SqlDataReader reader = cmd.ExecuteReader())
        //        {
        //            if (reader.Read())
        //            {
        //                reserva = new Reservacion
        //                {
        //                    ID_Reserva = Convert.ToInt32(reader["ID_Reserva"]),
        //                    FormaPago = reader["FormaPago"].ToString(),
        //                    ID_Hotel = Convert.ToInt32(reader["ID_Hotel"]),
        //                    FechaEntrada = Convert.ToDateTime(reader["FechaEntrada"]),
        //                    FechaSalida = Convert.ToDateTime(reader["FechaSalida"]),
        //                    ClienteRFC = reader["ClienteRFC"].ToString(),
        //                    NomCli = reader["NomCli"].ToString(),
        //                    Total = Convert.ToDecimal(reader["Total"]),
        //                    Anticipo = Convert.ToDecimal(reader["Anticipo"]),
        //                    CodigoReservacion = (Guid)reader["CodigoReservacion"],
        //                    FechaRegistro = Convert.ToDateTime(reader["FechaRegistro"]),
        //                    UsuarioRegistro = reader["UsuarioRegistro"].ToString()
        //                };

        //                nombreHotel = reader["Nombre"].ToString();
        //            }
        //        }
        //    }

        //    return reserva;
        //}

        public static Reservacion ObtenerReservacionPorCodigo(Guid codigoReservacion, out string nombreHotel)
        {
            Reservacion reserva = null;
            nombreHotel = "";

            using (SqlConnection conn = BDConexion.ObtenerConexion())
            {
                string query = @"
        SELECT R.*, H.Nombre AS NombreHotel, H.Ciudad
        FROM Reservacion R
        INNER JOIN Hotel H ON R.ID_Hotel = H.ID_Hotel
        WHERE R.CodigoReservacion = @CodigoReservacion";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@CodigoReservacion", codigoReservacion);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        reserva = new Reservacion
                        {
                            ID_Reserva = Convert.ToInt32(reader["ID_Reserva"]),
                            FormaPago = reader["FormaPago"].ToString(),
                            ID_Hotel = Convert.ToInt32(reader["ID_Hotel"]),
                            FechaEntrada = Convert.ToDateTime(reader["FechaEntrada"]),
                            FechaSalida = Convert.ToDateTime(reader["FechaSalida"]),
                            ClienteRFC = reader["ClienteRFC"].ToString(),
                            NomCli = reader["NomCli"].ToString(),
                            Total = Convert.ToDecimal(reader["Total"]),
                            Anticipo = Convert.ToDecimal(reader["Anticipo"]),
                            CodigoReservacion = (Guid)reader["CodigoReservacion"],
                            FechaRegistro = Convert.ToDateTime(reader["FechaRegistro"]),
                            UsuarioRegistro = reader["UsuarioRegistro"].ToString(),
                            Ciudad = reader["Ciudad"].ToString() //  nueva línea
                        };

                        nombreHotel = reader["NombreHotel"].ToString(); // usa el alias del SELECT
                    }
                }
            }

            return reserva;
        }


        public static DataTable ObtenerDetallesPorIDReserva(int idReserva)
        {
            DataTable tabla = new DataTable();

            using (SqlConnection conn = BDConexion.ObtenerConexion())
            {
                string query = @"SELECT * FROM DetalleReservacion WHERE ID_Reserva = @ID_Reserva";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ID_Reserva", idReserva);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tabla);
            }

            return tabla;
        }

        public static Reservacion BuscarPorRFCoCodigo(string entrada, out string nombreHotel, out string ciudad)
        {
            nombreHotel = "";
            ciudad = "";
            Reservacion reservacion = null;

            using (SqlConnection conn = BDConexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                if (Guid.TryParse(entrada, out Guid codigoGuid))
                {
                    cmd.CommandText = @"
                SELECT R.*, H.Nombre, H.Ciudad
                FROM Reservacion R
                INNER JOIN Hotel H ON R.ID_Hotel = H.ID_Hotel
                WHERE R.CodigoReservacion = @Entrada";
                    cmd.Parameters.AddWithValue("@Entrada", codigoGuid);
                }
                else
                {
                    cmd.CommandText = @"
                SELECT TOP 1 R.*, H.Nombre, H.Ciudad
                FROM Reservacion R
                INNER JOIN Hotel H ON R.ID_Hotel = H.ID_Hotel
                WHERE R.ClienteRFC = @Entrada
                ORDER BY R.FechaRegistro DESC";
                    cmd.Parameters.AddWithValue("@Entrada", entrada);
                }

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        reservacion = new Reservacion
                        {
                            ID_Reserva = Convert.ToInt32(reader["ID_Reserva"]),
                            FormaPago = reader["FormaPago"].ToString(),
                            ID_Hotel = Convert.ToInt32(reader["ID_Hotel"]),
                            FechaEntrada = Convert.ToDateTime(reader["FechaEntrada"]),
                            FechaSalida = Convert.ToDateTime(reader["FechaSalida"]),
                            ClienteRFC = reader["ClienteRFC"].ToString(),
                            NomCli = reader["NomCli"].ToString(),
                            Total = Convert.ToDecimal(reader["Total"]),
                            Anticipo = Convert.ToDecimal(reader["Anticipo"]),
                            CodigoReservacion = (Guid)reader["CodigoReservacion"],
                            Estado = reader["Estado"].ToString()

                        };

                        nombreHotel = reader["Nombre"].ToString();
                        ciudad = reader["Ciudad"].ToString(); 
                    }
                }
            }

            return reservacion;
        }

        public static void CancelarReservacionesNoCheckIn()
        {
            using (SqlConnection conn = BDConexion.ObtenerConexion())
            {
                string query = @"
            UPDATE Reservacion
            SET Estado = 'Cancelada',
                UsuarioCancela = 'Sistema',
                FechaCancelacion = GETDATE()
            WHERE Estado = 'Activa'
              AND FechaEntrada < CAST(GETDATE() AS DATE)
              AND ID_Reserva NOT IN (
                  SELECT ID_Reserva FROM DetalleReservacion
              );";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
            }
        }


        public static void CancelarReservacion(int idReserva, DateTime fechaEntrada, decimal anticipo, string usuarioCancela)
        {
            int diasAnticipacion = (fechaEntrada.Date - DateTime.Now.Date).Days;

            if (diasAnticipacion >= 3)
            {
                MessageBox.Show($"Reservación cancelada con éxito.\nSe debe devolver el anticipo de {anticipo:C}.",
                                "Cancelación anticipada", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Reservación cancelada.\nNo aplica devolución de anticipo por cancelarse con menos de 3 días de anticipación.",
                                "Cancelación tardía", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            // Actualizar el estado de la reservación y registrar la cancelación
            using (SqlConnection conn = BDConexion.ObtenerConexion())
            {
                string query = @"
            UPDATE Reservacion 
            SET Estado = 'Cancelada',
                UsuarioCancela = @UsuarioCancela,
                FechaCancelacion = GETDATE()
            WHERE ID_Reserva = @ID_Reserva";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ID_Reserva", idReserva);
                cmd.Parameters.AddWithValue("@UsuarioCancela", usuarioCancela);
                cmd.ExecuteNonQuery();

                // Cambiar estado de habitaciones asociadas
                List<int> habitaciones = DetalleReservacionDAO.ObtenerHabitacionesPorReservacion(idReserva);
                foreach (int idHab in habitaciones)
                {
                    HabitacionDAO.CambiarEstadoHabitacion(idHab, "Disponible");
                }
            }
        }

        public static bool EstaCheckOutRealizado(int idReserva)
        {
            using (SqlConnection conn = BDConexion.ObtenerConexion())
            {
                string query = "SELECT CheckOutRealizado FROM Reservacion WHERE ID_Reserva = @ID_Reserva";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ID_Reserva", idReserva);

                return Convert.ToBoolean(cmd.ExecuteScalar());
            }
        }

        public static bool EstaCheckInRealizado(int idReserva)
        {
            using (SqlConnection conn = BDConexion.ObtenerConexion())
            {
                string query = "SELECT CheckInRealizado FROM Reservacion WHERE ID_Reserva = @ID_Reserva";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ID_Reserva", idReserva);

                return Convert.ToBoolean(cmd.ExecuteScalar());
            }
        }

    }



}

