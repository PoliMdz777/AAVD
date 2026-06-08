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
using static Pantallas_alto_volumen_de_datos.DAO.HotelesDAO;
using System.Text.RegularExpressions;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using CassandraEnlaceServer;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;
using Cassandra;

namespace Pantallas_alto_volumen_de_datos
{
    public partial class CheckOut : Form
    {
        private Reservation reservaCheckout = null;
        EnlaceCassandra enlace = new EnlaceCassandra();
        public CheckOut()
        {

            InitializeComponent();
            //CargarServicios();
            //dgvServicios.DataSource = ServiciosAdicionalesDAO.ObtenerTodosLosServicios();
         
        }
        //private void CargarServicios()
        //{
        //    // Obtenemos toda la lista con precios
        //    List<ServicioAdicionales> servicios = ServiciosAdicionalesDAO.ObtenerTodosLosServicios();

        //    // Limpiamos y cargamos el CheckedListBox
        //    CLBServicios.Items.Clear();

        //    foreach (var servicio in servicios)
        //    {
        //        CLBServicios.Items.Add(servicio); 
        //    }
        //}

      
        

       

        private decimal ObtenerTotal()
        {
            decimal total = 0;

            foreach (var item in CLBServicios.CheckedItems)
            {
                ServicioAdicionales servicio = (ServicioAdicionales)item;
                total += servicio.Precio;
            }

            return total * 1.18m; // Total con IVA
        }
        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        //private void btnbuscar_Click(object sender, EventArgs e)
        //{
        //    Guid codigo;
        //    if (Guid.TryParse(txtCodigo.Text, out codigo))
        //    {
        //        string nombreHotel;
        //        reservaActual = ReservacionesDAO.ObtenerReservacionPorCodigo(codigo, out nombreHotel);

        //        if (reservaActual != null)
        //        {
        //            // Mostrar datos en controles
        //            txtNombreCliente.Text = reservaActual.NomCli;
        //            txtNomHotel.Text = nombreHotel;

        //            // Cargar detalles al DataGridView
        //            var detalles = ReservacionesDAO.ObtenerDetallesPorIDReserva(reservaActual.ID_Reserva);
        //            dgvDetalles2.DataSource = detalles;
        //        }
        //        else
        //        {
        //            MessageBox.Show("Reservación no encontrada.");
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("El código ingresado no es válido.");
        //    }
        //}


        private void btnbuscar_Click(object sender, EventArgs e)
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
                    if (!Guid.TryParse(txtCodigo.Text, out codigo))
                    {
                        MessageBox.Show("El código ingresado no es válido.");
                        return;
                    }
                    reservaCheckout = enlace.ObtenerReservacionPorCodigo2(codigo);
                    break;
                case "Nombre":
                    string name = txtCodigo.Text.Trim();
                    reservaCheckout = enlace.ObtenerReservacionPorNombre(name);
                    break;
                case "RFC":
                    string rfc = txtCodigo.Text.Trim();
                    reservaCheckout = enlace.ObtenerReservacionPorRFC(rfc);
                    break;
                default:
                    MessageBox.Show("Por favor selecciona un criterio de búsqueda válido.");
                    return;
            }


            // Mostrar el RFC del cliente en el TextBox correspondiente
            txtNombreCliente.Text = reservaCheckout.RfcClient;
            txtNameHotel.Text = enlace.GetHotelById(Convert.ToInt32(reservaCheckout.IdHotel)).name;

            dgvDetalles2.Rows.Clear();
            dgvDetalles2.Columns.Clear();

            dgvDetalles2.Columns.Add("hotelid", "hotel");
            dgvDetalles2.Columns.Add("num", "Numero de habitación");
            dgvDetalles2.Columns.Add("type", "Tipo de habitación");
            dgvDetalles2.Columns.Add("price", "Precio por noche");
            dgvDetalles2.Columns.Add("persons", "Número de personas");
            dgvDetalles2.Columns.Add("total", "Total por habitación");

            dgvDetalles2.Columns["hotelid"].Visible = false;

