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
using System.Windows.Forms.DataVisualization.Charting;
using System.Data.SqlClient;
using CassandraEnlaceServer;

namespace Pantallas_alto_volumen_de_datos
{
    public partial class RepVentas : Form
    {
        EnlaceCassandra enlace = new EnlaceCassandra();

        public RepVentas()
        {
            InitializeComponent();
            CargarPaises();
            CargarAnios();
        }
        private void CargarPaises()
        {
            List<string> paises = enlace.GetAllCountryFromHotels();

            cbPais.Items.Clear();
            cbPais.Items.AddRange(paises.ToArray());
        }


        private void CargarCiudadesPorPais(string pais)
        {
            string paisSeleccionado = cbPais.SelectedItem.ToString();
            var ciudades = enlace.GetAllCitiesFromHotels(paisSeleccionado);

            cbCiudad.Items.Clear();
            cbCiudad.Items.AddRange(ciudades.ToArray());
        }


        private void CargarHotelesPorCiudad(string ciudad)
        {
            string ciudadSeleccionada = cbCiudad.SelectedItem?.ToString();
            string paisSeleccionado = cbPais.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(ciudadSeleccionada) || string.IsNullOrEmpty(paisSeleccionado))
                return;

            List<Hotel> hoteles = enlace.GetAllHotelesFromCity(paisSeleccionado, ciudadSeleccionada);

            cbHotel.DataSource = null;
            cbHotel.DataSource = hoteles;
            cbHotel.DisplayMember = "name";
            cbHotel.ValueMember = "id";
        }

        private void CargarAnios()
        {
            HashSet<int> yearsHotel = enlace.GetAllYearsFromHotels();

            cbAnio.Items.Clear();
            cbAnio.Items.AddRange(yearsHotel.OrderBy(a => a).Select(a => a.ToString()).ToArray());
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            string pais = cbPais.SelectedItem?.ToString();
            string ciudad = cbCiudad.SelectedItem?.ToString();
            string hotel = (cbHotel.SelectedItem as Hotel)?.name;
            string anioTexto = cbAnio.SelectedItem?.ToString();

            pais = pais == "Todos" ? null : pais;
            ciudad = ciudad == "Todos" ? null : ciudad;
            hotel = hotel == "Todos" ? null : hotel;
            int? anio = (anioTexto == "Todos" || string.IsNullOrEmpty(anioTexto)) ? (int?)null : Convert.ToInt32(anioTexto);

            //Hotel hotelFound = 

            var datos = ObtenerReporteIngresos(pais, ciudad, null, hotel, anio);

            dgvVentas.DataSource = null;
            dgvVentas.DataSource = datos;
        }

        private void cbPais_SelectedIndexChanged(object sender, EventArgs e)
        {
            string paisSeleccionado = cbPais.SelectedItem.ToString();
            if (paisSeleccionado != "Todos")
                CargarCiudadesPorPais(paisSeleccionado);
        }

        private void cbCiudad_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ciudadSeleccionada = cbCiudad.SelectedItem.ToString();
            if (ciudadSeleccionada != "Todos")
                CargarHotelesPorCiudad(ciudadSeleccionada);
        }

        private void btnMostrarGrafico_Click(object sender, EventArgs e)
        {
            if (dgvVentas.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecciona al menos un hotel para graficar.");
                return;
            }

            GraficarIngresosPorHotel();
        }

        public List<ReporteIngresos> ObtenerReporteIngresos(string pais, string ciudad, string state, string hotelNombre, int? anio)
        {
            var hoteles = enlace.GetHotelesFiltrados(pais, ciudad, state,hotelNombre);
            var reporte = new List<ReporteIngresos>();

            foreach (var hotel in hoteles)
            {
                var reservaciones = enlace.GetReservacionesPorHotelYAnio(hotel.id, anio);

                var agrupado = reservaciones
                    .GroupBy(r => new { r.EntryDate.Year, r.EndDate.Month })
                    .Select(g =>
                    {
                        decimal hospedaje = g.Sum(r => r.total);
                        decimal servicios = g.Sum(r => enlace.GetTotalServiciosPorReserva(r.Id));

                        return new ReporteIngresos
                        {
                            Ciudad = hotel.city,
                            Hotel = hotel.name,
                            Anio = g.Key.Year,
                            Mes = g.Key.Month,
                            IngresosHospedaje = hospedaje,
                            IngresosServiciosAdicionales = servicios,
                            IngresosTotales = hospedaje + servicios
                        };
                    });

                reporte.AddRange(agrupado);
            }

            return reporte;
        }


        private void GraficarIngresosPorHotel()
        {
            chartVentas.Series.Clear();

            var serie = new System.Windows.Forms.DataVisualization.Charting.Series("Ingresos");
            serie.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

            foreach (DataGridViewRow fila in dgvVentas.SelectedRows)
            {
                string hotel = fila.Cells["Hotel"].Value.ToString();
                decimal ingresos = Convert.ToDecimal(fila.Cells["IngresosTotales"].Value);

                serie.Points.AddXY(hotel, ingresos);
            }

            chartVentas.Series.Add(serie);

            // Opcional: títulos para ejes
            chartVentas.ChartAreas[0].AxisX.Title = "Hotel";
            chartVentas.ChartAreas[0].AxisY.Title = "Ingresos Totales ($)";
        }
    }
}
