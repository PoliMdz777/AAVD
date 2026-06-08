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
using System.Text.RegularExpressions;
using CassandraEnlaceServer;





namespace Pantallas_alto_volumen_de_datos
{
    public partial class RegHotel : Form
    {
        EnlaceCassandra enlace = new EnlaceCassandra();
        EmpleadosCassandra usuarioConectado = new EmpleadosCassandra();
        DateTime DateTimeOp;

        public RegHotel(EmpleadosCassandra user, DateTime opDate)
        {
            InitializeComponent();
            LoadInfo();
            CargarServicios();


            //dgvHoteles.DataSource = enlace.GetAllHotels();
            //dgvHoteles.Columns["ID_Hotel"].Visible = false;
            //dgvHoteles.Columns["Usuario"].Visible = false;
            //dgvHoteles.Columns["FechaHoraRegistro"].Visible = false;
        }

        private bool ContieneNumeros(string texto)
        {
            return texto.Any(char.IsDigit);
        }


        private void CargarServicios()
        {
            List<additionalService> aditionalServices = enlace.GetAllAditionalService();

            // Enlaza al CheckedListBox (asegúrate de que se llama así)
            CLBServicios.DataSource = aditionalServices;
            CLBServicios.DisplayMember = "name";
            CLBServicios.ValueMember = "name";
        }
        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnRegHot_Click(object sender, EventArgs e)
        {
            // 1. Validar campos obligatorios
            if (string.IsNullOrWhiteSpace(NombreHoteltxt.Text) ||
                string.IsNullOrWhiteSpace(pisosnum.Text) ||
                string.IsNullOrWhiteSpace(VistaRtxt.Text) ||
                string.IsNullOrWhiteSpace(numPools.Text) ||
                string.IsNullOrWhiteSpace(Domiciliotxt.Text) ||
                string.IsNullOrWhiteSpace(Ciudadtxt.Text) ||
                string.IsNullOrWhiteSpace(Estadotxt.Text) ||
                string.IsNullOrWhiteSpace(Paistxt.Text) ||
                string.IsNullOrWhiteSpace(phoneTextBox.Text))
            {
                MessageBox.Show("Todos los campos deben estar llenos.", "Campos vacíos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Validar que ciudad, estado y país no contengan números
            if (ContieneNumeros(Ciudadtxt.Text) || ContieneNumeros(Estadotxt.Text) || ContieneNumeros(Paistxt.Text))
            {
                MessageBox.Show("Ciudad, Estado y País no deben contener números.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //// 3. Validar que el código SAT sea exactamente de 8 dígitos numéricos
            //if (!Regex.IsMatch(CodigoSATtxt.Text, @"^\d{8}$"))
            //{
            //    MessageBox.Show("El código SAT debe contener exactamente 8 números.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}

            // 4. Crear el objeto hotel si todo es válido
            Hotel hotel = new Hotel
            {
                //unitservice = UniServiciotxt.Text,
                //sat = CodigoSATtxt.Text,
                name = NombreHoteltxt.Text,
                city = Ciudadtxt.Text,
                state = Estadotxt.Text,
                country = Paistxt.Text,
                address = Domiciliotxt.Text,
                phone = phoneTextBox.Text,
                totalFloors = int.Parse(pisosnum.Text),
                turisticZone = ZTsi.Checked,
                views = VistaRtxt.Text,
                numPools = int.Parse(numPools.Text),
                eventsSalon = SEsi.Checked,
                dateOperation = new Cassandra.LocalDate(FechaOps.Value.Year, FechaOps.Value.Month, FechaOps.Value.Day),
                userId = SesionUsuario.ID_Usuario,
                dateRegistered = new Cassandra.LocalDate(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day),
                status = statusCombobox.Text == "Activo" ? true : false
            };

            // 5. Obtener los servicios seleccionados del CheckedListBox
            // Verificar que el tipo del CheckedListBox contiene los datos correctos
            List<string> serviciosSeleccionadosNombres = new List<string>();

            foreach (var item in CLBServicios.CheckedItems)
            {
                // Asegurarse de que el item sea del tipo 'additionalService'
                serviciosSeleccionadosNombres.Add(item.ToString());

            }

            // Asegúrate de que 'hotel.aditionalService' sea un List<string>
            hotel.aditionalService = serviciosSeleccionadosNombres;


            // 6. Insertar en BD
            bool resultado = enlace.InsertHotel(hotel);//, serviciosSeleccionados);

            if (resultado)
            {
                MessageBox.Show("¡Hotel registrado correctamente!");
                LimpiarCampos();
                LoadInfo(); // Recargar la lista de hoteles en el DataGridView
            }
            else
            {
                MessageBox.Show("Ocurrió un error al registrar el hotel.");
            }
        }

        //private void btnRegHot_Click(object sender, EventArgs e) //llamar aqui la funcion de registrar
        //{
        //    Hoteles hotel = new Hoteles
        //    {
        //        CodigoSAT = CodigoSATtxt.Text,
        //        Nombre = NombreHoteltxt.Text,
        //        UnidadServicio = UniServiciotxt.Text,
        //        NumPisos = int.Parse(pisosnum.Text), 
        //        Vista = VistaRtxt.Text,
        //        Piscina = int.Parse(numPools.Text),
        //        SalonEventos = SEsi.Checked,
        //        FechaInicioOps = FechaOps.Value,
        //        ZonaTuristica = ZTsi.Checked,
        //        Domicilio = Domiciliotxt.Text,
        //        Usuario = SesionUsuario.Nombre,
        //        FechaHoraRegistro = DateTime.Now,
        //        Ciudad = Ciudadtxt.Text,
        //        Estado = Estadotxt.Text,
        //        Pais = Paistxt.Text
        //    };

        //    // 2. Obtener los servicios seleccionados del CheckedListBox
        //    List<int> serviciosSeleccionados = new List<int>();
        //    foreach (var item in CLBServicios.CheckedItems)
        //    {
        //        ServicioAdicionales servicio = item as ServicioAdicionales;
        //        serviciosSeleccionados.Add(servicio.ID_Servicio);
        //    }

        //    // 3. Insertar el hotel y los servicios usando el DAO
        //    int resultado = HotelesDAO.InsertarHotel(hotel, serviciosSeleccionados);

        //    if (resultado > 0)
        //    {
        //        MessageBox.Show("¡Hotel registrado correctamente!");
        //        LimpiarCampos();

        //    }else
        //        MessageBox.Show("Ocurrió un error al registrar el hotel.");
        //}

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void dgvHoteles_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvHoteles.Rows[e.RowIndex];
                int idHotelSeleccionado = Convert.ToInt32(fila.Cells[0].Value);

                // Llenar los TextBox con los datos de la fila  
                NombreHoteltxt.Text = fila.Cells["Nombre"].Value.ToString();
                Ciudadtxt.Text = fila.Cells["Ciudad"].Value.ToString();
                Estadotxt.Text = fila.Cells["Estado"].Value.ToString();
                Paistxt.Text = fila.Cells["Pais"].Value.ToString();
                Domiciliotxt.Text = fila.Cells["Domicilio"].Value.ToString();
                pisosnum.Text = fila.Cells["NumPisos"].Value.ToString();

                // Para Zona Turística  
                if (fila.Cells["ZonaTuristica"].Value.ToString() == "Si")
                {
                    ZTsi.Checked = true;
                    ZTno.Checked = false;
                }
                else
                {
                    ZTsi.Checked = false;
                    ZTno.Checked = true;
                }

                VistaRtxt.Text = fila.Cells["Vista"].Value.ToString();
                numPools.Text = fila.Cells["Piscina"].Value.ToString();


                // Para Salón de Eventos  
                if (fila.Cells["SalonEventos"].Value.ToString() == "Si")
                {
                    SEsi.Checked = true;
                    SEno.Checked = false;
                }
                else
                {
                    SEsi.Checked = false;
                    SEno.Checked = true;
                }

                phoneTextBox.Text = fila.Cells["Telefono"].Value.ToString();
                // Para el estado del hotel
                if (fila.Cells["Status"].Value.ToString() == "Activo")
                {
                    statusCombobox.SelectedIndex = 0; // Activo
                }
                else
                {
                    statusCombobox.SelectedIndex = 1; // Inactivo
                }


                // Para fechas  
                var localDate = fila.Cells["FechaInicioOps"].Value as Cassandra.LocalDate;
                if (localDate != null)
                {
                    FechaOps.Value = new DateTime(localDate.Year, localDate.Month, localDate.Day);
                }
                //FechaOps.Value = Convert.ToDateTime(fila.Cells["FechaInicioOps"].Value);

                List<string> serviciosHotel = enlace.GetAllAditionalServiceByHotel(idHotelSeleccionado);
                // Primero desmarcamos todo  
                for (int i = 0; i < CLBServicios.Items.Count; i++)
                {
                    CLBServicios.SetItemChecked(i, false);
                }

                if (serviciosHotel == null || serviciosHotel.Count == 0)
                {
                    return;
                }
                // Ahora marcamos los que correspondan  
                foreach (var name in serviciosHotel)
                {
                    for (int i = 0; i < CLBServicios.Items.Count; i++)
                    {
                        var servicio = CLBServicios.Items[i] as additionalService;
                        if (servicio != null && servicio.name == name)
                        {
                            CLBServicios.SetItemChecked(i, true);
                        }
                    }
                }
            }
        }

        private void btnEliminarHotel_Click(object sender, EventArgs e)
        {
            if (dgvHoteles.SelectedRows.Count > 0)
            {
                int idHotel = Convert.ToInt32(dgvHoteles.CurrentRow.Cells[0].Value);
                // ID_Hotel

                DialogResult result = MessageBox.Show("¿Seguro que deseas dar de baja este hotel?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    int retorno = enlace.hotelDardeBaja(idHotel);

                    if (retorno > 0)
                    {
                        //dgvHoteles.DataSource = enlace.GetAllHotels(); // Recarga el DataGridView
                        LimpiarCampos();
                        LoadInfo(); // Recargar la lista de hoteles en el DataGridView
                    }
                    else
                    {
                        MessageBox.Show("Error al dar de baja el hotel.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    return;
                }

                DialogResult resultDelete = MessageBox.Show("¿Seguro que deseas ELIMINAR este hotel?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultDelete == DialogResult.Yes)
                {
                    int retorno = enlace.DeleteHotel(idHotel);

                    if (retorno > 0)
                    {
                        //dgvHoteles.DataSource = enlace.GetAllHotels(); // Recarga el DataGridView
                        LimpiarCampos();
                        LoadInfo(); // Recargar la lista de hoteles en el DataGridView
                    }
                    else
                    {
                        MessageBox.Show("Error al dar de baja el hotel.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }


            }
            else
            {
                MessageBox.Show("Selecciona un hotel para dar de baja.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnGuardarCambiosHotel_Click(object sender, EventArgs e)
        {

            if (dgvHoteles.SelectedRows.Count > 0)
            {
                if (string.IsNullOrWhiteSpace(NombreHoteltxt.Text) ||
                 string.IsNullOrWhiteSpace(pisosnum.Text) ||
                 string.IsNullOrWhiteSpace(VistaRtxt.Text) ||
                 string.IsNullOrWhiteSpace(numPools.Text) ||
                 string.IsNullOrWhiteSpace(Domiciliotxt.Text) ||
                 string.IsNullOrWhiteSpace(Ciudadtxt.Text) ||
                 string.IsNullOrWhiteSpace(Estadotxt.Text) ||
                 string.IsNullOrWhiteSpace(Paistxt.Text) ||
                 string.IsNullOrWhiteSpace(phoneTextBox.Text))
                {
                    MessageBox.Show("Todos los campos deben estar llenos.", "Campos vacíos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 2. Validar que ciudad, estado y país no contengan números
                if (ContieneNumeros(Ciudadtxt.Text) || ContieneNumeros(Estadotxt.Text) || ContieneNumeros(Paistxt.Text))
                {
                    MessageBox.Show("Ciudad, Estado y País no deben contener números.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                Hotel hotel = new Hotel
                {
                    //unitservice = UniServiciotxt.Text,
                    //sat = CodigoSATtxt.Text,
                    name = NombreHoteltxt.Text,
                    city = Ciudadtxt.Text,
                    state = Estadotxt.Text,
                    country = Paistxt.Text,
                    address = Domiciliotxt.Text,
                    phone = phoneTextBox.Text,
                    totalFloors = int.Parse(pisosnum.Text),
                    turisticZone = ZTsi.Checked,
                    views = VistaRtxt.Text,
                    numPools = int.Parse(numPools.Text),
                    eventsSalon = SEsi.Checked,
                    dateOperation = new Cassandra.LocalDate(FechaOps.Value.Year, FechaOps.Value.Month, FechaOps.Value.Day),
                    userId = usuarioConectado.id,
                    dateRegistered = new Cassandra.LocalDate(DateTimeOp.Year, DateTimeOp.Month, DateTimeOp.Day),
                    status = statusCombobox.Text == "Activo" ? true : false
                };

                //hotel.id = Convert.ToInt32(dgvHoteles.CurrentRow.Cells["ID"].Value);

                List<string> serviciosSeleccionadosNombres = new List<string>();

                foreach (var item in CLBServicios.CheckedItems)
                {
                    // Asegurarse de que el item sea del tipo 'additionalService'
                    serviciosSeleccionadosNombres.Add(item.ToString());

                }

                // Asegúrate de que 'hotel.aditionalService' sea un List<string>
                hotel.aditionalService = serviciosSeleccionadosNombres;

                bool resultado = enlace.UpdateHotel(hotel);




                if (resultado)
                {
                    MessageBox.Show("Hotel actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //dgvHoteles.DataSource = enlace.GetAllHotels();
                    btnGuardarCambiosHotel.Visible = false;
                    Paistxt.Enabled = true;
                    Ciudadtxt.Enabled = true;
                    Estadotxt.Enabled = true;
                    NombreHoteltxt.Enabled = true;
                    LimpiarCampos();
                    LoadInfo(); // Recargar la lista de hoteles en el DataGridView
                }
                else
                {
                    MessageBox.Show("Error al actualizar el hotel.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Selecciona un hotel para modificar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void LimpiarCampos()
        {


            NombreHoteltxt.Clear();

            VistaRtxt.Clear();
            Ciudadtxt.Clear();
            Estadotxt.Clear();
            Paistxt.Clear();
            Domiciliotxt.Clear();

            pisosnum.Value = 0;
            numPools.Value = 0;

            phoneTextBox.Text = string.Empty;
            statusCombobox.SelectedIndex = -1; // Resetea el combo box a ningún seleccionado

            SEsi.Checked = false;
            ZTsi.Checked = false;


            FechaOps.Value = DateTime.Now;

            // Limpiar los servicios seleccionados
            for (int i = 0; i < CLBServicios.Items.Count; i++)
            {
                CLBServicios.SetItemChecked(i, false);
            }
            dgvHoteles.DataSource = enlace.GetAllHotels();
        }

        private void btnModHot_Click(object sender, EventArgs e)
        {
            btnGuardarCambiosHotel.Visible = true;
            Paistxt.Enabled = false;
            Ciudadtxt.Enabled = false;
            Estadotxt.Enabled = false;
            NombreHoteltxt.Enabled = false;
        }

        private void SEsi_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void pisosnum_ValueChanged(object sender, EventArgs e)
        {

        }

        private void CLBServicios_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        /* yes */
        internal bool LoadInfo()
        {
            List<Hotel> hotels = enlace.GetAllHotels();

            dgvHoteles.DataSource = null;
            dgvHoteles.Rows.Clear();
            dgvHoteles.Columns.Clear(); // Clear existing columns
            dgvHoteles.Columns.Add("ID", "ID");
            dgvHoteles.Columns.Add("Status", "Status");
            dgvHoteles.Columns.Add("Nombre", "Nombre");
            dgvHoteles.Columns.Add("Pais", "Pais");
            dgvHoteles.Columns.Add("Ciudad", "Ciudad");
            dgvHoteles.Columns.Add("Estado", "Estado");
            dgvHoteles.Columns.Add("Domicilio", "Domicilio");
            dgvHoteles.Columns.Add("Telefono", "Telefono");
            dgvHoteles.Columns.Add("NumPisos", "Numero de Pisos");
            dgvHoteles.Columns.Add("SalonEventos", "Salon de Eventos");
            dgvHoteles.Columns.Add("ZonaTuristica", "Zona Turistica");
            dgvHoteles.Columns.Add("Piscina", "Piscina");
            dgvHoteles.Columns.Add("Vista", "Vista");
            dgvHoteles.Columns.Add("FechaInicioOps", "Fecha Inicio Operacion");
            dgvHoteles.Columns.Add("FechaInicioOps", "Fecha de Registro");

            if (hotels.Count == 0)
            {
                //MessageBox.Show("No se encontraron hoteles.");
                dgvHoteles.Rows.Add("Ninguno registrado!");
                return false;
            }
            foreach (Hotel hotel in hotels)
            {
                // Assuming you have a DataGridView named dataGridView1
                dgvHoteles.Rows.Add(hotel.id, hotel.status ? "Activo" : "Inactivo", hotel.name, hotel.country, hotel.city, hotel.state, hotel.address, hotel.phone, hotel.totalFloors, hotel.turisticZone ? "Si" : "No", hotel.eventsSalon ? "Si" : "No", hotel.numPools, hotel.views, hotel.dateOperation, hotel.dateRegistered);
            }

            return true;
        }

    }


}