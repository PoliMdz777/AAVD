using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pantallas_alto_volumen_de_datos.Entidades; // le tienes que agregar esas dos librerias sino no jala jajaja
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Text.RegularExpressions;


namespace Pantallas_alto_volumen_de_datos.DAO
{
    public class UsuarioDAO
    {
        public static int InsertarUsuario(Usuario usuario)
        {
            int retorno = 0;

            using (SqlConnection conexion = BDConexion.ObtenerConexion())
            {
                string query = "EXEC InsertarUsuario2 @Correo, @Password, @Nombre, @TipoUsuario, @Telefonos, @FechaNacimiento, @NumSeguro, @RFC, @UserReg, @FechaHora, @TelCasa";
                SqlCommand comando = new SqlCommand(query, conexion);

                comando.Parameters.AddWithValue("@Correo", usuario.Correo);
                comando.Parameters.AddWithValue("@Password", usuario.Password);
                comando.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                comando.Parameters.AddWithValue("@TipoUsuario", usuario.TipoUsuario);
                comando.Parameters.AddWithValue("@Telefonos", usuario.Telefonos);
                comando.Parameters.AddWithValue("@FechaNacimiento", usuario.FechaNacimiento);
                comando.Parameters.AddWithValue("@NumSeguro", usuario.NumSeguro);
                comando.Parameters.AddWithValue("@RFC", usuario.RFC);
                comando.Parameters.AddWithValue("@UserReg", usuario.UserReg);
                comando.Parameters.AddWithValue("@FechaHora", usuario.FechaHora);
                comando.Parameters.AddWithValue("@TelCasa", usuario.TelCasa);

                retorno = comando.ExecuteNonQuery();
            }

            return retorno;
        }


        public static List<Usuario> ObtenerTodosLosUsuarios()
        {
            List<Usuario> lista = new List<Usuario>();

            using (SqlConnection conexion = BDConexion.ObtenerConexion())
            {
                string query = "SELECT * FROM Usuario WHERE Activo = 1;";
                //string query = "SELECT * FROM Usuario;";
                SqlCommand comando = new SqlCommand(query, conexion);
                SqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    Usuario usuario = new Usuario();
                    usuario.ID_Usuario = reader.GetInt32(0);
                    usuario.Correo = reader.GetString(1);
                    usuario.Password = reader.GetString(2);
                    usuario.Nombre = reader.GetString(3);
                    usuario.NumeroNomina = reader.GetString(4);
                    usuario.TipoUsuario = reader.GetString(5);
                    usuario.Telefonos = reader.GetString(6);
                    usuario.FechaNacimiento = reader.GetDateTime(7);
                    usuario.NumSeguro = reader.GetInt32(8);
                    usuario.RFC = reader.GetString(9);
                    usuario.UserReg = reader.GetString(10);
                    usuario.FechaHora = reader.GetDateTime(11);
                    usuario.TelCasa = reader.GetString(13);

                    lista.Add(usuario);
                }
            }

            return lista;
        }

        public static int ActualizarUsuario(Usuario usuario)
        {
            int retorno = 0;

            using (SqlConnection conexion = BDConexion.ObtenerConexion())
            {
                string query = @"UPDATE Usuario SET 
                         Correo = @Correo,
                         Password = @Password,
                         Nombre = @Nombre,
                         TipoUsuario = @TipoUsuario,
                         Telefonos = @Telefonos,
                         FechaNacimiento = @FechaNacimiento,
                         NumSeguro = @NumSeguro,
                         RFC = @RFC,
                         TelCasa = @TelCasa

                         WHERE ID_Usuario = @ID_Usuario";

                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@Correo", usuario.Correo);
                comando.Parameters.AddWithValue("@Password", usuario.Password);
                comando.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                comando.Parameters.AddWithValue("@TipoUsuario", usuario.TipoUsuario);
                comando.Parameters.AddWithValue("@Telefonos", usuario.Telefonos);
                comando.Parameters.AddWithValue("@FechaNacimiento", usuario.FechaNacimiento);
                comando.Parameters.AddWithValue("@NumSeguro", usuario.NumSeguro);
                comando.Parameters.AddWithValue("@RFC", usuario.RFC);
                comando.Parameters.AddWithValue("@ID_Usuario", usuario.ID_Usuario);
                comando.Parameters.AddWithValue("@TelCasa", usuario.TelCasa);

                retorno = comando.ExecuteNonQuery();
            }

            return retorno;
        }

        public static int DarDeBajaUsuario(int idUsuario)
        {
            int retorno = 0;

            using (SqlConnection conexion = BDConexion.ObtenerConexion())
            {
                string query = "UPDATE Usuario SET Activo = 0 WHERE ID_Usuario = @ID";
                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@ID", idUsuario);

                retorno = comando.ExecuteNonQuery();
            }

            return retorno;
        }

