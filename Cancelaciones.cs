using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CassandraEnlaceServer;
using Pantallas_alto_volumen_de_datos.DAO;
using Pantallas_alto_volumen_de_datos.Entidades;
using Pantallas_alto_volumen_de_datos.User;

namespace Pantallas_alto_volumen_de_datos
{
    public partial class Cancelaciones : Form
    {
        EnlaceCassandra enlace = new EnlaceCassandra();
        private EmpleadosCassandra SesionUsuario;
        private DateTime DateTimeOp;
        private Reservation reservaEncontrada;


        public Cancelaciones(EmpleadosCassandra usuerConnected, DateTime DateTimeOp)
        {
            InitializeComponent();
            this.SesionUsuario = usuerConnected;
            this.DateTimeOp = DateTimeOp;
        }
        private Reservation reserva;

        private void LimpiarCampos()
        {
            txtBuscar.Text = "";
            txtNombreReserva.Text = "";
          
            txtAnticipo.Text = "";
            txtTotalRestante.Text = "";
            
            reservaEncontrada = null;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void Cancelaciones_Load(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }


        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string selectedForm = cbHotel.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(selectedForm))
            {
                MessageBox.Show("Por favor selecciona un criterio de búsqueda válido.");
                return;
            }
            Guid codigo = Guid.Empty;
            switch (selectedForm)
            {
                case "Codigo de reservación":
                    if (!Guid.TryParse(txtBuscar.Text, out codigo))
                    {
                        MessageBox.Show("El código ingresado no es válido.");
                        return;
                    }
                    reserva = enlace.ObtenerReservacionPorCodigo2(codigo);
                    break;
                case "Nombre":
                    string name = txtBuscar.Text.Trim();
                    reserva = enlace.ObtenerReservacionPorNombre(name);
                    break;
                case "RFC":
                    string rfc = txtBuscar.Text.Trim();
                    reserva = enlace.ObtenerReservacionPorRFC(rfc);
                    break;
                default:
                    MessageBox.Show("Por favor selecciona un criterio de búsqueda válido.");
                    return;
            }

            if (reserva == null)
            {
                MessageBox.Show("No se encontró una reservación con ese código.");
                return;
            }
            reservaEncontrada = reserva;
            string estado = reserva.Status?.Trim().ToLower();

            if (estado == "cancelada")
            {
                MessageBox.Show("Esta reservación ya ha sido cancelada y no puede modificarse.", "Reservación Cancelada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                reserva = null;
                LimpiarCampos();
                return;
            }

            if (estado == "checkin")
            {
                MessageBox.Show("Esta reservación ya tiene Check-In y no puede modificarse.", "Reservación con Check-In", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                reserva = null;
                LimpiarCampos();
                return;
            }

            if (reserva.Status?.ToLower() == "checkout")
            {
                MessageBox.Show("Esta reservación ya tiene Check-Out y no puede modificarse.", "Reservación con Check-In", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                reserva = null;
                //LimpiarCampos();
                return;
            }

            txtNameHotel.Text = enlace.GetHotelById(reserva.IdHotel).name.ToString();
            txtNombreReserva.Text = reserva.RfcClient;
            txtAnticipo.Text = reserva.anticipo.ToString("C");
            txtTotalRestante.Text = reserva.total.ToString("C");
            dtpEntrada.Value = reserva.EntryDate.ToDateTimeOffset().DateTime;
            dtpSalida.Value = reserva.EndDate.ToDateTimeOffset().DateTime;

            dgvHabitaciones.Rows.Clear();
            dgvHabitaciones.Columns.Clear();
            dgvHabitaciones.Columns.Add("hotelid", "hotel");
            dgvHabitaciones.Columns.Add("num", "Numero de habitación");
            dgvHabitaciones.Columns.Add("type", "Tipo de habitación");

            dgvHabitaciones.Columns["hotelid"].Visible = false;

            string numeroHabitacion = string.Empty;
            string tipoHabitacion = string.Empty;
            string reser = string.Empty;
            if (reserva.RoomType.Count > 0)
            {
                foreach (string reservacionRooms in reserva.RoomType)
                {
                    reser = reservacionRooms;
                    numeroHabitacion = reservacionRooms.Split('-')[0];
                    tipoHabitacion = reser.Split('-')[1];

                    dgvHabitaciones.Rows.Add(
                        reserva.IdHotel.ToString(),
                        numeroHabitacion,
                        tipoHabitacion
                    );
                }
            }
        }


        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (reservaEncontrada == null)
            {
                MessageBox.Show("Primero debes buscar una reservación.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Calcular días de diferencia con la fecha de entrada
            DateTime fechaCheckIn = reservaEncontrada.EntryDate.ToDateTimeOffset().DateTime.Date;
            DateTime hoy = DateTime.Today;
            int diasDeDiferencia = (fechaCheckIn - hoy).Days;

            if (diasDeDiferencia >= 3)
            {
                MessageBox.Show("La cancelación se realizó con más de 3 días de anticipación.\nSe devolverá el anticipo al cliente.", "Reembolso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("La cancelación se realizó con menos de 3 días de anticipación.\nNo se realizará ningún reembolso.", "Sin Reembolso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            // Confirmar cancelación
            var confirm = MessageBox.Show("¿Estás seguro de que deseas cancelar esta reservación?", "Confirmar cancelación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            // Ejecutar cancelación
            if (enlace.CancelarReservacion(reservaEncontrada.Id))
            {
                MessageBox.Show("Reservación cancelada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                reservaEncontrada.Status = "cancelada"; // Actualiza el estado en memoria
                LimpiarCampos();
            }
            else
            {
                MessageBox.Show("No se pudo cancelar la reservación.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }







    }
}