            // Obtener información detallada de los tipos de habitación
            List<roomTypes> tiposHabitacionHotel = enlace.ObtenerTiposHabitacionPorHotel(reservaCheckout.IdHotel);
            // Procesar cada habitación de la reserva
            if (reservaCheckout.RoomType != null && reservaCheckout.RoomType.Count > 0)
            {
                // Calcular precio por noche de cada tipo de habitación
                decimal precioPorNoche = 0;
                if (reservaCheckout.total > 0 && reservaCheckout.Nights > 0)
                {
                    // Dividir el total entre el número de noches para obtener precio por noche
                    precioPorNoche = reservaCheckout.total / reservaCheckout.Nights;

                    // Dividir entre el número de habitaciones si hay más de una
                    if (reservaCheckout.NumRooms > 1)
                    {
                        precioPorNoche = precioPorNoche / reservaCheckout.NumRooms;
                    }
                }
                // Calcular personas por habitación
                int personasPorHabitacion = 0;
                if (reservaCheckout.NumGuests > 0 && reservaCheckout.NumRooms > 0)
                {
                    personasPorHabitacion = reservaCheckout.NumGuests / reservaCheckout.NumRooms;
                }

                foreach (string reservacionRooms in reservaCheckout.RoomType)
                {
                    string[] partes = reservacionRooms.Split('-');
                    string numeroHabitacion = partes.Length > 0 ? partes[0] : "N/A";
                    string tipoHabitacion = partes.Length > 1 ? partes[1] : "N/A";

                    // Buscar información específica del tipo de habitación
                    decimal precioEspecifico = precioPorNoche;
                    int maxPersonas = personasPorHabitacion;

                    // Intentar encontrar el tipo de habitación en la lista para obtener detalles específicos
                    var tipoHabitacionInfo = tiposHabitacionHotel.FirstOrDefault(t => t.name == tipoHabitacion);
                    if (tipoHabitacionInfo != null)
                    {
                        // Usar el precio real del tipo de habitación si está disponible
                        precioEspecifico = tipoHabitacionInfo.priceNight;
                        maxPersonas = tipoHabitacionInfo.maxGuests;
                    }

                    // Calcular total por esta habitación
                    decimal totalHabitacion = precioEspecifico * reservaCheckout.Nights;

                    dgvDetalles2.Rows.Add(
                reservaCheckout.IdHotel.ToString(),
                numeroHabitacion,
                tipoHabitacion,
                precioEspecifico.ToString("C"),
                maxPersonas.ToString(),
                totalHabitacion.ToString("C")
                );
                }
                //    string numeroHabitacion = string.Empty;
                //string tipoHabitacion = string.Empty;
                //string reser = string.Empty;
                //if (reservaCheckout.RoomType.Count > 0)
                //{
                //    foreach (string reservacionRooms in reservaCheckout.RoomType)
                //    {
                //        reser = reservacionRooms;
                //        numeroHabitacion = reservacionRooms.Split('-')[0];
                //        tipoHabitacion = reser.Split('-')[1];

                //        dgvDetalles2.Rows.Add(
                //            reservaCheckout.IdHotel.ToString(),
                //            numeroHabitacion,
                //            tipoHabitacion
                //        );
                //    }

                // Agregar una fila de totales si hay múltiples habitaciones
                if (reservaCheckout.NumRooms > 1)
                {
                    dgvDetalles2.Rows.Add(
                        "",
                        "TOTAL",
                        "",
                        (precioPorNoche * reservaCheckout.NumRooms).ToString("C"),
                        reservaCheckout.NumGuests.ToString(),
                        reservaCheckout.total.ToString("C")
                    );

                    // Dar formato especial a la fila de totales
                    if (dgvDetalles2.Rows.Count > 0)
                    {
                        DataGridViewRow totalRow = dgvDetalles2.Rows[dgvDetalles2.Rows.Count - 1];
                        //totalRow.DefaultCellStyle.Font = new Font(dgvDetalles2.Font, FontStyle.Bold);
                        totalRow.DefaultCellStyle.BackColor = Color.LightGray;
                    }
                }
            
            }


            // Obtener los servicios adicionales del hotel al que pertenece la reservación
            List<string> servicios = enlace.ObtenerServiciosAdicionalesPorHotel(reservaCheckout.IdHotel);