        public static int ReactivarUsuario(int idUsuario)
        {
            int retorno = 0;

            using (SqlConnection conexion = BDConexion.ObtenerConexion())
            {
                string query = "UPDATE Usuario SET Activo = 1 WHERE ID_Usuario = @ID";
                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@ID", idUsuario);

                retorno = comando.ExecuteNonQuery();
            }

            return retorno;
        }

        //public static List<Usuario> ObtenerUsuariosInactivos()
        //{
        //    List<Usuario> lista = new List<Usuario>();

        //    using (SqlConnection conexion = BDConexion.ObtenerConexion())
        //    {
        //        string query = "SELECT * FROM Usuario WHERE Activo = 0;";
        //        SqlCommand comando = new SqlCommand(query, conexion);
        //        SqlDataReader reader = comando.ExecuteReader();

        //        while (reader.Read())
        //        {
        //            Usuario usuario = new Usuario();
        //            usuario.ID_Usuario = reader.GetInt32(0);
        //            usuario.Correo = reader.GetString(1);
        //            usuario.Password = reader.GetString(2);
        //            usuario.Nombre = reader.GetString(3);
        //            usuario.NumeroNomina = reader.GetString(4);
        //            usuario.TipoUsuario = reader.GetString(5);
        //            usuario.Telefonos = reader.GetString(6);
        //            usuario.FechaNacimiento = reader.GetDateTime(7);
        //            usuario.NumSeguro = reader.GetInt32(8);
        //            usuario.RFC = reader.GetString(9);
        //            usuario.UserReg = reader.GetString(10);
        //            usuario.FechaHora = reader.GetDateTime(11);
        //            // Activo no es necesario si ya sabes que son todos 0

        //            lista.Add(usuario);
        //        }
        //    }

        //    return lista;
        //}

        public static int ActualizarPassword(int idUsuario, string nuevaPassword)
        {
            if (!EsPasswordValida(nuevaPassword))
            {
                MessageBox.Show("La contraseña debe tener al menos 8 caracteres, una mayúscula, una minúscula y un carácter especial.", "Contraseña inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return 0;
            }

            using (SqlConnection conexion = BDConexion.ObtenerConexion())
            {
                // Obtener contraseñas anteriores
                string querySelect = "SELECT Password, UltimaPassword, PenultimaPassword FROM Usuario WHERE ID_Usuario = @ID";
                SqlCommand cmdSelect = new SqlCommand(querySelect, conexion);
                cmdSelect.Parameters.AddWithValue("@ID", idUsuario);

                SqlDataReader reader = cmdSelect.ExecuteReader();
                if (!reader.Read()) return 0;

                string actual = reader["Password"].ToString();
                string ultima = reader["UltimaPassword"]?.ToString();
                string penultima = reader["PenultimaPassword"]?.ToString();
                reader.Close();

                // Validar que la nueva no sea igual a las anteriores
                if (nuevaPassword == actual || nuevaPassword == ultima || nuevaPassword == penultima)
                {
                    MessageBox.Show("La nueva contraseña no puede ser igual a las últimas dos utilizadas.", "Contraseña repetida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return 0;
                }

                // Actualizar contraseñas
                string queryUpdate = @"
                  UPDATE Usuario
                  SET 
                   PenultimaPassword = UltimaPassword,
                   UltimaPassword = Password,
                   Password = @NuevaPassword
                  WHERE ID_Usuario = @ID";

                SqlCommand cmdUpdate = new SqlCommand(queryUpdate, conexion);
                cmdUpdate.Parameters.AddWithValue("@NuevaPassword", nuevaPassword);
                cmdUpdate.Parameters.AddWithValue("@ID", idUsuario);

                return cmdUpdate.ExecuteNonQuery();
            }
        }

        public static bool EsPasswordValida(string password)
        {
            if (string.IsNullOrEmpty(password) || password.Length < 8)
                return false;

            bool tieneMayuscula = password.Any(char.IsUpper);
            bool tieneMinuscula = password.Any(char.IsLower);
            bool tieneEspecial = password.Any(ch => !char.IsLetterOrDigit(ch));

            return tieneMayuscula && tieneMinuscula && tieneEspecial;
        }

        public static string ObtenerPasswordActual(int idUsuario)
        {
            string password = string.Empty;

            using (SqlConnection conn = BDConexion.ObtenerConexion())
            {
                string query = "SELECT Password FROM Usuario WHERE ID_Usuario = @ID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ID", idUsuario);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    password = reader["Password"].ToString();
                }
            }

            return password;
        }

        

       public static bool EsRFCValido(string rfc)
       {
        if (string.IsNullOrWhiteSpace(rfc))
            return false;

        // Patrón: Personas Morales (12) o Físicas (13)
        string patronRFC = @"^[A-ZÑ&]{3,4}\d{6}[A-Z0-9]{3}$";

        return Regex.IsMatch(rfc.ToUpper(), patronRFC);
       }


    }
}
