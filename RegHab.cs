using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Pantallas_alto_volumen_de_datos.Entidades;
using System.Data.SqlClient;
using Pantallas_alto_volumen_de_datos.DAO;
using Pantallas_alto_volumen_de_datos.User;
using System.Net.NetworkInformation;
using CassandraEnlaceServer;

namespace Pantallas_alto_volumen_de_datos
{
    public partial class RegHab : Form
    {
        EnlaceCassandra enlace = new EnlaceCassandra();
        private EmpleadosCassandra SesionUsuario;
        private DateTime DateTimeOp;
        public RegHab(EmpleadosCassandra usuerConnected, DateTime DateTimeOp)
        {
            InitializeComponent();
            this.SesionUsuario = usuerConnected;
            this.DateTimeOp = DateTimeOp;
            RoomType_Load();
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }
        
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        
        private void RegHab_Load(object sender, EventArgs e)
        {
            CargarAmenidades();
            List<Hotel> hoteles = enlace.GetAllHotels(); // o como se llame tu clase DAO
            hoteles.Insert(0, new Hotel { id = -1, name = "Seleccionar Hotel" }); // Agregar opción de selección
            HotelHabcb.DataSource = null; // Limpiar el DataSource antes de asignar uno nuevo
            HotelHabcb.DataSource = hoteles;
            HotelHabcb.DisplayMember = "name";
            HotelHabcb.ValueMember = "id";
            //HotelHabcb.Items.Add("Seleccionar"); // Limpiar el DataSource antes de asignar uno nuevo
        }
        private bool RoomType_Load(int id = -1)
        {
            dgvHabitaciones.DataSource = null;
            dgvHabitaciones.Rows.Clear();
            List<roomTypes> AllRoomTypes = enlace.GetAllRoomTypes();

            dgvTiposHab.DataSource = null;
            dgvTiposHab.Rows.Clear();
            dgvTiposHab.Columns.Clear(); // Clear existing columns
            dgvTiposHab.Columns.Add("name", "Nombre");
            dgvTiposHab.Columns.Add("ID", "ID");
            dgvTiposHab.Columns.Add("idHotel", "Hotel");
            dgvTiposHab.Columns.Add("bedTypes", "Tipo de cama");
            dgvTiposHab.Columns.Add("bedNums", "Numero de camas");
            dgvTiposHab.Columns.Add("priceNight", "Precio por noche");
            dgvTiposHab.Columns.Add("maxGuests", "Número de personas");
            dgvTiposHab.Columns.Add("maxRooms", "Número de habitaciones");
            dgvTiposHab.Columns.Add("description", "Descripción");
            dgvTiposHab.Columns.Add("viewType", "Vista");
            dgvTiposHab.Columns.Add("dimensions", "Dimensiones");
            dgvTiposHab.Columns.Add("level", "Nivel de habitación");
            //dgvTiposHab.Columns.Add("amenites", "Amenidades");
            dgvTiposHab.Columns.Add("habInitNum", "Habitación inicial");
            dgvTiposHab.Columns.Add("habEndNum", "Habitación final");
            dgvTiposHab.Columns.Add("emailUser", "Usuario de registro");
            dgvTiposHab.Columns.Add("dateRegistered", "Dia de registro");

            if (AllRoomTypes.Count == 0)
            {
                //MessageBox.Show("No se encontraron hoteles.");
                dgvTiposHab.Rows.Add("Ninguno registrado!");
                return false;
            }
            foreach (roomTypes roomtype in AllRoomTypes)
            {
                // Assuming you have a DataGridView named dataGridView1
                if(id != -1 && roomtype.id != id)
                {
                    continue; // Skip this room type if it doesn't match the provided ID
                }
                dgvTiposHab.Rows.Add(roomtype.name, roomtype.id, roomtype.idHotel, roomtype.bedTypes, roomtype.bedNums, roomtype.priceNight, roomtype.maxGuests, roomtype.maxRooms, roomtype.description, roomtype.viewType, roomtype.dimensions, roomtype.level, roomtype.habInitNum, roomtype.habEndNum, roomtype.emailUser, roomtype.dateRegistered);
            }
            dgvTiposHab.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

            return true;
        }

        private void CargarAmenidades()
        {
            List<amenities> amenidades = enlace.GetAllAmenities();

            clbAmenidades.DataSource = amenidades; // tu CheckedListBox
            clbAmenidades.DisplayMember = "name";
            clbAmenidades.ValueMember = "name";
        }

