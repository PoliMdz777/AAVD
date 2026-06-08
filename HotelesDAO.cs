using Pantallas_alto_volumen_de_datos.Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Pantallas_alto_volumen_de_datos.DAO
{
    public class HotelesDAO
    {
        public static int InsertarHotel(Hoteles hotel, List<int> serviciosSeleccionados)
        {
            int retorno = 0;
            int idHotelGenerado = 0;

            using (SqlConnection conexion = BDConexion.ObtenerConexion())
            {
                // 1. Ejecutar el Stored Procedure para insertar el hotel
                string query = @"EXEC InsertarHotel 
                                 @CodigoSAT, @Nombre, @UnidadServicio, @NumPisos, @Vista,
                                 @Piscina, @SalonEventos, @FechaInicioOps, @ZonaTuristica,
                                 @Domicilio, @Usuario, @FechaHoraRegistro, @Ciudad, @Estado, @Pais";

                SqlCommand comando = new SqlCommand(query, conexion);

                comando.Parameters.AddWithValue("@CodigoSAT", hotel.CodigoSAT);
                comando.Parameters.AddWithValue("@Nombre", hotel.Nombre);
                comando.Parameters.AddWithValue("@UnidadServicio", hotel.UnidadServicio);
                comando.Parameters.AddWithValue("@NumPisos", hotel.NumPisos);
                comando.Parameters.AddWithValue("@Vista", hotel.Vista);
                comando.Parameters.AddWithValue("@Piscina", hotel.Piscina);
                comando.Parameters.AddWithValue("@SalonEventos", hotel.SalonEventos);
                comando.Parameters.AddWithValue("@FechaInicioOps", hotel.FechaInicioOps);
                comando.Parameters.AddWithValue("@ZonaTuristica", hotel.ZonaTuristica);
                comando.Parameters.AddWithValue("@Domicilio", hotel.Domicilio);
                comando.Parameters.AddWithValue("@Usuario", hotel.Usuario);
                comando.Parameters.AddWithValue("@FechaHoraRegistro", hotel.FechaHoraRegistro);
                comando.Parameters.AddWithValue("@Ciudad", hotel.Ciudad);
                comando.Parameters.AddWithValue("@Estado", hotel.Estado);
                comando.Parameters.AddWithValue("@Pais", hotel.Pais);

                retorno = comando.ExecuteNonQuery();
            }

            // 2. Obtener el ID del último hotel insertado
            using (SqlConnection conexion = BDConexion.ObtenerConexion())
            {
                string queryID = "SELECT MAX(ID_Hotel) FROM Hotel;";
                SqlCommand cmd = new SqlCommand(queryID, conexion);
                idHotelGenerado = Convert.ToInt32(cmd.ExecuteScalar());
            }

            // 3. Insertar servicios adicionales seleccionados
            if (idHotelGenerado > 0 && serviciosSeleccionados != null)
            {
                using (SqlConnection conexion = BDConexion.ObtenerConexion())
                {
                    foreach (int idServicio in serviciosSeleccionados)
                    {
                        string queryServicios = @"INSERT INTO ServicioPorHotel (ID_Hotel, ID_Servicio)
                                                  VALUES (@ID_Hotel, @ID_Servicio);";

                        SqlCommand cmd = new SqlCommand(queryServicios, conexion);
                        cmd.Parameters.AddWithValue("@ID_Hotel", idHotelGenerado);
                        cmd.Parameters.AddWithValue("@ID_Servicio", idServicio);
                        cmd.ExecuteNonQuery();
                    }
                }
            }

            return retorno;
        }

        public static List<Hoteles> ObtenerTodosLosHoteles()
        {
            List<Hoteles> lista = new List<Hoteles>();

            using (SqlConnection conexion = BDConexion.ObtenerConexion())
            {
                // Ahora solo trae los hoteles activos
                string query = "SELECT * FROM Hotel WHERE Activo = 1;";
                SqlCommand comando = new SqlCommand(query, conexion);
                SqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    Hoteles hotel = new Hoteles();
                    hotel.ID_Hotel = reader.GetInt32(0);
                    hotel.CodigoSAT = reader.GetString(1);
                    hotel.Nombre = reader.GetString(2);
                    hotel.UnidadServicio = reader.GetString(3);
                    hotel.NumPisos = reader.GetInt32(4);
                    hotel.Vista = reader.GetString(5);
                    hotel.SalonEventos = reader.GetBoolean(6);
                    hotel.FechaInicioOps = reader.GetDateTime(7);
                    hotel.Domicilio = reader.GetString(8);
                    hotel.Usuario = reader.GetString(9);
                    hotel.FechaHoraRegistro = reader.GetDateTime(10);
                    hotel.Ciudad = reader.GetString(11);
                    hotel.Estado = reader.GetString(12);
                    hotel.Pais = reader.GetString(13);
                    hotel.ZonaTuristica = reader.GetBoolean(14);
                    hotel.Piscina = reader.GetInt32(15);

                    lista.Add(hotel);
                }
            }

            return lista;
        }

        public static List<int> ObtenerServiciosPorHotel(int idHotel)
        {
            List<int> servicios = new List<int>();

            using (SqlConnection conexion = BDConexion.ObtenerConexion())
            {
                string query = "SELECT ID_Servicio FROM ServicioPorHotel WHERE ID_Hotel = @ID_Hotel;";
                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@ID_Hotel", idHotel);

                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    servicios.Add(reader.GetInt32(0)); // Agregamos cada ID_Servicio
                }
            }

            return servicios;
        }


        public static int DarDeBajaHotel(int idHotel)
        {
              int retorno = 0;

             using (SqlConnection conexion = BDConexion.ObtenerConexion())
             {
                string query = "UPDATE Hotel SET Activo = 0 WHERE ID_Hotel = @ID_Hotel;";
                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@ID_Hotel", idHotel);

                retorno = comando.ExecuteNonQuery();
             }

           return retorno;
        }


        public static int ActualizarHotel(Hoteles hotel)
        {
            int retorno = 0;

            using (SqlConnection conexion = BDConexion.ObtenerConexion())
            {
                string query = @"UPDATE Hotel SET
                         CodigoSAT = @CodigoSAT,
                         Nombre = @Nombre,
                         UnidadServicio = @UnidadServicio,
                         NumPisos = @NumPisos,
                         Vista = @Vista,
                         SalonEventos = @SalonEventos,
                         FechaInicioOps = @FechaInicioOps,
                         Domicilio = @Domicilio,
                         Usuario = @Usuario,
                         FechaHoraRegistro = @FechaHoraRegistro,
                         Ciudad = @Ciudad,
                         Estado = @Estado,
                         Pais = @Pais,
                         ZonaTuristica = @ZonaTuristica,
                         Piscina = @Piscina
                         WHERE ID_Hotel = @ID_Hotel;";

                SqlCommand comando = new SqlCommand(query, conexion);

                comando.Parameters.AddWithValue("@CodigoSAT", hotel.CodigoSAT);
                comando.Parameters.AddWithValue("@Nombre", hotel.Nombre);
                comando.Parameters.AddWithValue("@UnidadServicio", hotel.UnidadServicio);
                comando.Parameters.AddWithValue("@NumPisos", hotel.NumPisos);
                comando.Parameters.AddWithValue("@Vista", hotel.Vista);
                comando.Parameters.AddWithValue("@SalonEventos", hotel.SalonEventos);
                comando.Parameters.AddWithValue("@FechaInicioOps", hotel.FechaInicioOps);
                comando.Parameters.AddWithValue("@Domicilio", hotel.Domicilio);
                comando.Parameters.AddWithValue("@Usuario", hotel.Usuario);
                comando.Parameters.AddWithValue("@FechaHoraRegistro", hotel.FechaHoraRegistro);
                comando.Parameters.AddWithValue("@Ciudad", hotel.Ciudad);
                comando.Parameters.AddWithValue("@Estado", hotel.Estado);
                comando.Parameters.AddWithValue("@Pais", hotel.Pais);
                comando.Parameters.AddWithValue("@ZonaTuristica", hotel.ZonaTuristica);
                comando.Parameters.AddWithValue("@Piscina", hotel.Piscina);
                comando.Parameters.AddWithValue("@ID_Hotel", hotel.ID_Hotel);

                retorno = comando.ExecuteNonQuery();
            }

            return retorno;
        }

        public static void ActualizarServiciosHotel(int idHotel, List<int> nuevosServicios)
        {
            using (SqlConnection conexion = BDConexion.ObtenerConexion())
            {
                // 1. Eliminar los servicios anteriores
                string deleteQuery = "DELETE FROM ServicioPorHotel WHERE ID_Hotel = @ID_Hotel;";
                SqlCommand deleteCmd = new SqlCommand(deleteQuery, conexion);
                deleteCmd.Parameters.AddWithValue("@ID_Hotel", idHotel);
                deleteCmd.ExecuteNonQuery();

                // 2. Insertar los nuevos servicios
                foreach (int idServicio in nuevosServicios)
                {
                    string insertQuery = @"INSERT INTO ServicioPorHotel (ID_Hotel, ID_Servicio)
                                   VALUES (@ID_Hotel, @ID_Servicio);";
                    SqlCommand insertCmd = new SqlCommand(insertQuery, conexion);
                    insertCmd.Parameters.AddWithValue("@ID_Hotel", idHotel);
                    insertCmd.Parameters.AddWithValue("@ID_Servicio", idServicio);
                    insertCmd.ExecuteNonQuery();
                }
            }
        }

        public static List<Hoteles> ObtenerHotelesPorCiudad(string ciudad)
        {
            List<Hoteles> lista = new List<Hoteles>();

            using (SqlConnection conexion = BDConexion.ObtenerConexion())
            {
                SqlCommand comando = new SqlCommand("SELECT ID_Hotel, Nombre FROM Hotel WHERE Ciudad = @Ciudad", conexion);
                comando.Parameters.AddWithValue("@Ciudad", ciudad);

                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Hoteles h = new Hoteles
                    {
                        ID_Hotel = Convert.ToInt32(reader["ID_Hotel"]),
                        Nombre = reader["Nombre"].ToString(),

                       
                    };
                    lista.Add(h);
                }
                reader.Close();
            }

            return lista;
        }


        public class InfoHotelFactura
        {
            public string Nombre { get; set; }
            public string Ciudad { get; set; }
            public string Estado { get; set; }
            public string Pais { get; set; }
            public string CodigoSAT { get; set; }
            public string UnidadServicio { get; set; }
        }

        public static InfoHotelFactura ObtenerDatosHotelPorID(int idHotel)
        {
            InfoHotelFactura datos = null;

            using (SqlConnection conn = BDConexion.ObtenerConexion())
            {
                string query = @"SELECT Nombre, Ciudad, Estado, Pais, CodigoSAT, UnidadServicio
                         FROM Hotel
                         WHERE ID_Hotel = @ID_Hotel";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ID_Hotel", idHotel);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        datos = new InfoHotelFactura
                        {
                            Nombre = reader["Nombre"].ToString(),
                            Ciudad = reader["Ciudad"].ToString(),
                            Estado = reader["Estado"].ToString(),
                            Pais = reader["Pais"].ToString(),
                            CodigoSAT = reader["CodigoSAT"].ToString(),
                            UnidadServicio = reader["UnidadServicio"].ToString()
                        };
                    }
                }
            }

            return datos;
        }


    }
}
