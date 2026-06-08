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

namespace Pantallas_alto_volumen_de_datos
{
    public partial class Facturas : Form
    {
        public Facturas()
        {
            InitializeComponent();
            //this.Load += Facturas_Load;
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            string cliente = txtCliente.Text.Trim();
            int? idHotel = cbHotel.SelectedItem != null ? (int?)((Hoteles)cbHotel.SelectedItem).ID_Hotel : null;
            DateTime? fechaInicio = dtpInicio.Value.Date;
            DateTime? fechaFin = dtpFin.Value.Date;

            var facturas = FacturasDAO.ObtenerFacturasFiltradas(cliente, idHotel, fechaInicio, fechaFin);
            dgvFacturas.DataSource = facturas;
        }

        private void Facturas_Load(object sender, EventArgs e)
        {
            //cbHotel.DataSource = HotelesDAO.ObtenerTodosLosHoteles();// Método que devuelva lista de hoteles
            //cbHotel.DisplayMember = "Nombre";
            //cbHotel.ValueMember = "ID_Hotel";
            //CargarTodasLasFacturas();
        }

        private void CargarTodasLasFacturas()
        {
            // Llama al DAO con todos los filtros en null
            DataTable tabla = FacturasDAO.ObtenerFacturasFiltradas("", null, null, null);
            dgvFacturas.DataSource = tabla;
            dgvFacturas.Columns["ID_Factura"].Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