        private void RegHabBtn_Click(object sender, EventArgs e)
        {
            if(HotelHabcb.SelectedIndex == 0)
            {
                MessageBox.Show("Por favor selecciona un hotel.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 1. Validar que ningún campo esté vacío
            if (string.IsNullOrWhiteSpace(NomHabtxt.Text) ||
                string.IsNullOrWhiteSpace(NumCamas.Text) ||
                cantPersonasInt.Value == 0 ||
                string.IsNullOrWhiteSpace(PrecioPersonatxt.Text) ||
                string.IsNullOrWhiteSpace(Dimensionestxt.Text) ||
                habInitINT.Value == 0 ||
                habEndINT.Value == 0 ||
                string.IsNullOrWhiteSpace(CantHabitaciones.Text) ||
                HotelHabcb.SelectedItem == null ||
                string.IsNullOrWhiteSpace(NiveldHabcb.Text) ||
                string.IsNullOrWhiteSpace(Vistacb.Text))
            {
                MessageBox.Show("Por favor completa todos los campos obligatorios.", "Campos vacíos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Validar números
            if (!int.TryParse(NumCamas.Text, out int numCamas) || numCamas <= 0 ||
                !decimal.TryParse(PrecioPersonatxt.Text, out decimal precioPersona) || precioPersona < 0 ||
                !int.TryParse(CantHabitaciones.Text, out int cantHabitaciones) || cantHabitaciones <= 0)
            {
                MessageBox.Show("Verifica que todos los valores numéricos sean válidos y positivos.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int numInicial = (int)habInitINT.Value;
            int numFinal = (int)habEndINT.Value;
            int cantPersonas = (int)cantPersonasInt.Value;

            if (numFinal < numInicial)
            {
                MessageBox.Show("El número final no puede ser menor al número inicial.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int totalHabitacionesCalculadas = numFinal - numInicial + 1;
            if (totalHabitacionesCalculadas != cantHabitaciones)
            {
                MessageBox.Show($"El rango de habitaciones no coincide con la cantidad de habitaciones.\nHabitaciones en rango: {totalHabitacionesCalculadas}\nCantidad ingresada: {cantHabitaciones}", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Hotel hotelSeleccionado = (Hotel)HotelHabcb.SelectedItem;
            int idHotelSeleccionado = hotelSeleccionado.id;

            roomTypes tipoHabitacion = new roomTypes
            {
                name = NomHabtxt.Text.Trim(),
                bedNums = numCamas,
                bedTypes = TipoCamacb.Text.Trim(),
                maxGuests = cantPersonas,
                priceNight = precioPersona,
                maxRooms = cantHabitaciones,
                level = NiveldHabcb.Text.Trim(),
                dimensions = Dimensionestxt.Text.Trim(),
                description = Caracteristicastxt.Text.Trim(),
                amenites = clbAmenidades.CheckedItems.Cast<amenities>().Select(a => a.name).ToList(), // se asigna después
                habInitNum = numInicial,
                habEndNum = numFinal,
                emailUser = SesionUsuario.email,
                dateRegistered = new Cassandra.LocalDate(DateTimeOp.Year, DateTimeOp.Month, DateTimeOp.Day),
                viewType = Vistacb.Text.Trim(),
                idHotel = idHotelSeleccionado
            };
            //tipoHabitacion.amenites = clbAmenidades.CheckedItems.Cast<amenities>().Select(a => a.name).ToList();

            // Aquí sigue todo tu código igual...
            bool resultadoTipo = enlace.InsertRoomType(tipoHabitacion);

            if (resultadoTipo)
            {
                MessageBox.Show("¡Tipo de habitación registrado correctamente!");
                int habId = enlace.GetLastHabitacionId();
                //Crear habitaciones
                if (habId <= 0)
                {
                    habId = 0;

                }
                MessageBox.Show("¡Registrando las habitacion!");
                for (int i = numInicial; i <= numFinal; i++)
                {
                    Habitaciones habitacion = new Habitaciones
                    {
                        id = ++habId, // Aquí se usa el ID del tipo de habitación recién creado
                        num = i,
                        tipo = tipoHabitacion.name,
                        status = "Disponible",
                        hotel = idHotelSeleccionado
                    };
                    enlace.InsertHabitaciones(habitacion);
                }

                MessageBox.Show("Habitaciones creadas y amenidades asignadas exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarCampos();
            }
            else
            {
                MessageBox.Show("Error al registrar el tipo de habitación.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            RoomType_Load(); // Recargar los tipos de habitación para mostrar el nuevo
        }

        private void LimpiarCampos()
        {
            NomHabtxt.Clear();
            NumCamas.Value = 0;
            TipoCamacb.SelectedIndex = -1;
            Vistacb.SelectedIndex = -1;
            cantPersonasInt.Value = 0;
            PrecioPersonatxt.Clear();
            CantHabitaciones.Value = 0;
            NiveldHabcb.SelectedIndex = -1;
            Dimensionestxt.Clear();
            Caracteristicastxt.Clear();
            habInitINT.Value = 0;
            habEndINT.Value = 0;
            HotelHabcb.SelectedIndex = -1;

            dgvHabitaciones.DataSource = null;
            dgvTiposHab.DataSource = null;

            // Desmarcar todas las amenidades
            for (int i = 0; i < clbAmenidades.Items.Count; i++)
            {
                clbAmenidades.SetItemChecked(i, false);
            }
        }

        private void CargarAmenidadesPorTipo(int idTipo)
        {
            List<string> serviciosHotel = enlace.GetAllAmenitiesByRoomType(idTipo);

            // Primero desmarcamos todo  
            for (int i = 0; i < clbAmenidades.Items.Count; i++)
            {
                clbAmenidades.SetItemChecked(i, false);
            }

            // Ahora marcamos los que correspondan  
            foreach (var name in serviciosHotel)
            {
                for (int i = 0; i < clbAmenidades.Items.Count; i++)
                {
                    var servicio = clbAmenidades.Items[i] as amenities;
                    if (servicio != null && servicio.name == name)
                    {
                        clbAmenidades.SetItemChecked(i, true);
                    }
                }
            }
        }

        private void ModHabbtn_Click(object sender, EventArgs e)
        {
            if (HotelHabcb.SelectedIndex == 0)
            {
                MessageBox.Show("Por favor selecciona un hotel.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Guardarhabbtn.Visible = true;
        }

        private void Guardarhabbtn_Click(object sender, EventArgs e)
        {
            ModificarTipoHabitacion();
        }

        private void ModificarTipoHabitacion()
        {

            string roomtype = dgvTiposHab.SelectedRows.Count > 0 ? dgvTiposHab.SelectedRows[0].Cells["name"].Value.ToString() : string.Empty;
            int idHotel = dgvTiposHab.SelectedRows.Count > 0 ? Convert.ToInt32(dgvTiposHab.SelectedRows[0].Cells["idHotel"].Value) : -1;
            int roomTypeID = dgvTiposHab.SelectedRows.Count > 0 ? Convert.ToInt32(dgvTiposHab.SelectedRows[0].Cells["ID"].Value) : -1;

            if (string.IsNullOrEmpty(roomtype) || idHotel == -1 || roomTypeID == -1)
            {
                MessageBox.Show("Por favor selecciona un tipo de habitación para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            enlace.deleteRoomTypeFromHotel(roomTypeID, idHotel, roomtype);

            if (HotelHabcb.SelectedIndex == 0)
            {
                MessageBox.Show("Por favor selecciona un hotel.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 1. Validar que ningún campo esté vacío
            if (string.IsNullOrWhiteSpace(NomHabtxt.Text) ||
                string.IsNullOrWhiteSpace(NumCamas.Text) ||
                cantPersonasInt.Value == 0 ||
                string.IsNullOrWhiteSpace(PrecioPersonatxt.Text) ||
                string.IsNullOrWhiteSpace(Dimensionestxt.Text) ||
                habInitINT.Value == 0 ||
                habEndINT.Value == 0 ||
                string.IsNullOrWhiteSpace(CantHabitaciones.Text) ||
                HotelHabcb.SelectedItem == null ||
                string.IsNullOrWhiteSpace(NiveldHabcb.Text) ||
                string.IsNullOrWhiteSpace(Vistacb.Text))
            {
                MessageBox.Show("Por favor completa todos los campos obligatorios.", "Campos vacíos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Validar números
            if (!int.TryParse(NumCamas.Text, out int numCamas) || numCamas <= 0 ||
                !decimal.TryParse(PrecioPersonatxt.Text, out decimal precioPersona) || precioPersona < 0 ||
                !int.TryParse(CantHabitaciones.Text, out int cantHabitaciones) || cantHabitaciones <= 0)
            {
                MessageBox.Show("Verifica que todos los valores numéricos sean válidos y positivos.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int numInicial = (int)habInitINT.Value;
            int numFinal = (int)habEndINT.Value;
            int cantPersonas = (int)cantPersonasInt.Value;

            if (numFinal < numInicial)
            {
                MessageBox.Show("El número final no puede ser menor al número inicial.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int totalHabitacionesCalculadas = numFinal - numInicial + 1;
            if (totalHabitacionesCalculadas != cantHabitaciones)
            {
                MessageBox.Show($"El rango de habitaciones no coincide con la cantidad de habitaciones.\nHabitaciones en rango: {totalHabitacionesCalculadas}\nCantidad ingresada: {cantHabitaciones}", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Hotel hotelSeleccionado = (Hotel)HotelHabcb.SelectedItem;
            int idHotelSeleccionado = hotelSeleccionado.id;

            roomTypes tipoHabitacion = new roomTypes
            {
                name = NomHabtxt.Text.Trim(),
                bedNums = numCamas,
                bedTypes = TipoCamacb.Text.Trim(),
                maxGuests = cantPersonas,
                priceNight = precioPersona,
                maxRooms = cantHabitaciones,
                level = NiveldHabcb.Text.Trim(),
                dimensions = Dimensionestxt.Text.Trim(),
                description = Caracteristicastxt.Text.Trim(),
                amenites = clbAmenidades.CheckedItems.Cast<amenities>().Select(a => a.name).ToList(), // se asigna después
                habInitNum = numInicial,
                habEndNum = numFinal,
                emailUser = SesionUsuario.email,
                dateRegistered = new Cassandra.LocalDate(DateTimeOp.Year, DateTimeOp.Month, DateTimeOp.Day),
                viewType = Vistacb.Text.Trim(),
                idHotel = idHotelSeleccionado
            };
            //tipoHabitacion.amenites = clbAmenidades.CheckedItems.Cast<amenities>().Select(a => a.name).ToList();

            // Aquí sigue todo tu código igual...
            bool resultadoTipo = enlace.InsertRoomType(tipoHabitacion);

            if (resultadoTipo)
            {
                MessageBox.Show("¡Tipo de habitación registrado correctamente!");
                int habId = enlace.GetLastHabitacionId();
                //Crear habitaciones
                if (habId <= 0)
                {
                    habId = 0;

                }
                MessageBox.Show("¡Registrando las habitacion!");
                for (int i = numInicial; i <= numFinal; i++)
                {
                    Habitaciones habitacion = new Habitaciones
                    {
                        id = ++habId, // Aquí se usa el ID del tipo de habitación recién creado
                        num = i,
                        tipo = tipoHabitacion.name,
                        status = "Disponible",
                        hotel = idHotelSeleccionado
                    };
                    enlace.InsertHabitaciones(habitacion);
                }

                MessageBox.Show("Habitaciones creadas y amenidades asignadas exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarCampos();
            }
            else
            {
                MessageBox.Show("Error al registrar el tipo de habitación.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            LimpiarCampos();
            RoomType_Load(); // Recargar los tipos de habitación después de eliminar
        }

        private void dgvTiposHab_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Obtener ID del tipo seleccionado
                int idTipo = Convert.ToInt32(dgvTiposHab.Rows[e.RowIndex].Cells["ID"].Value);


                // Obtener tipo de habitación completo (opcional si lo usas después)
                roomTypes tipo = enlace.GetAllRoomTypes().FirstOrDefault(t => t.id == idTipo);

                DataGridViewRow fila = dgvTiposHab.Rows[e.RowIndex];

                if(fila == null || fila.Cells.Count == 0)
                {
                    MessageBox.Show("No se seleccionó una fila válida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (fila.Cells["ID"].Value == null)
                {
                    MessageBox.Show("No se seleccionó un tipo de habitación válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                // Llenar los TextBox
                IDTipoTxt.Text = fila.Cells["ID"].Value.ToString();
                NomHabtxt.Text = fila.Cells["name"].Value.ToString();
                NumCamas.Text = fila.Cells["bedNums"].Value.ToString();
                TipoCamacb.Text = fila.Cells["bedTypes"].Value.ToString();
                cantPersonasInt.Value = (int)fila.Cells["maxGuests"].Value;
                PrecioPersonatxt.Text = fila.Cells["priceNight"].Value.ToString();
                CantHabitaciones.Text = fila.Cells["maxRooms"].Value.ToString();
                Vistacb.Text = fila.Cells["viewType"].Value.ToString();
                NiveldHabcb.Text = fila.Cells["level"].Value.ToString();
                Dimensionestxt.Text = fila.Cells["dimensions"].Value.ToString();
                Caracteristicastxt.Text = fila.Cells["description"].Value.ToString();
                habInitINT.Value = (int)fila.Cells["habInitNum"].Value;
                habEndINT.Value = (int)fila.Cells["habEndNum"].Value;
                HotelHabcb.SelectedValue = fila.Cells["idHotel"].Value;

                int idHotel = (int)fila.Cells["idHotel"].Value;
                if (idHotel <= 0) { MessageBox.Show(null,"Hubo un error con el id de el tipo de habitación", null); return; }
                loadRoomTypeHabtiationsOfHotel(idHotel, fila.Cells["name"].Value.ToString());
                // Ya tienes idTipo declarado arriba, úsalo aquí
                CargarAmenidadesPorTipo(idTipo);
            }
        }

        public void loadRoomTypeHabtiationsOfHotel(int id, string type)
        {
            List<Habitaciones> habitaciones = enlace.GetAllRoomsByHotelId(id);
            if (habitaciones.Count == 0)
            {
                //MessageBox.Show("No se encontraron habitaciones para este hotel.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                dgvHabitaciones.DataSource = null;
                dgvHabitaciones.Rows.Clear();
                dgvHabitaciones.Columns.Clear(); // Limpiar columnas existentes
                dgvHabitaciones.Columns.Add("id", "ID");
                dgvHabitaciones.Columns.Add("num", "Número");
                dgvHabitaciones.Columns.Add("tipo", "Tipo");
                dgvHabitaciones.Columns.Add("status", "Estado");
                dgvHabitaciones.Columns.Add("hotel", "Hotel");
                foreach (var habitacion in habitaciones)
                {
                    if (habitacion.tipo == type)
                    {

                        dgvHabitaciones.Rows.Add(habitacion.id, habitacion.num, habitacion.tipo, habitacion.status, habitacion.hotel);
                    }
                }
                if (dgvHabitaciones.Rows.Count == 0) { 
                    dgvHabitaciones.Rows.Add(0, $"Ninguna habitación {type} registrada");
                }
                dgvHabitaciones.Columns["id"].Visible = false; // Ocultar la columna de ID si no es necesaria
                dgvHabitaciones.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                int dgvWidth = 0;
                foreach (DataGridViewColumn column in dgvHabitaciones.Columns)
                {
                    dgvWidth += column.Width;
                }
                if (dgvWidth < dgvHabitaciones.Width)
                {
                    //dgvHabitaciones.Columns[0].Width = 50; // Ajustar el ancho de la primera columna
                }
                else
                {
                }
                dgvHabitaciones.Size = new Size(dgvWidth + 30, dgvHabitaciones.Size.Height);
                
                //dgvHabitaciones.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Ajustar columnas para llenar el espacio disponible

            }
        }

        private void EliHabbtn_Click(object sender, EventArgs e)
        {
            string roomtype = dgvTiposHab.SelectedRows.Count > 0 ? dgvTiposHab.SelectedRows[0].Cells["name"].Value.ToString() : string.Empty;
            int idHotel = dgvTiposHab.SelectedRows.Count > 0 ? Convert.ToInt32(dgvTiposHab.SelectedRows[0].Cells["idHotel"].Value) : -1;
            int roomTypeID = dgvTiposHab.SelectedRows.Count > 0 ? Convert.ToInt32(dgvTiposHab.SelectedRows[0].Cells["ID"].Value) : -1;

            if (string.IsNullOrEmpty(roomtype) || idHotel == -1 || roomTypeID == -1)
            {
                MessageBox.Show("Por favor selecciona un tipo de habitación para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            enlace.deleteRoomTypeFromHotel(roomTypeID, idHotel, roomtype);
            LimpiarCampos();
            RoomType_Load(); // Recargar los tipos de habitación después de eliminar
        }

        private void clbAmenidades_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void HotelSelectedChanged(object sender, EventArgs e)
        {
            var id = HotelHabcb.SelectedValue;

            //MessageBox.Show(null, id.ToString(), null);

            int idHotel = Convert.ToInt32(id);
            if(idHotel == -1)
            {
                // Si se selecciona "Seleccionar Hotel", no hacemos nada
                RoomType_Load();
                return;
            }
            RoomType_Load(idHotel);
        }
    }
}