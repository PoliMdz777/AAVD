using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pantallas_alto_volumen_de_datos.Entidades;
using System.Data.SqlClient;

namespace Pantallas_alto_volumen_de_datos.DAO
{
    public class ClientesDAO
    {
        public static int RegistrarCliente(Cliente cliente)
        {
            int resultado = 0;

            using (SqlConnection conexion = BDConexion.ObtenerConexion())
            {
                SqlCommand comando = new SqlCommand("sp_RegistrarCliente", conexion);
                comando.CommandType = System.Data.CommandType.StoredProcedure;

                comando.Parameters.AddWithValue("@Nombre", cliente.Nombre);
                comando.Parameters.AddWithValue("@Apellidos", cliente.Apellidos);
                comando.Parameters.AddWithValue("@Ciudad", cliente.Ciudad);
                comando.Parameters.AddWithValue("@Estado", cliente.Estado);
                comando.Parameters.AddWithValue("@Pais", cliente.Pais);
                comando.Parameters.AddWithValue("@RFC", cliente.RFC);
                comando.Parameters.AddWithValue("@Correo", cliente.Correo);
                comando.Parameters.AddWithValue("@Celular", cliente.Celular);
                comando.Parameters.AddWithValue("@FechaNacimiento", cliente.FechaNacimiento);
                comando.Parameters.AddWithValue("@EstadoCivil", cliente.EstadoCivil);
                comando.Parameters.AddWithValue("@Telefono", cliente.Telefono);

                resultado = comando.ExecuteNonQuery();
            }

            return resultado;
        }

        public static List<Cliente> ObtenerClientesActivos()
        {
            List<Cliente> lista = new List<Cliente>();

            using (SqlConnection conexion = BDConexion.ObtenerConexion())
            {
                string query = "SELECT * FROM Cliente WHERE Status = 1";
                SqlCommand comando = new SqlCommand(query, conexion);
                SqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    Cliente cliente = new Cliente
                    {
                        ID_Cliente = reader.GetInt32(reader.GetOrdinal("ID_Cliente")),
                        Nombre = reader["Nombre"].ToString(),
                        Apellidos = reader["Apellidos"].ToString(),
                        Ciudad = reader["Ciudad"].ToString(),
                        Estado = reader["Estado"].ToString(),
                        Pais = reader["Pais"].ToString(),
                        RFC = reader["RFC"].ToString(),
                        Correo = reader["Correo"].ToString(),
                        Celular = reader["Celular"].ToString(),
                        FechaNacimiento = Convert.ToDateTime(reader["FechaNacimiento"]),
                        EstadoCivil = reader["EstadoCivil"].ToString(),
                        Telefono = reader["Telefono"].ToString()
                    };

                    lista.Add(cliente);
                }
            }

            return lista;
        }

        public static int EliminarCliente(int idCliente)
        {
            int resultado = 0;

            using (SqlConnection conexion = BDConexion.ObtenerConexion())
            {
                string query = "UPDATE Cliente SET Status = 0 WHERE ID_Cliente = @ID_Cliente";

                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@ID_Cliente", idCliente);

                resultado = comando.ExecuteNonQuery();
            }

            return resultado;
        }


        public static int ModificarCliente(Cliente cliente)
        {
            int resultado = 0;

            using (SqlConnection conexion = BDConexion.ObtenerConexion())
            {
                string query = @"UPDATE Cliente SET 
              Nombre = @Nombre,
              Apellidos = @Apellidos,
              Ciudad = @Ciudad,
              Estado = @Estado,
              Pais = @Pais,
              RFC = @RFC,
              Correo = @Correo,
              Celular = @Celular,
              FechaNacimiento = @FechaNacimiento,
              EstadoCivil = @EstadoCivil,
              Telefono = @Telefono
              WHERE ID_Cliente = @ID_Cliente";

                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@ID_Cliente", cliente.ID_Cliente);
                comando.Parameters.AddWithValue("@Nombre", cliente.Nombre);
                comando.Parameters.AddWithValue("@Apellidos", cliente.Apellidos);
                comando.Parameters.AddWithValue("@Ciudad", cliente.Ciudad);
                comando.Parameters.AddWithValue("@Estado", cliente.Estado);
                comando.Parameters.AddWithValue("@Pais", cliente.Pais);
                comando.Parameters.AddWithValue("@RFC", cliente.RFC);
                comando.Parameters.AddWithValue("@Correo", cliente.Correo);
                comando.Parameters.AddWithValue("@Celular", cliente.Celular);
                comando.Parameters.AddWithValue("@FechaNacimiento", cliente.FechaNacimiento);
                comando.Parameters.AddWithValue("@EstadoCivil", cliente.EstadoCivil);
                comando.Parameters.AddWithValue("@Telefono", cliente.Telefono);

                resultado = comando.ExecuteNonQuery();
            }

            return resultado;
        }

        public static bool ExisteClientePorRFC(string rfc)
        {
            using (SqlConnection conexion = BDConexion.ObtenerConexion())
            {
                string query = "SELECT COUNT(*) FROM Cliente WHERE RFC = @rfc";
                using (var cmd = new SqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@rfc", rfc);
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        public static int ObtenerIDClientePorRFC(string rfc)
        {
            using (SqlConnection conn = BDConexion.ObtenerConexion())
            {
                string query = "SELECT ID_Cliente FROM Cliente WHERE RFC = @RFC";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@RFC", rfc);

                object result = cmd.ExecuteScalar();
                return result != null ? Convert.ToInt32(result) : 0;
            }
        }

        public static bool ExisteRFC(string rfc)
        {
            using (SqlConnection conn = BDConexion.ObtenerConexion())
            {
                string query = "SELECT COUNT(*) FROM Cliente WHERE RFC = @RFC";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@RFC", rfc);
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }

        public static bool ExisteRFCParaOtroCliente(string rfc, int idClienteActual)
        {
            using (SqlConnection conn = BDConexion.ObtenerConexion())
            {
                string query = "SELECT COUNT(*) FROM Cliente WHERE RFC = @RFC AND ID_Cliente != @ID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@RFC", rfc);
                cmd.Parameters.AddWithValue("@ID", idClienteActual);
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }
    }
}