            // Limpiar y llenar el CheckedListBox con los servicios encontrados
            CLBServicios.Items.Clear();
            foreach (var servicio in servicios)
            {
                CLBServicios.Items.Add(servicio);
            }
        }


        private void LimpiarCamposCheckOut()
        {
            // Limpiar texto
            txtCodigo.Text = string.Empty;
            txtNombreCliente.Text = string.Empty;
            

            txtTotalServicios.Text = string.Empty;

            

            CLBServicios.Items.Clear();
            // Limpiar DataGridView
            

            
        }


        //private void btnbuscar_Click(object sender, EventArgs e)
        //{
        //    string entrada = txtCodigo.Text.Trim();

        //    if (string.IsNullOrEmpty(entrada))
        //    {
        //        MessageBox.Show("Por favor ingresa un RFC o código de reservación.");
        //        return;
        //    }

        //    string nombreHotel, ciudad;
        //    reservaActual = ReservacionesDAO.BuscarPorRFCoCodigo(entrada, out nombreHotel, out ciudad);

        //    if (reservaActual != null)
        //    {
        //        txtNombreCliente.Text = reservaActual.NomCli;
        //        txtNomHotel.Text = nombreHotel;

        //        var detalles = ReservacionesDAO.ObtenerDetallesPorIDReserva(reservaActual.ID_Reserva);
        //        dgvDetalles2.DataSource = detalles;

        //        int idHotel = reservaActual.ID_Hotel;
        //        var servicios = ServiciosAdicionalesDAO.ObtenerServiciosPorHotel(idHotel);

        //        CLBServicios.Items.Clear();
        //        foreach (var servicio in servicios)
        //        {
        //            CLBServicios.Items.Add(servicio);
        //        }

        //        dgvServicios.DataSource = servicios;
        //    }
        //    else
        //    {
        //        MessageBox.Show("No se encontró ninguna reservación con ese código o RFC.");
        //    }
        //}


        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnCalcularSA_Click(object sender, EventArgs e)
        {
            decimal total = 0;

            foreach (var item in CLBServicios.CheckedItems)
            {
                additionalService Servicio = enlace.GetAdditionalServiceByName(item.ToString());
                total += Servicio.price;
            }

            // Aplicar IVA del 18%
            decimal totalConIVA = total * 1.18m;

            txtTotalServicios.Text = totalConIVA.ToString("C");
        }

        
        private void btnConfirmar_Click(object sender, EventArgs e)
        {

            string codigoReserva = txtCodigo.Text.Trim();

            if (string.IsNullOrEmpty(codigoReserva))
            {
                MessageBox.Show("Por favor ingresa el código de la reservación.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Usando Guid.Parse (lanza excepción si el formato es inválido)

            // Validar que sea un GUID válido
            if (!Guid.TryParse(codigoReserva, out Guid guid))
            {
                MessageBox.Show("El código de reservación no es válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            Reservation reserva = enlace.ObtenerReservacionPorCodigo2(guid);
            if (reserva == null)
            {
                MessageBox.Show("No se encontró la reservación.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validar que la reservación tenga Check-In
            if (reserva.Status?.ToLower() != "checkin")
            {
                MessageBox.Show("Solo se puede hacer Check-Out a reservaciones con Check-In realizado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Procesar total de servicios
            decimal totalServicios = 0;
            string totalServiciosTexto = txtTotalServicios.Text.Replace("$", "").Replace(",", "").Trim();
            decimal.TryParse(totalServiciosTexto, out totalServicios);

            string carpetaFacturas = @"C:\Facturas";

            if (!Directory.Exists(carpetaFacturas))
            {
                Directory.CreateDirectory(carpetaFacturas);
            }

            // Generar nombre único para el PDF
            Guid idFactura = TimeUuid.NewId();
            string nombreArchivo = $"Factura_{idFactura}.pdf";
            string rutaPDF = Path.Combine(carpetaFacturas, nombreArchivo);

            //decimal variable = 0;

            //string prueba = txtTotalServicios.Text.ToString().Replace("$", "").Replace(",", "").Trim();

            //decimal.TryParse(prueba, out variable);

            CHECKOUT2 obj = new CHECKOUT2
            {
                ID_factura = idFactura,
                ID_Reserva = reserva.Id,
                ID_Hotel = reserva.IdHotel,
                Fecha_Check_Out = reserva.CheckOut,
                RFCliente = reserva.RfcClient,
                Habitaciones = reserva.RoomType,
                Total = reserva.total,
                Anticipo = reserva.anticipo,
                Formadepago = reserva.PayForm,
                Servicios = CLBServicios.CheckedItems.Cast<string>().ToList(),
                TotalServicios = totalServicios,
                FormapagoServicios = txtFormaPago.Text,
                TotalCobrado = reserva.total + totalServicios,
                RUTAPDF = rutaPDF  // ← AHORA SÍ EXISTE

            };

            bool resultado = enlace.GuardarCheckOutCompleto(obj);

            if (resultado)
            {
                // Generar el PDF
                try
                {
                    GenerarFacturaPDF(obj, rutaPDF);
                    MessageBox.Show($"Check-out registrado correctamente.\n\nFactura guardada en:\n{rutaPDF}",
                        "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Check-out guardado pero hubo un error al generar el PDF:\n{ex.Message}",
                        "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                LimpiarCamposCheckOut();
            }
            else
            {
                MessageBox.Show("No se pudo completar el Check-Out. Revisa los datos e intenta nuevamente.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          

        }

        public void GenerarFacturaPDF(CHECKOUT2 factura, string rutaPDF)
        {
            Document doc = new Document();
            PdfWriter.GetInstance(doc, new FileStream(rutaPDF, FileMode.Create));
            doc.Open();

            // Título y encabezado
            doc.Add(new Paragraph("Factura de Check-Out", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16)));
            doc.Add(new Paragraph("Fecha: " + DateTime.Now.ToString("dd/MM/yyyy")));
            doc.Add(new Paragraph(" "));

            // Información general
            doc.Add(new Paragraph($"ID Factura: {factura.ID_factura}"));
            doc.Add(new Paragraph($"ID Reserva: {factura.ID_Reserva}"));
            doc.Add(new Paragraph($"Hotel: {enlace.GetHotelById(Convert.ToInt32(factura.ID_Hotel)).name}"));
            doc.Add(new Paragraph($"RFC Cliente: {factura.RFCliente}"));
            doc.Add(new Paragraph($"Fecha Check-Out: {factura.Fecha_Check_Out}"));
            doc.Add(new Paragraph(" "));

            // Tabla de habitaciones
            doc.Add(new Paragraph("Habitaciones:", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12)));
            PdfPTable tablaHabitaciones = new PdfPTable(1);
            tablaHabitaciones.WidthPercentage = 80;

            foreach (string habitacion in factura.Habitaciones)
            {
                PdfPCell celda = new PdfPCell(new Phrase(habitacion));
                celda.HorizontalAlignment = Element.ALIGN_LEFT;
                tablaHabitaciones.AddCell(celda);
            }
            doc.Add(tablaHabitaciones);
            doc.Add(new Paragraph(" "));

            // Tabla de servicios
            doc.Add(new Paragraph("Servicios adicionales:", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12)));
            PdfPTable tablaServicios = new PdfPTable(1);
            tablaServicios.WidthPercentage = 80;

            foreach (string servicio in factura.Servicios)
            {
                PdfPCell celda = new PdfPCell(new Phrase(servicio));
                celda.HorizontalAlignment = Element.ALIGN_LEFT;
                tablaServicios.AddCell(celda);
            }
            doc.Add(tablaServicios);
            doc.Add(new Paragraph(" "));

            // Totales
            doc.Add(new Paragraph($"Total habitación: ${factura.Total:F2}"));
            doc.Add(new Paragraph($"Anticipo: ${factura.Anticipo:F2}"));
            doc.Add(new Paragraph($"Forma de pago habitación: {factura.Formadepago}"));
            doc.Add(new Paragraph($"Total servicios: ${factura.TotalServicios:F2}"));
            doc.Add(new Paragraph($"Forma de pago servicios: {factura.FormapagoServicios}"));
            doc.Add(new Paragraph($"Total cobrado: ${factura.TotalCobrado:F2}"));

            doc.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }


        
    }
}
