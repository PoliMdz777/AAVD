using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pantallas_alto_volumen_de_datos
{
    class HistorialCliente
    {
        //datos del cliente y su historial de reservas
        public string RFC { get; set; }
        public string NombreCliente { get; set; }
        public string Ciudad { get; set; }
        //datos hotel
        public int IdHotel { get; set; }
        public string Hotel { get; set; }
        public string NombreHotel { get; set; }

        // Datos de la Habitación
        public string TipoHabitacion { get; set; }
        public string NumeroHabitacion { get; set; }
        public int NumPersonasHospedadas { get; set; }
        // Datos de la Reservación
        public Guid CodigoReservacion { get; set; }
        public DateTime FechaReservacion { get; set; }
        public DateTime FechaCheckIn { get; set; }
        public DateTime FechaCheckOut { get; set; }
        public string EstatusReservacion { get; set; }

        // Datos Financieros
        public decimal Anticipo { get; set; }
        public decimal MontoHospedaje { get; set; }
        public decimal MontoServiciosAdicionales { get; set; }
        public decimal TotalFactura { get; set; }
        public decimal Total { get; set; }
    }
}
