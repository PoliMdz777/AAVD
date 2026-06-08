using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cassandra;
using CassandraEnlaceServer;
using Pantallas_alto_volumen_de_datos.DAO;
using Pantallas_alto_volumen_de_datos.Entidades;
using Pantallas_alto_volumen_de_datos.User;


namespace Pantallas_alto_volumen_de_datos
{
    public partial class Reservaciones : Form
    {

        EnlaceCassandra enlace = new EnlaceCassandra();
        private EmpleadosCassandra SesionUsuario;
        private DateTime DateTimeOp;

        public Reservaciones(EmpleadosCassandra usuerConnected, DateTime DateTimeOp)
        {
            InitializeComponent();
            this.SesionUsuario = usuerConnected;
            this.DateTimeOp = DateTimeOp;
            dgvDetalleReservacion.DataError += dgv_DataError;
            loadCiudadesCombobox();
        }

        //variables

        private List<string> habitacionesSeleccionadas = new List<string>();

        private int idHotelSeleccionado;
        private int idTipoSeleccionado;
        private decimal totalConImpuestos = 0;
        private decimal anticipoCalculado = 0;
        private int numeroNoches = 0;

        private LocalDate enter;
        private LocalDate end;

        public List<Habitaciones> GetAllRoomNumsAvailableBetweenDates(string typeSelected)
        {
            List<Habitaciones> allHabType = enlace.GetAllHabtiationOfType(idHotelSeleccionado, typeSelected);
            List<int> numsReserved = GetAllNumsHabOfTypeFromReserv(typeSelected);

            // Initialize the 'aceptadas' list to avoid CS0165 error  
            List<Habitaciones> aceptadas = new List<Habitaciones>();
            bool aceptar = true;

            foreach (Habitaciones Habt in allHabType)
            {
                aceptar = true; // Reset 'aceptar' for each room  
                foreach (int numHab in numsReserved)
                {
                    if (Habt.num == numHab)
                    {
                        aceptar = false;
                        break;
                    }
                }
                if (aceptar)
                {
                    aceptadas.Add(Habt);
                }
            }
            return aceptadas;
        }




        public List<int> GetAllNumsHabOfTypeFromReserv(string typeSelected)
        {
            List<Reservation> allReservations = enlace.GetAllReservationsBetweenDaysInHotelAlternative(idHotelSeleccionado, enter, end);
            List<int> numReservAceptadas = new List<int>();
            bool aceptar = false;
            foreach (Reservation reserv in allReservations)
            {

                List<string> habReserved = reserv.RoomType;

                foreach (string room in habReserved)
                {
                    string roomCopy = room;
                    string habNum = roomCopy.Split('-')[0].Trim();
                    string habType = room.Split('-')[1].Trim();

                    if (habType == typeSelected)
                    {
                        aceptar = true;
                    }
                    if (aceptar)
                    {
                        int numHab;
                        int.TryParse(habNum, out numHab);
                        numReservAceptadas.Add(numHab);
                    }
                }
            }
            return numReservAceptadas;
        }


        
        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void Reservaciones_Load(object sender, EventArgs e)
        {

        }


        private bool loadCiudadesCombobox()
        {
            try
            {
                List<Hotel> allHoteles = enlace.GetAllHotels();

                List<string> ciudades = allHoteles
                    .Select(h => h.city)
                    .Distinct()
                    .OrderBy(c => c)
                    .ToList();

                if (ciudades.Count == 0)
                {
                    MessageBox.Show("No se encontraron ciudades en la base de datos.");
                    return false;
                }
                ciudadFiltro.DataSource = ciudades;
                ciudadFiltro.SelectedIndex = -1; // Para que no haya selección inicial
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar las ciudades: {ex.Message}");
                return false;
            }
        }


        private void btnFiltrarCiudad_Click(object sender, EventArgs e)
        {
            string ciudad = ciudadFiltro.Text.Trim();

            if (string.IsNullOrWhiteSpace(ciudad))
            {
                MessageBox.Show("Debes escribir una ciudad para filtrar.");
                return;
            }

            List<Hotel> hoteles = enlace.ObtenerHotelesPorCiudad(ciudad);

            if (hoteles.Count == 0)
            {
                MessageBox.Show("No se encontraron hoteles en esa ciudad.");
                return;
            }

            HotelesenCiudad.DataSource = hoteles;

        }

