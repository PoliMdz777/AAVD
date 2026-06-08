using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Pantallas_alto_volumen_de_datos.DAO;
using Pantallas_alto_volumen_de_datos.Entidades;
using System.Data.SqlClient;
using Pantallas_alto_volumen_de_datos.User;
using CassandraEnlaceServer;

namespace Pantallas_alto_volumen_de_datos
{
    public partial class Login : Form
    {
        EnlaceCassandra enlace = new EnlaceCassandra();
        public Login()
        {
            InitializeComponent();
        }

        private void btn_login(object sender, EventArgs e) //evento al darle click al boton
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            TextBox UserInput = txtCorreo;
            TextBox PasswdInput = txtContrasena;

            if (string.IsNullOrEmpty(UserInput.Text) || string.IsNullOrEmpty(PasswdInput.Text))
            {
                MessageBox.Show("Por favor, ingrese su usuario y contraseña", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            EmpleadosCassandra usuario = new EmpleadosCassandra();
            DateTime fechaOp = OperationDate.Value;

            if (UserInput.Text.All(char.IsDigit))
            {
                int id = Convert.ToInt32(UserInput.Text);
                usuario = enlace.UserLogIn(null, PasswdInput.Text, id);
            }
            else
            {
                usuario = enlace.UserLogIn(UserInput.Text, PasswdInput.Text);
            }
            if (usuario != null)
            {
                //MessageBox.Show("Usuario: " + usuario.name + "\n" +"Email: " + usuario.email + "\nadmin:" + usuario.admin + "\nFecha:" + fecha);
                this.Hide();
                Principal mainMenu = new Principal(usuario, fechaOp);
                mainMenu.ShowDialog();
                this.Close();
            }
            else
            {
                UserInput.Text = "";
                PasswdInput.Text = "";
            }

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
        private bool ValidarUsuario(string correo, string password)
        {
            using (SqlConnection conexion = BDConexion.ObtenerConexion())
            {
                string query = "SELECT ID_Usuario, Nombre, NumeroNomina, Correo, TipoUsuario FROM Usuario WHERE Correo = @Correo AND Password = @Password";
                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@Correo", correo);
                    cmd.Parameters.AddWithValue("@Password", password);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        // Guardar datos del usuario en sesión global
                        SesionUsuario.ID_Usuario = reader.GetInt32(0);
                        SesionUsuario.Nombre = reader.GetString(1);
                        SesionUsuario.NumeroNomina = reader.GetString(2);
                        SesionUsuario.Correo = reader.GetString(3);
                        SesionUsuario.TipoUsuario = reader.GetString(4);

                        return true;
                    }

                    return false;
                }
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
           
        }
    }
}
