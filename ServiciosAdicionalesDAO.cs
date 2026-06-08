using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pantallas_alto_volumen_de_datos.Entidades;
using System.Data.SqlClient;

namespace Pantallas_alto_volumen_de_datos.DAO
{
    public class ServiciosAdicionalesDAO
    {
        public static List<ServicioAdicionales> ObtenerTodosLosServicios()
        {
            List<ServicioAdicionales> lista = new List<ServicioAdicionales>();

            using (SqlConnection conn = BDConexion.ObtenerConexion())
            {
                string query = "SELECT ID_Servicio, Nombre, Precio FROM ServicioAdicional";
                SqlCommand cmd = new SqlCommand(query, conn);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ServicioAdicionales servicio = new ServicioAdicionales
                        {
                            ID_Servicio = Convert.ToInt32(reader["ID_Servicio"]),
                            Nombre = reader["Nombre"].ToString(),
                            Precio = Convert.ToDecimal(reader["Precio"])
                        };
                        lista.Add(servicio);
                    }
                }
            }

            return lista;
        }

        public static List<ServicioAdicionales> ObtenerServiciosPorHotel(int idHotel)
        {
            List<ServicioAdicionales> lista = new List<ServicioAdicionales>();

            using (SqlConnection conn = BDConexion.ObtenerConexion())
            {
                string query = @"
            SELECT SA.ID_Servicio, SA.Nombre, SA.Precio
            FROM ServicioAdicional SA
            INNER JOIN ServicioPorHotel SH ON SA.ID_Servicio = SH.ID_Servicio
            WHERE SH.ID_Hotel = @ID_Hotel";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ID_Hotel", idHotel);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ServicioAdicionales servicio = new ServicioAdicionales
                        {
                            ID_Servicio = Convert.ToInt32(reader["ID_Servicio"]),
                            Nombre = reader["Nombre"].ToString(),
                            Precio = Convert.ToDecimal(reader["Precio"])
                        };
                        lista.Add(servicio);
                    }
                }
            }

            return lista;
        }

    }





}
