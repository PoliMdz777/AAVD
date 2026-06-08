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
using System.Data.SqlClient;
using CassandraEnlaceServer;

namespace Pantallas_alto_volumen_de_datos
{
    public partial class HistorialCli : Form
    {
        EnlaceCassandra enlace = new EnlaceCassandra();
        public HistorialCli()
        {
            InitializeComponent();
            ConfigurarDataGridView();
           // CargarAnios();
        }

        private void ConfigurarDataGridView()
        {
            dgvHistorialCliente.AutoGenerateColumns = false;
            dgvHistorialCliente.AllowUserToAddRows = false;
            dgvHistorialCliente.ReadOnly = true;
            dgvHistorialCliente.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // Configurar columnas
            dgvHistorialCliente.Columns.Clear();

            // Datos del Cliente
            dgvHistorialCliente.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "NombreCliente",
                HeaderText = "Nombre Cliente",
                Width = 150
            });

            dgvHistorialCliente.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Ciudad",
                HeaderText = "Ciudad",
                Width = 100
            });

            // Datos del Hotel
            dgvHistorialCliente.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "NombreHotel",
                HeaderText = "Nombre Hotel",
                Width = 150
            });

            // Datos de Habitación
            dgvHistorialCliente.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TipoHabitacion",
                HeaderText = "Tipo Habitación",
                Width = 120
            });

            dgvHistorialCliente.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "NumeroHabitacion",
                HeaderText = "Núm. Habitación",
                Width = 100
            });

            dgvHistorialCliente.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "NumPersonasHospedadas",
                HeaderText = "Personas",
                Width = 80
            });

            // Datos de Reservación
            dgvHistorialCliente.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "CodigoReservacion",
                HeaderText = "Código Reservación",
                Width = 250
            });

            dgvHistorialCliente.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "FechaReservacion",
                HeaderText = "Fecha Reservación",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" }
            });

            dgvHistorialCliente.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "FechaCheckIn",
                HeaderText = "Check In",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" }
            });

            dgvHistorialCliente.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "FechaCheckOut",
                HeaderText = "Check Out",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" }
            });

            dgvHistorialCliente.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "EstatusReservacion",
                HeaderText = "Estatus",
                Width = 100
            });

            // Datos Financieros
            dgvHistorialCliente.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Anticipo",
                HeaderText = "Anticipo",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" }
            });

            dgvHistorialCliente.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "MontoHospedaje",
                HeaderText = "Monto Hospedaje",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" }
            });

            dgvHistorialCliente.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "MontoServiciosAdicionales",
                HeaderText = "Servicios Adicionales",
                Width = 140,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" }
            });

            dgvHistorialCliente.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TotalFactura",
                HeaderText = "Total Factura",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "C2",
                    BackColor = System.Drawing.Color.LightYellow,
                    Font = new System.Drawing.Font("Arial", 9, System.Drawing.FontStyle.Bold)
                }
            });
        }


        //private void CargarAnios()
        //{
        //    HashSet<int> yearsHotel = enlace.GetAllYearsFromHotels();

        //    cbAnio.Items.Clear();
        //    cbAnio.Items.AddRange(yearsHotel.OrderBy(a => a).Select(a => a.ToString()).ToArray());
        //}


        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            string rfc = txtRFC.Text.Trim();
            string anioTexto = txtAnio.Text.Trim();

            if (string.IsNullOrEmpty(rfc) || string.IsNullOrEmpty(anioTexto))
            {
                MessageBox.Show("Por favor ingresa el RFC y selecciona un año.");
                return;
            }

            if (!int.TryParse(anioTexto, out int anio))
            {
                MessageBox.Show("El año debe ser un número válido.");
                return;
            }

         //   var historial = enlace.ObtenerHistorialCliente(rfc, anio);
            var historial = enlace.ObtenerHistorialClienteCompleto(rfc, anio);
            //if (historial.Count == 0)
            //{
            //    MessageBox.Show("No se encontraron registros para el RFC y año especificados.",
            //        "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            dgvHistorialCliente.DataSource = historial;
            dgvHistorialCliente.AllowUserToAddRows = false;

        }

        //private void btnFiltrar_Click(object sender, EventArgs e)
        //{
        //    string rfc = txtRFC.Text.Trim();
        //    string anioTexto = txtAnio.SelectedItem?.ToString();

        //    if (string.IsNullOrEmpty(rfc) || string.IsNullOrEmpty(anioTexto))
        //    {
        //        MessageBox.Show("Por favor ingresa el RFC y selecciona un año.");
        //        return;
        //    }

        //    //  No conviertas a int aquí
        //    DataTable historial = HistorialClienteDAO.ObtenerHistorialCliente(rfc, anioTexto);
        //    dgvHistorialCliente.DataSource = historial;

        //    dgvHistorialCliente.AllowUserToAddRows = false;
        //}
    }
}
