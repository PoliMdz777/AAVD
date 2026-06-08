using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pantallas_alto_volumen_de_datos.Entidades;
using System.Data.SqlClient;

namespace Pantallas_alto_volumen_de_datos.DAO
{
    public class TipoHabitacionDAO
    {
        public static int InsertarTipoHabitacion(TipoHabitacion tipoHabitacion)
        {
            int retorno = 0;

            using (SqlConnection conexion = BDConexion.ObtenerConexion())
            {
                string query = "EXEC InsertarTipoHabitacion @Nombre, @NumCamas, @TipoCama, @CantPersonas, @PrecioPorPersona, @CantHabitaciones, @NivelHabitacion, @Dimensiones, @Caracteristicas, @Amenidades, @NumInicial, @NumFinal, @UsuarioRegistra, @FechaHoraRegistro, @ID_Hotel, @Vista";
                SqlCommand comando = new SqlCommand(query, conexion);

                comando.Parameters.AddWithValue("@Nombre", tipoHabitacion.Nombre);
                comando.Parameters.AddWithValue("@NumCamas", tipoHabitacion.NumCamas);
                comando.Parameters.AddWithValue("@TipoCama", tipoHabitacion.TipoCama);
                comando.Parameters.AddWithValue("@CantPersonas", tipoHabitacion.CantPersonas);
                comando.Parameters.AddWithValue("@PrecioPorPersona", tipoHabitacion.PrecioPorPersona);
                comando.Parameters.AddWithValue("@CantHabitaciones", tipoHabitacion.CantHabitaciones);
                comando.Parameters.AddWithValue("@NivelHabitacion", tipoHabitacion.NivelHabitacion);
                comando.Parameters.AddWithValue("@Dimensiones", tipoHabitacion.Dimensiones);
                comando.Parameters.AddWithValue("@Caracteristicas", tipoHabitacion.Caracteristicas);
                comando.Parameters.AddWithValue("@Amenidades", tipoHabitacion.Amenidades);
                comando.Parameters.AddWithValue("@NumInicial", tipoHabitacion.NumInicial);
                comando.Parameters.AddWithValue("@NumFinal", tipoHabitacion.NumFinal);
                comando.Parameters.AddWithValue("@UsuarioRegistra", tipoHabitacion.UsuarioRegistra);
                comando.Parameters.AddWithValue("@FechaHoraRegistro", tipoHabitacion.FechaHoraRegistro);
                comando.Parameters.AddWithValue("@ID_Hotel", tipoHabitacion.ID_Hotel);
                comando.Parameters.AddWithValue("@Vista", tipoHabitacion.Vista);

                retorno = comando.ExecuteNonQuery();
            }

            return retorno;
        }


        public static List<TipoHabitacion> ObtenerTiposHabitacionPorHotel(string nombreHotel)
        {
            List<TipoHabitacion> lista = new List<TipoHabitacion>();

            using (SqlConnection conexion = BDConexion.ObtenerConexion())
            {
                string query = @"SELECT th.*
                 FROM TiposHabitacion th
                 INNER JOIN Hotel h ON th.ID_Hotel = h.ID_Hotel
                 WHERE h.Nombre LIKE @NombreHotel + '%' AND th.Activo = 1";

                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@NombreHotel", nombreHotel);

                SqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    TipoHabitacion tipo = new TipoHabitacion
                    {
                        ID_Tipo = reader.GetInt32(0),
                        Nombre = reader.GetString(1),
                        NumCamas = reader.GetInt32(2),
                        TipoCama = reader.GetString(3),
                        CantPersonas = reader.GetInt32(4),
                        PrecioPorPersona = reader.GetDecimal(5),
                        CantHabitaciones = reader.GetInt32(6),
                        NivelHabitacion = reader.GetString(7),
                        Dimensiones = reader.GetString(8),
                        Caracteristicas = reader.GetString(9),
                        Amenidades = reader.GetString(10),
                        NumInicial = reader.GetInt32(11),
                        NumFinal = reader.GetInt32(12),
                        UsuarioRegistra = reader.GetString(13),
                        FechaHoraRegistro = reader.GetDateTime(14),
                        ID_Hotel = reader.GetInt32(15)
                    };

                    lista.Add(tipo);
                }
            }

            return lista;
        }

