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
using Pantallas_alto_volumen_de_datos.DAO;
using System.Data.SqlClient;
using Org.BouncyCastle.Math;
using System.Windows.Forms.DataVisualization.Charting;
using Microsoft.IdentityModel.Tokens;
using Cassandra;
using CassandraEnlaceServer;

namespace Pantallas_alto_volumen_de_datos
{
    public partial class RepOcupacion : Form
    {
        EnlaceCassandra enlace = new EnlaceCassandra();

        public RepOcupacion()
        {
            InitializeComponent();
            LlenarComboPaises();
            //CargarPaises();
            CargarAnios();
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


        public static DataTable ObtenerDatosOcupacion(string nombreHotel, int anio)
        {
            DataTable tabla = new DataTable();

            using (SqlConnection conn = BDConexion.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand("SP_ReporteOcupacionHotel", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Puedes dejar NULL los parámetros que no estés filtrando
                    cmd.Parameters.AddWithValue("@Pais", DBNull.Value);
                    cmd.Parameters.AddWithValue("@Ciudad", DBNull.Value);
                    cmd.Parameters.AddWithValue("@Hotel", nombreHotel);
                    cmd.Parameters.AddWithValue("@Anio", anio);
                    cmd.Parameters.AddWithValue("@Mes", DBNull.Value);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(tabla);
                }
            }

            return tabla;
        }



        //private DataTable ObtenerDatosOcupacion(string hotel, int anio)
        //{
        //    using (SqlConnection conn = BDConexion.ObtenerConexion())
        //    {
        //        SqlCommand cmd = new SqlCommand("SP_ReporteOcupacionHotel", conn);
        //        cmd.CommandType = CommandType.StoredProcedure;

        //        cmd.Parameters.AddWithValue("@Hotel", hotel);
        //        cmd.Parameters.AddWithValue("@Anio", anio);

        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        DataTable dt = new DataTable();
        //        da.Fill(dt);

        //        return dt;
        //    }
        //}


        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            string hotel = cbHotel.SelectedItem?.ToString();
            string pais = cbPais.SelectedItem?.ToString();
            string ciudad = cbCiudad.SelectedItem?.ToString();
            string anioTexto = cbAnio.SelectedItem?.ToString();

            // Convierte "Todos" en null
            pais = pais == "Todos" ? null : pais;
            ciudad = ciudad == "Todos" ? null : ciudad;
            hotel = hotel == "Todos" ? null : hotel;
            int? anio = (anioTexto == "Todos" || string.IsNullOrEmpty(anioTexto)) ? (int?)null : Convert.ToInt32(anioTexto);

            if (cbHotel.SelectedItem == null || cbAnio.SelectedItem == null)
            {
                MessageBox.Show("Por favor selecciona hotel, año y mes.", "Campos requeridos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Hotel hotelSeleccionado = cbHotel.SelectedItem as Hotel;
            int year = int.Parse(cbAnio.SelectedItem.ToString());


            var reporte = enlace.ObtenerReporteOcupacion(hotelSeleccionado.id, year);

            dgvOcupacion.DataSource = null;
            dgvOcupacion.DataSource = reporte;

            dataGridViewResumen.DataSource = null;
            dataGridViewResumen.DataSource = reporte
                .Select(r => new { r.NombreHotel, r.PorcentajeOcupacion })
                .ToList();

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

        

        private void dataGridViewResumen_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.RowIndex >= 0)
            //{
            //    string hotel = dataGridViewResumen.Rows[e.RowIndex].Cells["NombreHotel"].Value.ToString();
            //    int anio = Convert.ToInt32(dataGridViewResumen.Rows[e.RowIndex].Cells["Anio"].Value);

            //    lblHotelSeleccionado.Text = $"Hotel: {hotel} (Año: {anio})";

            //    // Usa el mismo método de obtención
            //    DataTable datos = ObtenerDatosOcupacion(hotel, anio);

            //    chartRepOcupacion.Series.Clear();
            //    Series serie = new Series("Ocupación Anual");
            //    serie.ChartType = SeriesChartType.Column;
            //    serie.Color = Color.SkyBlue;

            //    foreach (DataRow row in datos.Rows)
            //    {
            //        int mes = Convert.ToInt32(row["Mes"]);
            //        decimal porcentaje = Convert.ToDecimal(row["PorcentajeOcupacion"]);
            //        serie.Points.AddXY(mes, porcentaje);
            //    }

            //    chartRepOcupacion.Series.Add(serie);
            //}
        }

        private void btnMostrarGrafico_Click(object sender, EventArgs e)
        {
            if (dgvOcupacion.Rows.Count == 0)
            {
                MessageBox.Show("No hay datos para mostrar en la gráfica.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            chartRepOcupacion.Series.Clear();
            Series serie = new Series("Ocupación")
            {
                ChartType = SeriesChartType.Column,
                IsValueShownAsLabel = true
            };

            foreach (DataGridViewRow row in dgvOcupacion.Rows)
            {
                string nombreHotel = row.Cells["NombreHotel"].Value.ToString();
                double porcentaje = Convert.ToDouble(row.Cells["PorcentajeOcupacion"].Value);
                serie.Points.AddXY(nombreHotel, porcentaje);
            }

            chartRepOcupacion.Series.Add(serie);
            chartRepOcupacion.ChartAreas[0].RecalculateAxesScale();
        }

        private void LlenarComboPaises()
        {
            List<string> paises = enlace.GetAllCountryFromHotels();

            cbPais.Items.Clear();
            cbPais.Items.AddRange(paises.ToArray());
        }
    }
}
