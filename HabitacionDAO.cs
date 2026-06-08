using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Pantallas_alto_volumen_de_datos.Entidades;

namespace Pantallas_alto_volumen_de_datos.DAO
{
    public class HabitacionDAO
    {
        public static int InsertarHabitacion(Habitacion habitacion)
        {
            int retorno = 0;

            using (SqlConnection conexion = BDConexion.ObtenerConexion())
            {
                string query = "EXEC InsertarHabitacion @NumeroHabitacion, @ID_Tipo, @Status, @ID_Hotel";
                SqlCommand comando = new SqlCommand(query, conexion);

                comando.Parameters.AddWithValue("@NumeroHabitacion", habitacion.NumeroHabitacion);
                comando.Parameters.AddWithValue("@ID_Tipo", habitacion.ID_Tipo);
                comando.Parameters.AddWithValue("@Status", habitacion.Status);
                comando.Parameters.AddWithValue("@ID_Hotel", habitacion.ID_Hotel);

                retorno = comando.ExecuteNonQuery();
            }

            return retorno;
        }

        public static void EliminarHabitacionesPorTipo(int idTipo)
        {
            using (SqlConnection conexion = BDConexion.ObtenerConexion())
            {
                string query = "DELETE FROM Habitacion WHERE ID_Tipo = @ID_Tipo";
                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@ID_Tipo", idTipo);

                comando.ExecuteNonQuery();
            }
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

        public static List<Habitacion> ObtenerHabitacionesPorTipo(int idTipo)
        {
            List<Habitacion> lista = new List<Habitacion>();

            using (SqlConnection conexion = BDConexion.ObtenerConexion())
            {
                string query = "SELECT * FROM Habitacion WHERE ID_Tipo = @ID_Tipo ";
                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@ID_Tipo", idTipo);

                SqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    Habitacion habitacion = new Habitacion
                    {
                        ID_Hab = reader.GetInt32(0),
                        NumeroHabitacion = reader.GetString(1),
                        ID_Tipo = reader.GetInt32(2),
                        Status = reader.GetString(3),
                        // Agrega más campos si los tienes
                    };

                    lista.Add(habitacion);
                }
            }

            return lista;
        }

        public static int DarDeBajaHabitacionPorNumeroYTipo(string numeroHabitacion, int idTipo)
        {
            int filasAfectadas = 0;

            using (SqlConnection conexion = BDConexion.ObtenerConexion())
            {
                string query = "UPDATE Habitacion SET Status = 'Inactivo' WHERE NumeroHabitacion = @NumeroHabitacion AND ID_Tipo = @ID_Tipo";
                SqlCommand comando = new SqlCommand(query, conexion);

                comando.Parameters.AddWithValue("@NumeroHabitacion", numeroHabitacion);
                comando.Parameters.AddWithValue("@ID_Tipo", idTipo);

                filasAfectadas = comando.ExecuteNonQuery();
            }

            return filasAfectadas;
        }

        public static List<Habitacion> ObtenerHabitacionesDisponibles(int idHotel, int idTipo, DateTime fechaEntrada, DateTime fechaSalida)
        {
            List<Habitacion> disponibles = new List<Habitacion>();

            using (SqlConnection conexion = BDConexion.ObtenerConexion())
            {
                SqlCommand comando = new SqlCommand("SP_ObtenerHabitacionesDisponibles", conexion);
                comando.CommandType = System.Data.CommandType.StoredProcedure;

                comando.Parameters.AddWithValue("@ID_Tipo", idTipo);
                comando.Parameters.AddWithValue("@ID_Hotel", idHotel);
                comando.Parameters.AddWithValue("@FechaEntrada", fechaEntrada);
                comando.Parameters.AddWithValue("@FechaSalida", fechaSalida);

                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    disponibles.Add(new Habitacion
                    {
                        ID_Hab = reader.GetInt32(0),
                        NumeroHabitacion = reader.GetString(1),
                        ID_Tipo = reader.GetInt32(2),
                        Status = reader.GetString(3),
                        ID_Hotel = reader.GetInt32(4)
                    });
                }
            }

            return disponibles;
        }

        public static void ActualizarStatusHabitacion(int idHab, string nuevoStatus)
        {
            using (SqlConnection conexion = BDConexion.ObtenerConexion())
            {
                string query = "UPDATE Habitacion SET Status = @Status WHERE ID_Hab = @ID_Hab";
                SqlCommand cmd = new SqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@Status", nuevoStatus);
                cmd.Parameters.AddWithValue("@ID_Hab", idHab);
                cmd.ExecuteNonQuery();
            }
        }

        public static void CambiarEstadoHabitacion(int idHab, string nuevoEstado)
        {
            using (SqlConnection conn = BDConexion.ObtenerConexion())
            {
                string query = "UPDATE Habitacion SET Status = @Estado WHERE ID_Hab = @ID_Hab";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Estado", nuevoEstado);
                cmd.Parameters.AddWithValue("@ID_Hab", idHab);
                cmd.ExecuteNonQuery();
            }
        }


    }
}