        public static List<TipoHabitacion> ObtenerTodosLosTiposHabitacion()
        {
            List<TipoHabitacion> lista = new List<TipoHabitacion>();

            using (SqlConnection conexion = BDConexion.ObtenerConexion())
            {
                string query = "SELECT * FROM TiposHabitacion WHERE Activo = 1";
                SqlCommand comando = new SqlCommand(query, conexion);
                SqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    TipoHabitacion tipo = new TipoHabitacion
                    {
                        ID_Tipo = reader.GetInt32(0),
                        Nombre = reader.GetString(1),
                        NumCamas = reader.GetInt32(2),
                        TipoCama = reader.GetString(3),
                        CantPersonas = reader.GetInt32(4),
                        PrecioPorPersona = reader.GetDecimal(5),
                        CantHabitaciones = reader.GetInt32(6),
                        NivelHabitacion = reader.GetString(7),
                        Dimensiones = reader.GetString(8),
                        Caracteristicas = reader.GetString(9),
                        Amenidades = reader.GetString(10),
                        NumInicial = reader.GetInt32(11),
                        NumFinal = reader.GetInt32(12),
                        UsuarioRegistra = reader.GetString(13),
                        FechaHoraRegistro = reader.GetDateTime(14),
                        ID_Hotel = reader.GetInt32(15)
                    };

                    lista.Add(tipo);
                }
            }

