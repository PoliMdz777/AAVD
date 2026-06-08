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
using CassandraEnlaceServer;
using iTextSharp.text.pdf.codec.wmf;

namespace Pantallas_alto_volumen_de_datos
{
    public partial class CheckIn : Form
    {
        
        private Reservation reservaEncontrada;
        EnlaceCassandra enlace = new EnlaceCassandra();

        public CheckIn()
        {
            InitializeComponent();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

   
        private void btnBuscarReserva_Click(object sender, EventArgs e)
        {
            string selectedForm = cbHotel.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(selectedForm))
            {
                MessageBox.Show("Por favor selecciona un criterio de búsqueda válido.");
                return;
            }

            // Buscar la reservación según el criterio seleccionado
            switch (selectedForm)
            {
                case "Codigo de reservación":
                    if (!Guid.TryParse(txtBuscar.Text, out Guid codigo))
                    {
                        MessageBox.Show("El código ingresado no es válido.");
                        return;
                    }
                    reservaEncontrada = enlace.ObtenerReservacionPorCodigo2(codigo);
                    break;
                case "Nombre":
                    string name = txtBuscar.Text.Trim();
                    reservaEncontrada = enlace.ObtenerReservacionPorNombre(name);
                    break;
                case "RFC":
                    string rfc = txtBuscar.Text.Trim();
                    reservaEncontrada = enlace.ObtenerReservacionPorRFC(rfc);
                    break;
                default:
                    MessageBox.Show("Por favor selecciona un criterio de búsqueda válido.");
                    return;
            }

            if (reservaEncontrada == null)
            {
                MessageBox.Show("No se encontró una reservación con ese código.", "Reservación no encontrada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                LimpiarCamposCheckIn();
                return;
            }

            // Validar el estado de la reservación
            string estado = reservaEncontrada.Status?.ToLower() ?? "";

            if (estado == "cancelada")
            {
                MessageBox.Show("Esta reservación ya fue cancelada.", "Reservación Cancelada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                LimpiarCamposCheckIn();
                return;
            }

            if (estado == "checkin")
            {
                MessageBox.Show("Esta reservación ya tiene Check-In realizado.", "Check-In ya realizado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarCamposCheckIn();
                return;
            }

            if (estado == "checkout")
            {
                MessageBox.Show("Esta reservación ya tiene Check-Out completado.", "Reservación completada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarCamposCheckIn();
                return;
            }

            // Validar que la fecha de entrada sea hoy o futura
            //DateTime hoy = DateTime.Today;
            //DateTime fechaEntrada = reservaEncontrada.EntryDate.ToDateTimeOffset().Date;

            //if (fechaEntrada < hoy)
            //{
            //    MessageBox.Show($"La fecha de entrada de esta reservación ({fechaEntrada:dd/MM/yyyy}) ya pasó.\nNo se puede hacer check-in en fechas pasadas.",
            //        "Fecha vencida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    LimpiarCamposCheckIn();
            //    return;
            //}

            // Obtener información del hotel
            Hotel hotel = enlace.GetHotelById(reservaEncontrada.IdHotel);

            if (hotel == null)
            {
                MessageBox.Show($"No se pudo obtener la información del hotel (ID: {reservaEncontrada.IdHotel}). Verifica que el hotel exista en la base de datos.",
                    "Error al cargar hotel", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LimpiarCamposCheckIn();
                return;
            }

            // Obtener información del cliente
            Client cliente = enlace.GetClientByRFC(reservaEncontrada.RfcClient);

            // Llenar los campos del formulario
            txtnombreHotel.Text = hotel.name;
            txtNombreCliente.Text = reservaEncontrada.RfcClient;
            txtAnticipo.Text = reservaEncontrada.anticipo.ToString("C");
            txtTotalRestante.Text = reservaEncontrada.total.ToString("C");

            //// Mostrar información adicional en etiquetas o controles nuevos
            //if (lblFechaInicio != null) lblFechaInicio.Text = $"Fecha entrada: {reservaEncontrada.EntryDate.ToDateTimeOffset().Date:dd/MM/yyyy}";
            //if (lblFechaFin != null) lblFechaFin.Text = $"Fecha salida: {reservaEncontrada.EndDate.ToDateTimeOffset().Date:dd/MM/yyyy}";
            //if (lblNumNoches != null) lblNumNoches.Text = $"Noches: {reservaEncontrada.Nights}";
            //if (lblNumPersonas != null) lblNumPersonas.Text = $"Personas: {reservaEncontrada.NumGuests}";


            // Configurar el DataGridView
            dgvDetalles2.Rows.Clear();
            dgvDetalles2.Columns.Clear();

            dgvDetalles2.Columns.Add("hotelid", "hotel");
            dgvDetalles2.Columns.Add("num", "Número de habitación");
            dgvDetalles2.Columns.Add("type", "Tipo de habitación");

            dgvDetalles2.Columns.Add("fecha_inicio", "Fecha Inicio");
            dgvDetalles2.Columns.Add("fecha_fin", "Fecha Fin");
            dgvDetalles2.Columns.Add("num_personas", "Personas");
            dgvDetalles2.Columns.Add("codigo_reserva", "Código Reserva");
            //dgvDetalles2.Columns.Add("type", "Tipo de habitación");
            //dgvDetalles2.Columns.Add("type", "Tipo de habitación");
            //dgvDetalles2.Columns.Add("type", "Tipo de habitación");

            dgvDetalles2.Columns["hotelid"].Visible = false;

            // Establecer anchos de columna
            dgvDetalles2.Columns["num"].Width = 80;
            dgvDetalles2.Columns["type"].Width = 120;
            dgvDetalles2.Columns["fecha_inicio"].Width = 100;
            dgvDetalles2.Columns["fecha_fin"].Width = 100;
            dgvDetalles2.Columns["num_personas"].Width = 80;

            // Formatear columnas de fecha
            dgvDetalles2.Columns["fecha_inicio"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvDetalles2.Columns["fecha_fin"].DefaultCellStyle.Format = "dd/MM/yyyy";

            // Procesar y mostrar las habitaciones
            if (reservaEncontrada.RoomType != null && reservaEncontrada.RoomType.Count > 0)
            {
                foreach (string habitacionInfo in reservaEncontrada.RoomType)
                {
                    if (string.IsNullOrWhiteSpace(habitacionInfo))
                        continue;

                    string numeroHabitacion = "N/A";
                    string tipoHabitacion = "N/A";

                    // El formato esperado es "numero-tipo" (ej: "101-Deluxe")
                    if (habitacionInfo.Contains("-"))
                    {
                        string[] partes = habitacionInfo.Split(new[] { '-' }, 2); // Solo dividir en 2 partes

                        if (partes.Length >= 2)
                        {
                            numeroHabitacion = partes[0].Trim();
                            tipoHabitacion = partes[1].Trim();
                        }
                        else
                        {
                            tipoHabitacion = partes[0].Trim();
                        }
                    }
                    else
                    {
                        tipoHabitacion = habitacionInfo.Trim();
                    }

                    dgvDetalles2.Rows.Add(
                        reservaEncontrada.IdHotel.ToString(),
                        numeroHabitacion,
                        tipoHabitacion,
                reservaEncontrada.EntryDate.ToDateTimeOffset().Date.ToString("yyyy-MM-dd"),
                reservaEncontrada.EndDate.ToDateTimeOffset().Date.ToString("yyyy-MM-dd"),
                reservaEncontrada.NumGuests.ToString(),
                reservaEncontrada.Id.ToString()
                    );
                }
            }
            else
            {
                MessageBox.Show("Esta reservación no tiene habitaciones asignadas.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCheckIn_Click(object sender, EventArgs e)
        {
            if (reservaEncontrada == null)
            {
                MessageBox.Show("Primero debes buscar una reservación.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validar que hoy sea la fecha de entrada
            //DateTime hoy = DateTime.Today;
            //DateTime fechaEntrada = reservaEncontrada.EntryDate.ToDateTimeOffset().Date;
            //if (hoy != fechaEntrada)
            //{
            //    MessageBox.Show($"El Check-In solo se puede realizar en la fecha de entrada: {fechaEntrada:dd/MM/yyyy}.", "Check-In no permitido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
             var confirm = MessageBox.Show("¿Deseas confirmar el Check-In de esta reservación?", "Confirmar Check-In", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
     //       var confirm = MessageBox.Show(
     //    $"¿Confirmar Check-In de esta reservación?\n\n" +
     //    $"Cliente: {txtNombreCliente.Text}\n" +
     //    $"Hotel: {txtnombreHotel.Text}\n" +
     //    $"Fecha: {fechaEntrada:dd/MM/yyyy}\n" +
     //    $"Noches: {reservaEncontrada.Nights}\n" +
     //    $"Habitaciones: {dgvDetalles2.Rows.Count}",
     //    "Confirmar Check-In",
     //    MessageBoxButtons.YesNo,
     //    MessageBoxIcon.Question
     //);
            if (confirm != DialogResult.Yes) return;

            if (enlace.RealizarCheckIn(reservaEncontrada.Id))
            {
                // Actualizar estado en memoria si lo manejas así  
                reservaEncontrada.Status = "CheckIn";

                // Crear string con habitaciones y tipos  
                var detalleHabitaciones = string.Join("\n", reservaEncontrada.RoomType.Select(h =>
                    $"Tipo de Habitación: {h}"));

              
                MessageBox.Show($"Check-In realizado exitosamente.\n\nHabitaciones asignadas:\n{detalleHabitaciones}", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarCamposCheckIn();
            }
            else
            {
                MessageBox.Show("No se pudo realizar el Check-In.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //private void btnCheckIn_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (reservaActual == null)
        //        {
        //            MessageBox.Show("No se ha seleccionado una reservación válida.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            return;
        //        }

        //        // Validar que hoy sea la fecha estimada de entrada
        //        DateTime hoy = DateTime.Now.Date;
        //        DateTime fechaEntrada = reservaActual.FechaEntrada.Date;

        //        if (hoy != fechaEntrada)
        //        {
        //            MessageBox.Show($"Solo se puede hacer el check-in en la fecha de entrada: {fechaEntrada:dd/MM/yyyy}.", "Check-in no permitido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            return;
        //        }

        //        if (ReservacionesDAO.EstaCheckInRealizado(reservaActual.ID_Reserva))
        //        {
        //            MessageBox.Show("Ya se ha realizado el Check-In para esta reservación.", "Check-In ya realizado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            return;
        //        }

        //        // Obtener habitaciones asociadas
        //        List<int> habitaciones = DetalleReservacionDAO.ObtenerHabitacionesPorReservacion(reservaActual.ID_Reserva);

        //        if (habitaciones.Count == 0)
        //        {
        //            MessageBox.Show("No hay habitaciones asociadas a esta reservación.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            return;
        //        }

        //        // Actualizar el estado de cada habitación a "Ocupada"
        //        foreach (int idHab in habitaciones)
        //        {
        //            HabitacionDAO.ActualizarStatusHabitacion(idHab, "Ocupada");
        //        }

        //        using (SqlConnection conn = BDConexion.ObtenerConexion())
        //        {
        //            string query = "UPDATE Reservacion SET CheckInRealizado = 1 WHERE ID_Reserva = @ID_Reserva";
        //            SqlCommand cmd = new SqlCommand(query, conn);
        //            cmd.Parameters.AddWithValue("@ID_Reserva", reservaActual.ID_Reserva);
        //            cmd.ExecuteNonQuery();
        //        }

        //        // (Opcional) podrías cambiar también el estado de la reservación si así lo deseas

        //        MessageBox.Show("Check-in realizado exitosamente.\nLas habitaciones ahora están marcadas como 'Ocupadas'.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

        //        // Limpieza
        //        LimpiarCamposCheckIn();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Ocurrió un error al confirmar el check-in: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        private void LimpiarCamposCheckIn()
        {
            txtBuscar.Clear();
            txtNombreCliente.Clear();
          
            txtAnticipo.Clear();
            txtTotalRestante.Clear();
            
        }

       
    }
}