        private void LimpiarFormularioReservacion()
        {
            txtRFC.Clear();
            txtNombreCliente.Clear();
            txtFormaPago.SelectedIndex = -1;
            txtTotal.Clear();
            txtAnticipo.Clear();
            //txtpersonas.Clear();
            dtpEntrada.Value = DateTime.Now;
            dtpSalida.Value = DateTime.Now.AddDays(1);
            habitacionesSeleccionadas.Clear();
            dgvDisponibles.DataSource = null; // Si usas un DataGridView para mostrar
        }

        private void HotelesenCiudad_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow filaSeleccionada = HotelesenCiudad.Rows[e.RowIndex];

                idHotelSeleccionado = Convert.ToInt32(filaSeleccionada.Cells["id"].Value); // Asegúrate que el nombre de columna sea correcto

                List<roomTypes> habitaciones = enlace.ObtenerTiposHabitacionPorHotel(idHotelSeleccionado);

                TiposdHabitacion.DataSource = habitaciones; // Asegúrate de que este es el nombre del segundo DataGridView
            }


        }

        private void btnBuscarHab_Click(object sender, EventArgs e)
        {
            if (TiposdHabitacion.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecciona un tipo de habitación.");
                return;
            }

            DataGridViewRow fila1 = TiposdHabitacion.SelectedRows[0];
            int idHotelSeleccionado = Convert.ToInt32(fila1.Cells["idHotel"].Value);

            if (idHotelSeleccionado <= 0)
            {
                MessageBox.Show("Seleccion invalida");
                return;
            }

            // Validar fechas
            DateTime fechaInicioDateTime = dtpEntrada.Value.Date;
            DateTime fechaFinDateTime = dtpSalida.Value.Date;

            if (fechaFinDateTime <= fechaInicioDateTime)
            {
                MessageBox.Show("La fecha de salida debe ser mayor a la fecha de entrada.", "Fechas inválidas", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            enter= new LocalDate(fechaInicioDateTime.Year,fechaInicioDateTime.Month,fechaInicioDateTime.Day);
            end = new LocalDate(fechaFinDateTime.Year, fechaFinDateTime.Month, fechaFinDateTime.Day);


            List<Habitaciones> habitacionesDisponibles = GetAllRoomNumsAvailableBetweenDates(TiposdHabitacion.SelectedRows[0].Cells["name"].Value.ToString());


            // Convertir fechas a tipo Cassandra.LocalDate
            var fechaInicio = new Cassandra.LocalDate(fechaInicioDateTime.Year, fechaInicioDateTime.Month, fechaInicioDateTime.Day);
            var fechaFin = new Cassandra.LocalDate(fechaFinDateTime.Year, fechaFinDateTime.Month, fechaFinDateTime.Day);

            // Tomar tipo de habitación seleccionado
            DataGridViewRow fila = TiposdHabitacion.SelectedRows[0];
            int idTipo = Convert.ToInt32(fila.Cells["id"].Value);
            string tipoNombre = fila.Cells["name"].Value.ToString();

            // Guardamos el tipo
            idTipoSeleccionado = idTipo;

            roomTypes room = enlace.GetRoomTypeById(idTipo);

            // Obtener habitaciones disponibles
            List<Habitaciones> disponibles = enlace.ObtenerHabitacionesDisponibles(
                idHotelSeleccionado,
                room.name,
                fechaInicio,
                fechaFin
            );

            if (disponibles == null || disponibles.Count == 0)
            {
                MessageBox.Show("No hay habitaciones disponibles para este tipo y fechas.");
            }

            dgvDisponibles.DataSource = habitacionesDisponibles;
        }


        private void dgvDisponibles_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvDisponibles.Rows[e.RowIndex];

                // Obtener ID del hotel y tipo de la habitación
                int idHotel = Convert.ToInt32(fila.Cells["hotel"].Value);
                string tipoNombre = fila.Cells["tipo"].Value.ToString();

                // Buscar detalles del tipo de habitación
                List<roomTypes> tipos = enlace.ObtenerTiposHabitacionPorHotel(idHotel);
                roomTypes tipo = tipos.FirstOrDefault(t => t.name == tipoNombre);

                if (tipo != null)
                {
                    //txtTipoHabitacionSeleccionado.Text = tipo.name;
                    //txtpersonas.Text = tipo.maxGuests.ToString();

                    // Guardamos variables si las necesitas después
                    idTipoSeleccionado = tipo.id;
                }
                else
                {
                    //txtTipoHabitacionSeleccionado.Text = "Desconocido";
                    //txtpersonas.Clear();
                }
            }
        }
        

        private void tbnGuardarHab_Click(object sender, EventArgs e)
        {
            if (dgvDisponibles.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecciona una habitación disponible.");
                return;
            }

            // Obtener la fila seleccionada
            DataGridViewRow fila = dgvDisponibles.SelectedRows[0];

            int numHab = Convert.ToInt32(fila.Cells["num"].Value);
            string tipoNombre = fila.Cells["tipo"].Value.ToString();
            string habitacionTexto = $"{numHab} - {tipoNombre}";

            // Verificar si ya fue seleccionada
            if (habitacionesSeleccionadas.Contains(habitacionTexto))
            {
                MessageBox.Show("Esta habitación ya fue seleccionada.");
                return;
            }

            // Validar que aún hay habitaciones disponibles de ese tipo
            int totalDisponibles = 0;
            foreach (DataGridViewRow row in dgvDisponibles.Rows)
            {
                if (!row.IsNewRow && row.Cells["tipo"].Value.ToString() == tipoNombre)
                    totalDisponibles++;
            }

            int yaSeleccionadas = habitacionesSeleccionadas.Count(h => h.Contains(tipoNombre));

            if (yaSeleccionadas >= totalDisponibles)
            {
                MessageBox.Show($"Ya seleccionaste todas las habitaciones disponibles de tipo '{tipoNombre}'.");
                return;
            }

            // Agregar a la lista
            habitacionesSeleccionadas.Add(habitacionTexto);

            MessageBox.Show($"Habitacion agregada con exito! '{tipoNombre}'.");

            // Cargar info de la habitación al textbox
            int hotelId = Convert.ToInt32(fila.Cells["hotel"].Value);
            var tipos = enlace.ObtenerTiposHabitacionPorHotel(hotelId);
            var tipo = tipos.FirstOrDefault(t => t.name == tipoNombre);

            if (tipo != null)
            {
                //txtTipoHabitacionSeleccionado.Text = tipo.name;
                //txtpersonas.Text = tipo.maxGuests.ToString();
                idTipoSeleccionado = tipo.id;
            }
            else
            {
                //txtTipoHabitacionSeleccionado.Text = "Desconocido";
                //txtpersonas.Clear();
            }

            LimpiarCamposHabitacion();
        }


        private void LimpiarCamposHabitacion()
        {

            //txtTipoHabitacionSeleccionado.Clear();
            
            //txtpersonas.Clear();
        }

        private void dgv_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            // Evita el error por índice fuera de rango
            e.ThrowException = false;
        }

        //private void btnConfirmarReservacion_Click(object sender, EventArgs e)
        //{
        //    // Validación 1: Habitaciones seleccionadas
        //    if (habitacionesSeleccionadas == null || habitacionesSeleccionadas.Count == 0)
        //    {
        //        MessageBox.Show("Debes seleccionar al menos una habitación.",
        //            "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        return;
        //    }

        //    // Validación 2: RFC y forma de pago
        //    if (string.IsNullOrWhiteSpace(txtRFC.Text))
        //    {
        //        MessageBox.Show("Debes ingresar el RFC del cliente.",
        //            "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        txtRFC.Focus();
        //        return;
        //    }

        //    if (string.IsNullOrWhiteSpace(txtNombreCliente.Text))
        //    {
        //        MessageBox.Show("Debes ingresar el nombre del cliente.",
        //            "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        txtNombreCliente.Focus();
        //        return;
        //    }

        //    if (txtFormaPago.SelectedIndex == -1)
        //    {
        //        MessageBox.Show("Debes seleccionar una forma de pago.",
        //            "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        txtFormaPago.Focus();
        //        return;
        //    }

        //    // Validación 3: RFC existe en la base de datos
        //    try
        //    {
        //        List<string> RFCexistentes = enlace.GetAllClientsRFC();
        //        if (!RFCexistentes.Contains(txtRFC.Text.Trim()))
        //        {
        //            MessageBox.Show("El RFC proporcionado no existe. Por favor, registre al cliente primero.",
        //                "RFC no encontrado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            return;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Error al validar el RFC: {ex.Message}",
        //            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return;
        //    }

        //    // Validación 4: Montos válidos
        //    decimal anticipoCalculado = 0;
        //    decimal totalConImpuestos = 0;

        //    try
        //    {
        //        string anticipoTexto = txtAnticipo.Text.Replace("$", "").Replace(",", "").Trim();
        //        if (!decimal.TryParse(anticipoTexto, out anticipoCalculado) || anticipoCalculado <= 0)
        //        {
        //            MessageBox.Show("El valor del anticipo no es válido.",
        //                "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            txtAnticipo.Focus();
        //            return;
        //        }

        //        string totalTexto = txtTotal.Text.Replace("$", "").Replace(",", "").Trim();
        //        if (!decimal.TryParse(totalTexto, out totalConImpuestos) || totalConImpuestos <= 0)
        //        {
        //            MessageBox.Show("El valor del total no es válido.",
        //                "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            txtTotal.Focus();
        //            return;
        //        }

        //        // Validar que el anticipo no sea mayor que el total
        //        if (anticipoCalculado > totalConImpuestos)
        //        {
        //            MessageBox.Show("El anticipo no puede ser mayor que el total.",
        //                "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            return;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Error al procesar los montos: {ex.Message}",
        //            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return;
        //    }

        //    // Validación 5: Fechas válidas
        //    DateTime fechaEntrada = dtpEntrada.Value.Date;
        //    DateTime fechaSalida = dtpSalida.Value.Date;
        //    int noches = (fechaSalida - fechaEntrada).Days;

        //    if (noches <= 0)
        //    {
        //        MessageBox.Show("La fecha de salida debe ser posterior a la fecha de entrada.",
        //            "Fechas inválidas", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        return;
        //    }

        //    // Validación 6: Hotel seleccionado
        //    if (idHotelSeleccionado <= 0)
        //    {
        //        MessageBox.Show("Debe seleccionar un hotel.",
        //            "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        return;
        //    }

        //    // Crear objeto de reservación
        //    try
        //    {
        //        var reserva = new Reservation
        //        {
        //            Id = TimeUuid.NewId(),
        //            RfcClient = txtRFC.Text.Trim().ToUpper(),
        //            name = txtNombreCliente.Text.Trim(),
        //            EmailUser = SesionUsuario?.email ?? "invitado@demo.com",
        //            DateReserved = DateTime.Now,
        //            IdHotel = idHotelSeleccionado,
        //            Nights = (short)noches,
        //            EntryDate = new LocalDate(fechaEntrada.Year, fechaEntrada.Month, fechaEntrada.Day),
        //            EndDate = new LocalDate(fechaSalida.Year, fechaSalida.Month, fechaSalida.Day),
        //            NumRooms = (short)habitacionesSeleccionadas.Count,
        //            RoomType = habitacionesSeleccionadas.ToList(), // Lista de strings "101 - Doble"
        //            CheckIn = fechaEntrada,
        //            CheckOut = fechaSalida,
        //            PayForm = txtFormaPago.Text.Trim(),
        //            SubTotal = (int)(totalConImpuestos / 1.18m),
        //            total = totalConImpuestos,
        //            anticipo = anticipoCalculado,
        //            Status = "Reservada"
        //        };

        //        // Confirmación final
        //        string mensaje = $"¿Confirmar reservación?\n\n" +
        //                        $"Cliente: {reserva.name} ({reserva.RfcClient})\n" +
        //                        $"Hotel ID: {reserva.IdHotel}\n" +
        //                        $"Noches: {reserva.Nights}\n" +
        //                        $"Habitaciones: {reserva.NumRooms}\n" +
        //                        $"Entrada: {fechaEntrada:dd/MM/yyyy}\n" +
        //                        $"Salida: {fechaSalida:dd/MM/yyyy}\n" +
        //                        //$"Total: {totalConImpuestos:C}\n" +
        //                        //$"Anticipo: {anticipoCalculado:C}";
        //        $"Total: {totalConImpuestos:$}\n" +
        //                     $"Anticipo: {anticipoCalculado:$}";

        //        if (MessageBox.Show(mensaje, "Confirmar Reservación",
        //            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
        //        {
        //            return;
        //        }

        //        // Guardar en la base de datos
        //        bool resultado = enlace.GuardarReservacion(reserva);

        //        if (resultado)
        //        {
        //            MessageBox.Show("¡Reservación guardada exitosamente!",
        //                "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            LimpiarFormularioReservacion();
        //        }
        //        else
        //        {
        //            MessageBox.Show("Ocurrió un error al guardar la reservación. Por favor, intente nuevamente.",
        //                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Error al crear la reservación: {ex.Message}\n\nStackTrace: {ex.StackTrace}",
        //            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        private void btnConfirmarReservacion_Click(object sender, EventArgs e)
        {

            if (habitacionesSeleccionadas == null || habitacionesSeleccionadas.Count == 0)
            {
                MessageBox.Show("Debes seleccionar al menos una habitación.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtRFC.Text) || string.IsNullOrWhiteSpace(txtFormaPago.Text))
            {
                MessageBox.Show("Debes ingresar RFC y forma de pago.");
                return;
            }

            List<string> RFCexistentes = enlace.GetAllClientsRFC();
            // Validar que el RFC exista en la base de datos
            if (!RFCexistentes.Contains(txtRFC.Text.Trim()))
            {
                MessageBox.Show("El RFC proporcionado no existe. Por favor, registre al cliente primero.");
                return;
            }


            // Reemplazar esta línea que causa el error:  
            // anticipoCalculado = txtAnticipo.Text;  

            // Limpiar y parsear el anticipo
            string anticipoCambiado = txtAnticipo.Text
                .Replace("$", "")           // Elimina símbolo de dólar
                .Replace("€", "")           // Elimina símbolo de euro
                .Replace(".", "")           // Elimina separadores de miles (puntos)
                .Replace(",", ".")          // Reemplaza coma decimal por punto
                .Trim();

            if (!decimal.TryParse(anticipoCambiado, NumberStyles.Any, CultureInfo.InvariantCulture, out anticipoCalculado) || anticipoCalculado <= 0)
            {
                MessageBox.Show($"El valor del anticipo no es válido: '{txtAnticipo.Text}'\nPor favor, ingrese un número válido.",
                    "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtAnticipo.Focus();
                return;
            }

            // Limpiar y parsear el total
            string totalCambiado = txtTotal.Text
                .Replace("$", "")           // Elimina símbolo de dólar
                .Replace("€", "")           // Elimina símbolo de euro
                .Replace(".", "")           // Elimina separadores de miles (puntos)
                .Replace(",", ".")          // Reemplaza coma decimal por punto
                .Trim();

            if (!decimal.TryParse(totalCambiado, NumberStyles.Any, CultureInfo.InvariantCulture, out totalConImpuestos) || totalConImpuestos <= 0)
            {
                MessageBox.Show($"El valor del total no es válido: '{txtTotal.Text}'\nPor favor, ingrese un número válido.",
                    "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTotal.Focus();
                return;
            }

            // Validar que el anticipo no sea mayor que el total
            if (anticipoCalculado > totalConImpuestos)
            {
                MessageBox.Show("El anticipo no puede ser mayor que el total.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //// Con la siguiente línea que convierte el texto a decimal:  
            //string anticipoCambiado = txtAnticipo.Text.Replace("$", ""); // Elimina el símbolo de moneda al final
            //if (!decimal.TryParse(anticipoCambiado, out anticipoCalculado))
            //{
            //    MessageBox.Show("El valor del anticipo no es válido. Por favor, ingrese un número válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}
            //string totalCambiado = txtTotal.Text.Replace("$", ""); // Elimina el símbolo de moneda al final
            //if (!decimal.TryParse(totalCambiado, out totalConImpuestos))
            //{
            //    MessageBox.Show("El valor del total no es válido. Por favor, ingrese un número válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}
            //idHotelSeleccionado = Convert.ToInt32(TiposdHabitacion.SelectedRows[0].Cells["idHotel"].Value);

            DateTime fechaEntrada = dtpEntrada.Value.Date;
            DateTime fechaSalida = dtpSalida.Value.Date;
            int noches = (fechaSalida - fechaEntrada).Days;

            if (noches <= 0)
            {
                MessageBox.Show("Las fechas no son válidas.");
                return;
            }

            List<string> habitacionesSeleccionadas1 = this.habitacionesSeleccionadas;


            // Agrupar nombres de tipo de habitación
            var tipos = habitacionesSeleccionadas
                .Select(h => h.Split('-')[1].Trim()) // "Hab 101 - Doble" => "Doble"
                .ToList();

            var reserva = new Reservation
            {


                Id = TimeUuid.NewId(),
                RfcClient = txtRFC.Text.Trim(),
                name = txtNombreCliente.Text.Trim(),
                EmailUser = SesionUsuario?.email ?? "invitado@demo.com",
                DateReserved = DateTime.Now,
                IdHotel = idHotelSeleccionado,
                Nights = (short)noches,
                EntryDate = new LocalDate(fechaEntrada.Year, fechaEntrada.Month, fechaEntrada.Day),
                EndDate = new LocalDate(fechaSalida.Year, fechaSalida.Month, fechaSalida.Day),
                NumRooms = (short)habitacionesSeleccionadas.Count,
                RoomType = habitacionesSeleccionadas1,
                //NumGuests = short.TryParse(txtpersonas.Text, out short numP) ? numP : (short)1,
                CheckIn = fechaEntrada,
                CheckOut = fechaSalida,
                PayForm = txtFormaPago.Text.Trim(),
                SubTotal = (int)(totalConImpuestos / 1.18m), // sin impuestos
                total = totalConImpuestos,
                anticipo = anticipoCalculado,
                Status = "Reservada",
            };

            if (MessageBox.Show($"¿Seguro que desea reservar {reserva.Nights} noches para el cliente {reserva.RfcClient} a nombre de {reserva.name}?", "Reservaciones", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
            {
                return;
            }

            bool resultado = enlace.GuardarReservacion(reserva);

            if (resultado)
            {
                MessageBox.Show("Reservación guardada exitosamente.");
                LimpiarFormularioReservacion();
            }
            else
            {
                MessageBox.Show("Ocurrió un error al guardar la reservación.");
            }
            LimpiarFormularioReservacion();
        }


    
        private void btnCalcular_Click_1(object sender, EventArgs e)
        {
            if (habitacionesSeleccionadas.Count == 0)
            {
                MessageBox.Show("No hay habitaciones seleccionadas.");
                return;
            }

            DateTime fechaEntrada = dtpEntrada.Value.Date;
            DateTime fechaSalida = dtpSalida.Value.Date;

            int noches = (fechaSalida - fechaEntrada).Days;
            if (noches <= 0)
            {
                MessageBox.Show("La fecha de salida debe ser posterior a la fecha de entrada.");
                return;
            }

            // Agrupar por tipo de habitación
            var habitacionesAgrupadas = habitacionesSeleccionadas
                .Select(h => h.Split('-')[1].Trim())  // extrae el nombre del tipo
                .GroupBy(tipo => tipo)
                .Select(grupo => new
                {
                    TipoNombre = grupo.Key,
                    Cantidad = grupo.Count()
                });

            DataGridViewRow fila1 = TiposdHabitacion.SelectedRows[0];
            int idHotelSeleccionado = Convert.ToInt32(fila1.Cells["idHotel"].Value);

            decimal total = 0;
            List<roomTypes> tiposHabitacionesHotel = enlace.ObtenerTiposHabitacionPorHotel(idHotelSeleccionado);


            foreach (var item in habitacionesAgrupadas)
            {
                var tipo = tiposHabitacionesHotel.FirstOrDefault(t => t.name == item.TipoNombre);
                if (tipo != null)
                {
                    decimal subtotal = item.Cantidad * noches * tipo.priceNight;
                    total += subtotal;
                }
                else
                {
                    MessageBox.Show($"No se encontró información de precio para el tipo: {item.TipoNombre}");
                }
            }

            // Opcional: impuestos y anticipo
            decimal totalConImpuestos = total * 1.18m;
            decimal anticipo = totalConImpuestos * 0.20m;

            txtTotal.Text = totalConImpuestos.ToString("$");
            txtAnticipo.Text = anticipo.ToString("$");
        }


        private void dgvDetalleReservacion_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }

  
}