            return lista;
        }

        public static TipoHabitacion ObtenerPorID(int idTipo)
        {
            using (SqlConnection conexion = BDConexion.ObtenerConexion())
            {
                string query = "SELECT * FROM TiposHabitacion WHERE ID_Tipo = @ID_Tipo";
                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@ID_Tipo", idTipo);

                SqlDataReader reader = comando.ExecuteReader();
                if (reader.Read())
                {
                    return new TipoHabitacion
                    {
                        ID_Tipo = reader.GetInt32(0),
                        Nombre = reader.GetString(1),
                        NumCamas = reader.GetInt32(2),
                        TipoCama = reader.GetString(3),
                        CantPersonas = reader.GetInt32(4),
                        PrecioPorPersona = reader.GetDecimal(5),
                        CantHabitaciones = reader.GetInt32(6),
                        NivelHabitacion = reader.GetString(7),
                        Dimensiones = reader.GetString(8),
                        Caracteristicas = reader.GetString(9),
                        Amenidades = reader.GetString(10),
                        NumInicial = reader.GetInt32(11),
                        NumFinal = reader.GetInt32(12),
                        UsuarioRegistra = reader.GetString(13),
                        FechaHoraRegistro = reader.GetDateTime(14),
                        ID_Hotel = reader.GetInt32(15)
                    };
                }
            }

            return null;
        }

        //public static int ObtenerIdHotelPorTipoHabitacion(int idTipo)
        //{
        //    int idHotel = 0;

        //    using (SqlConnection conexion = BDConexion.ObtenerConexion())
        //    {
        //        string query = "SELECT ID_Hotel FROM TiposHabitacion WHERE ID_Tipo = @ID_Tipo";
        //        SqlCommand comando = new SqlCommand(query, conexion);
        //        comando.Parameters.AddWithValue("@ID_Tipo", idTipo);

        //        object resultado = comando.ExecuteScalar();
        //        if (resultado != null)
        //        {
        //            idHotel = Convert.ToInt32(resultado);
        //        }
        //    }

        //    return idHotel;
        //}

        public static int ObtenerIdHotelPorTipoHabitacion(int idTipo)
        {
            int idHotel = 0;

            using (SqlConnection conexion = BDConexion.ObtenerConexion())
            {
                string query = "SELECT ID_Hotel FROM TiposHabitacion WHERE ID_Tipo = @ID_Tipo";
                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@ID_Tipo", idTipo);

                object resultado = comando.ExecuteScalar();
                if (resultado != null)
                {
                    idHotel = Convert.ToInt32(resultado);
                }
            }

            return idHotel;
        }

        public static int ActualizarTipoHabitacion(TipoHabitacion tipoHabitacion)
        {
            int retorno = 0;

            using (SqlConnection conexion = BDConexion.ObtenerConexion())
            {
                string query = @"UPDATE TiposHabitacion SET 
                                    Nombre = @Nombre,
                                    NumCamas = @NumCamas,
                                    TipoCama = @TipoCama,
                                    CantPersonas = @CantPersonas,
                                    PrecioPorPersona = @PrecioPorPersona,
                                    CantHabitaciones = @CantHabitaciones,
                                    NivelHabitacion = @NivelHabitacion,
                                    Dimensiones = @Dimensiones,
                                    Caracteristicas = @Caracteristicas,
                                    Amenidades = @Amenidades,
                                    NumInicial = @NumInicial,
                                    NumFinal = @NumFinal,
                                    Vista = @Vista
                                 WHERE ID_Tipo = @ID_Tipo";

                SqlCommand comando = new SqlCommand(query, conexion);

                comando.Parameters.AddWithValue("@Nombre", tipoHabitacion.Nombre);
                comando.Parameters.AddWithValue("@NumCamas", tipoHabitacion.NumCamas);
                comando.Parameters.AddWithValue("@TipoCama", tipoHabitacion.TipoCama);
                comando.Parameters.AddWithValue("@CantPersonas", tipoHabitacion.CantPersonas);
                comando.Parameters.AddWithValue("@PrecioPorPersona", tipoHabitacion.PrecioPorPersona);
                comando.Parameters.AddWithValue("@CantHabitaciones", tipoHabitacion.CantHabitaciones);
                comando.Parameters.AddWithValue("@NivelHabitacion", tipoHabitacion.NivelHabitacion);
                comando.Parameters.AddWithValue("@Dimensiones", tipoHabitacion.Dimensiones);
                comando.Parameters.AddWithValue("@Caracteristicas", tipoHabitacion.Caracteristicas);
                comando.Parameters.AddWithValue("@Amenidades", tipoHabitacion.Amenidades);
                comando.Parameters.AddWithValue("@NumInicial", tipoHabitacion.NumInicial);
                comando.Parameters.AddWithValue("@NumFinal", tipoHabitacion.NumFinal);
                comando.Parameters.AddWithValue("@Vista", tipoHabitacion.Vista);
                comando.Parameters.AddWithValue("@ID_Tipo", tipoHabitacion.ID_Tipo);

                retorno = comando.ExecuteNonQuery();
            }

            return retorno;
        }

   
        // NUEVO: Baja lógica
        

        // Dar de baja lógica también habitaciones relacionadas
        private static void DarDeBajaHabitacionesPorTipo(int idTipo)
        {
            using (SqlConnection conexion = BDConexion.ObtenerConexion())
            {
                string query = "UPDATE Habitacion SET Status = 'Inactivo' WHERE ID_Tipo = @ID_Tipo";
                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@ID_Tipo", idTipo);
                comando.ExecuteNonQuery();
            }
        }

        public static int DarDeBajaTipoHabitacion(int idTipo)
        {
            int retorno = 0;

            using (SqlConnection conexion = BDConexion.ObtenerConexion())
            {
                string query = "UPDATE TiposHabitacion SET Activo = 0 WHERE ID_Tipo = @ID_Tipo";
                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@ID_Tipo", idTipo);

                retorno = comando.ExecuteNonQuery();
            }

            // Después de dar de baja el tipo, también damos de baja sus habitaciones
            DarDeBajaHabitacionesPorTipo(idTipo);

            return retorno;
        }

        public static void InsertarAmenidadesPorTipoHabitacion(int idTipo, List<int> amenidadesSeleccionadas)
        {
            using (SqlConnection conexion = BDConexion.ObtenerConexion())
            {
                foreach (int idAmenidad in amenidadesSeleccionadas)
                {
                    string query = @"INSERT INTO AmenidadPorTipoHabitacion (ID_Tipo, ID_Amenidad)
                             VALUES (@ID_Tipo, @ID_Amenidad);";

                    SqlCommand comando = new SqlCommand(query, conexion);
                    comando.Parameters.AddWithValue("@ID_Tipo", idTipo);
                    comando.Parameters.AddWithValue("@ID_Amenidad", idAmenidad);
                    comando.ExecuteNonQuery();
                }
            }
        }

        public static void BorrarAmenidadesPorTipo(int idTipo)
        {
            using (SqlConnection conexion = BDConexion.ObtenerConexion())
            {
                string query = "DELETE FROM AmenidadPorTipoHabitacion WHERE ID_Tipo = @ID_Tipo";
                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@ID_Tipo", idTipo);
                comando.ExecuteNonQuery();
            }
        }


    }
}
