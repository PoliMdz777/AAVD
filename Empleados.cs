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
    public partial class Empleados : Form
    {
        EnlaceCassandra enlace = new EnlaceCassandra();
        private EmpleadosCassandra SesionUsuario;
        private DateTime DateTimeOp;

        private int usuarioSeleccionadoID = -1;
        public Empleados(EmpleadosCassandra usuerConnected, DateTime DateTimeOp)
        {
            InitializeComponent();
            MostrarEmpleados();
            MostrarUsuariosInactivos();
            this.SesionUsuario = usuerConnected;
            this.DateTimeOp = DateTimeOp;
        }

        private void btn_RegEmp_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos())
                return;

            string password = PaswordEmptxt.Text;

            if (!UsuarioDAO.EsPasswordValida(password))
            {
                MessageBox.Show("La contraseña debe tener al menos 8 caracteres, una mayúscula, una minúscula y un carácter especial.", "Contraseña inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

           

            if (string.IsNullOrWhiteSpace(NomEmptxt.Text) || NomEmptxt.Text.Any(char.IsDigit))
            {
                MessageBox.Show("El nombre no puede estar vacío ni contener números.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(CorreoEmptxt.Text) || !CorreoEmptxt.Text.Contains("@"))
            {
                MessageBox.Show("El correo no puede estar vacío y debe contener '@'.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(TelEmptxt.Text) || !TelEmptxt.Text.All(char.IsDigit) || TelEmptxt.Text.Length != 10)
            {
                MessageBox.Show("El teléfono debe contener exactamente 10 números.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DateTime fechaNacimiento = fechaNacimientoDtPk.Value;
            int edad = DateTime.Today.Year - fechaNacimiento.Year;

            // Ajustar si aún no ha cumplido años este año
            if (fechaNacimiento.Date > DateTime.Today.AddYears(-edad))
            {
                edad--;
            }

            if (edad < 18)
            {
                MessageBox.Show("El empleado debe ser mayor de edad (18+ años).",
                               "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            EmpleadosCassandra empleado = new EmpleadosCassandra
            {
                email = CorreoEmptxt.Text.Trim(),
                password = PaswordEmptxt.Text,
                name = NomEmptxt.Text.Trim(),
                admin = tipoUsuarioCmb.SelectedItem?.ToString().ToLower() == "administrador",
                phone = TelEmptxt.Text.Trim(),
                cellphone = TelEmptxt.Text.Trim(),
                address = txtDireccion.Text, 
                birthdate = new Cassandra.LocalDate(fechaNacimientoDtPk.Value.Year, fechaNacimientoDtPk.Value.Month, fechaNacimientoDtPk.Value.Day),
                status = true,
                //entrydate = DateTimeOp,
                entrydate = new Cassandra.LocalDate(fechaNacimientoDtPk.Value.Year, fechaNacimientoDtPk.Value.Month, fechaNacimientoDtPk.Value.Day),
                created_by = SesionUsuario.email
            };

           

            if (enlace.InsertUser(empleado))
            {
                MessageBox.Show("Empleado agregado con éxito", "Registro completo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MostrarEmpleados();
                LimpiarCampos();
            }
            else
            {
                MessageBox.Show("Hubo un error al agregar el empleado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //private void MostrarEmpleados()
        //{
        //    List<UserDetails> users = enlace.GetAllUsers();

        //    dgvEmpleado.Rows.Clear(); // Limpiar filas existentes
        //    dgvEmpleado.Columns.Clear(); // Limpiar columnas

        //    // Agregar columnas incluyendo el ID
        //    dgvEmpleado.Columns.Add("id", "ID");
        //    dgvEmpleado.Columns.Add("Status", "Estatus");
        //    dgvEmpleado.Columns.Add("admin", "Tipo de usuario");
        //    dgvEmpleado.Columns.Add("name", "Nombre");
        //    dgvEmpleado.Columns.Add("password", "Password");
        //    dgvEmpleado.Columns.Add("email", "Email");
        //    dgvEmpleado.Columns.Add("phone", "Teléfono");
        //    dgvEmpleado.Columns.Add("cellPhone", "Celular");
        //    dgvEmpleado.Columns.Add("address", "Dirección");
        //    dgvEmpleado.Columns.Add("birthdate", "Fecha de nacimiento");
        //    dgvEmpleado.Columns.Add("entryDate", "Fecha de entrada");

        //    foreach (UserDetails user in users)
        //    {
        //        dgvEmpleado.Rows.Add(
        //            user.id,
        //            user.status ? "Activo" : "Inactivo",
        //            user.admin ? "Administrador" : "Operador",
        //            user.name,
        //            user.password,
        //            user.email,
        //            user.phone,
        //            user.cellPhone,
        //            user.address,
        //            user.birthdate,
        //            user.entryDate
        //        );
        //    }
        //}
        private void MostrarEmpleados()
        {
            List<EmpleadosCassandra> users = enlace.GetAllUsers();

            dgvEmpleado.Rows.Clear();
            dgvEmpleado.Columns.Clear();

            // Definir columnas
            dgvEmpleado.Columns.Add("id", "ID");
            dgvEmpleado.Columns.Add("Status", "Estatus");
            dgvEmpleado.Columns.Add("admin", "Tipo de usuario");
            dgvEmpleado.Columns.Add("name", "Nombre");
            dgvEmpleado.Columns.Add("password", "Password");
            dgvEmpleado.Columns.Add("email", "Email");
            dgvEmpleado.Columns.Add("phone", "Teléfono");
            dgvEmpleado.Columns.Add("cellPhone", "Celular");
            dgvEmpleado.Columns.Add("address", "Dirección");
            dgvEmpleado.Columns.Add("birthdate", "Fecha de nacimiento");
            dgvEmpleado.Columns.Add("entryDate", "Fecha de entrada");

            foreach (EmpleadosCassandra user in users)
            {
                if (user.status) // 👈 solo empleados ACTIVOS
                {
                    dgvEmpleado.Rows.Add(
                        user.id,
                        "Activo",
                        user.admin ? "Administrador" : "Operativo",
                        user.name,
                        user.password,
                        user.email,
                        user.phone,
                        user.cellphone,
                        user.address,
                        user.birthdate,
                        user.entrydate
                    );
                }
            }
        }

        private void MostrarUsuariosInactivos()
        {
            List<EmpleadosCassandra> users = enlace.GetAllUsers();

            dgvEmpInactivos.Rows.Clear();
            dgvEmpInactivos.Columns.Clear();

            // Definir columnas
            dgvEmpInactivos.Columns.Add("id", "ID");
            dgvEmpInactivos.Columns.Add("Status", "Estatus");
            dgvEmpInactivos.Columns.Add("admin", "Tipo de usuario");
            dgvEmpInactivos.Columns.Add("name", "Nombre");
            dgvEmpInactivos.Columns.Add("password", "Password");
            dgvEmpInactivos.Columns.Add("email", "Email");
            dgvEmpInactivos.Columns.Add("phone", "Teléfono");
            dgvEmpInactivos.Columns.Add("cellPhone", "Celular");
            dgvEmpInactivos.Columns.Add("address", "Dirección");
            dgvEmpInactivos.Columns.Add("birthdate", "Fecha de nacimiento");
            dgvEmpInactivos.Columns.Add("entryDate", "Fecha de entrada");

            foreach (EmpleadosCassandra user in users)
            {
                if (!user.status) // solo mostrar usuarios con status = false
                {
                    dgvEmpInactivos.Rows.Add(
                        user.id,
                        "Inactivo",
                        user.admin ? "Administrador" : "Operativo",
                        user.name,
                        user.password,
                        user.email,
                        user.phone,
                        user.cellphone,
                        user.address,
                        user.birthdate,
                        user.entrydate
                    );
                }
            }
        }




        //private bool EsPasswordValida(string password)
        //{
        //    if (password.Length < 8)
        //        return false;

        //    bool tieneMayuscula = password.Any(char.IsUpper);
        //    bool tieneMinuscula = password.Any(char.IsLower);
        //    bool tieneEspecial = password.Any(ch => !char.IsLetterOrDigit(ch));

        //    return tieneMayuscula && tieneMinuscula && tieneEspecial;
        //}



        //private void MostrarUsuariosInactivos()
        //{
        //    dgvEmpInactivos.DataSource = UsuarioDAO.ObtenerUsuariosInactivos();
        //    dgvEmpInactivos.Columns["ID_Usuario"].Visible = false;
        //    dgvEmpInactivos.Columns["Activo"].Visible = false;
        //    dgvEmpInactivos.Columns["ID_Usuario"].Visible = false;
        //    dgvEmpInactivos.Columns["UserReg"].Visible = false;

        //}

        private void LimpiarCampos()
        {
            CorreoEmptxt.Clear();
            PaswordEmptxt.Clear();
            NomEmptxt.Clear();
            TelEmptxt.Clear();
           txtDireccion.Clear();
            tipoUsuarioCmb.SelectedIndex = -1; // Limpia la selección del ComboBox
            fechaNacimientoDtPk.Value = DateTime.Now; // Restaura la fecha actual
          
            TelCasatxt.Clear();

        }

        //private void dgvEmpleado_CellClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (e.RowIndex < 0) return; // Si se hace clic en el encabezado

        //    DataGridViewRow fila = dgvEmpleado.Rows[e.RowIndex];

        //    try
        //    {
        //        CorreoEmptxt.Text = fila.Cells["Email"].Value?.ToString() ?? "";
        //        PaswordEmptxt.Text = fila.Cells["Password"].Value?.ToString() ?? "";
        //        NomEmptxt.Text = fila.Cells["Name"].Value?.ToString() ?? "";
        //        TelEmptxt.Text = fila.Cells["Phone"].Value?.ToString() ?? "";
        //        TelCasatxt.Text = fila.Cells["Cellphone"].Value?.ToString() ?? "";
        //        txtDireccion.Text = fila.Cells["Address"].Value?.ToString() ?? "";

        //        //Leer el valor booleano real desde la columna oculta
        //        bool esAdmin = Convert.ToBoolean(fila.Cells["EsAdmin"].Value);
        //        tipoUsuarioCmb.SelectedItem = esAdmin ? "Administrador" : "Operativo";

        //        // Convertir LocalDate (Cassandra) a DateTime
        //        if (fila.Cells["BirthDate"].Value is Cassandra.LocalDate fechaNac)
        //        {
        //            fechaNacimientoDtPk.Value = new DateTime(fechaNac.Year, fechaNac.Month, fechaNac.Day);
        //        }

        //        // Solo si estás usando ID para editar
        //        // usuarioSeleccionadoID = Convert.ToInt32(fila.Cells["id"].Value); 
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error al cargar los datos del empleado: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        private void dgvEmpleado_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow fila = dgvEmpleado.Rows[e.RowIndex];

            try
            {
                // Obtener ID
                usuarioSeleccionadoID = Convert.ToInt32(fila.Cells["id"].Value);

                // Asignar valores a los campos
                CorreoEmptxt.Text = fila.Cells["email"].Value?.ToString() ?? "";
                PaswordEmptxt.Text = fila.Cells["password"].Value?.ToString() ?? "";
                NomEmptxt.Text = fila.Cells["name"].Value?.ToString() ?? "";
                TelEmptxt.Text = fila.Cells["phone"].Value?.ToString() ?? "";
                TelCasatxt.Text = fila.Cells["cellPhone"].Value?.ToString() ?? "";
                txtDireccion.Text = fila.Cells["Address"].Value?.ToString() ?? "";

                bool esAdmin = fila.Cells["admin"].Value?.ToString() == "Administrador";
                tipoUsuarioCmb.SelectedItem = esAdmin ? "Administrador" : "Operativo";

                if (fila.Cells["birthdate"].Value is Cassandra.LocalDate fechaNac)
                {
                    fechaNacimientoDtPk.Value = new DateTime(fechaNac.Year, fechaNac.Month, fechaNac.Day);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos del empleado: " + ex.Message);
            }
        }





        private void BtnModEmp_Click(object sender, EventArgs e)
        {
            BtnGuardarEmp.Visible = true;
        }




        private void BtnGuardarEmp_Click(object sender, EventArgs e)
        {
            if (usuarioSeleccionadoID == -1)
            {
                MessageBox.Show("Selecciona un usuario para actualizar.");
                return;
            }

            string nuevaPassword = PaswordEmptxt.Text.Trim();

            // Validaciones
            string nombre = NomEmptxt.Text.Trim();
            if (string.IsNullOrWhiteSpace(nombre) || nombre.Any(char.IsDigit))
            {
                MessageBox.Show("El nombre no puede estar vacío ni contener números.");
                return;
            }



            string correo = CorreoEmptxt.Text.Trim();
            if (string.IsNullOrWhiteSpace(correo) || !correo.Contains("@"))
            {
                MessageBox.Show("Correo inválido.");
                return;
            }

            string telefono = TelEmptxt.Text.Trim();
            string telCasa = TelCasatxt.Text.Trim();

            if (string.IsNullOrWhiteSpace(telefono) || !telefono.All(char.IsDigit) || telefono.Length < 8 ||
                string.IsNullOrWhiteSpace(telCasa) || !telCasa.All(char.IsDigit) || telCasa.Length < 8)
            {
                MessageBox.Show("Teléfonos inválidos.");
                return;
            }

            if (tipoUsuarioCmb.SelectedIndex == -1)
            {
                MessageBox.Show("Debes seleccionar un tipo de usuario.");
                return;
            }

            // Crear objeto empleado
            EmpleadosCassandra usuario = new EmpleadosCassandra
            {
                id = usuarioSeleccionadoID,
                email = correo,
                password = nuevaPassword,
                name = nombre,
                phone = telCasa,
                cellphone = telefono,
                admin = tipoUsuarioCmb.SelectedItem.ToString().ToLower() == "administrador",
                address = txtDireccion.Text.Trim(),
                birthdate = new Cassandra.LocalDate(
                    fechaNacimientoDtPk.Value.Year,
                    fechaNacimientoDtPk.Value.Month,
                    fechaNacimientoDtPk.Value.Day),
                //entrydate = DateTimeOp /*new Cassandra.LocalDate(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day)*/,
                entrydate = new Cassandra.LocalDate(
                    fechaNacimientoDtPk.Value.Year,
                    fechaNacimientoDtPk.Value.Month,
                    fechaNacimientoDtPk.Value.Day) /*new Cassandra.LocalDate(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day)*/,
                status = true,
                created_by = SesionUsuario.email
            };

            // Llamar método de actualización
            bool actualizado = enlace.ActualizarUsuario(usuario);

            if (actualizado)
            {
                MessageBox.Show("Usuario actualizado correctamente.");
                MostrarEmpleados();
                LimpiarCampos();
                usuarioSeleccionadoID = -1;
                BtnGuardarEmp.Visible = false;
            }
            else
            {
                MessageBox.Show("Ocurrió un error al actualizar.");
            }
        }



        private void btnDelEmp_Click(object sender, EventArgs e)
        {
            if (usuarioSeleccionadoID == -1)
            {
                MessageBox.Show("Selecciona un usuario primero.");
                return;
            }

            DialogResult confirmacion = MessageBox.Show("¿Estás seguro de dar de baja a este usuario?", "Confirmar baja", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmacion == DialogResult.Yes)
            {
                int resultado = EnlaceCassandra.DarDeBajaUsuario(usuarioSeleccionadoID); 

                if (resultado > 0)
                {
                    MessageBox.Show("Usuario dado de baja.");
                    MostrarEmpleados(); // refrescar
                    MostrarUsuariosInactivos();
                    LimpiarCampos();
                    usuarioSeleccionadoID = -1;
                }
                else
                {
                    MessageBox.Show("No se pudo dar de baja al usuario.");
                }
            }
        }



        private void btnHabilitar_Click(object sender, EventArgs e)
        {
            if (usuarioSeleccionadoID == -1)
            {
                MessageBox.Show("Selecciona un usuario inactivo primero.");
                return;
            }

            DialogResult confirmacion = MessageBox.Show("¿Deseas habilitar a este usuario?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmacion == DialogResult.Yes)
            {
                bool resultado = enlace.HabilitarUsuario(usuarioSeleccionadoID);

                if (resultado)
                {
                    MessageBox.Show("Usuario habilitado correctamente.");
                    MostrarEmpleados();           // Refresca empleados activos
                    MostrarUsuariosInactivos();
                    LimpiarCampos();
                    usuarioSeleccionadoID = -1;
                    btnHabilitar.Visible = false;
                }
                else
                {
                    MessageBox.Show("No se pudo habilitar al usuario.");
                }
            }
        }

        private void dgvEmpInactivos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvEmpInactivos.Rows[e.RowIndex];

                // Guarda el ID seleccionado
                usuarioSeleccionadoID = Convert.ToInt32(fila.Cells["id"].Value);

                // Muestra el botón de habilitar
                btnHabilitar.Visible = true;
            }
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(CorreoEmptxt.Text) ||
                string.IsNullOrWhiteSpace(PaswordEmptxt.Text) ||
                string.IsNullOrWhiteSpace(NomEmptxt.Text) ||
                string.IsNullOrWhiteSpace(TelEmptxt.Text) ||
                tipoUsuarioCmb.SelectedItem == null)
            {
                MessageBox.Show("Por favor, completa todos los campos obligatorios.", "Campos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Validar que el campo NSS sea un número válido
           

            return true;
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }
    }
}
