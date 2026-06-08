using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pantallas_alto_volumen_de_datos
{
    class ReporteOcupacion
    {
        public int HotelId { get; set; }
        public string NombreHotel { get; set; }
        public int TotalHabitaciones { get; set; }
        public int HabitacionesOcupadas { get; set; }
        public double PorcentajeOcupacion { get; set; }
        public string NumeroHabitacion { get; set; }
        public string Cliente { get; set; }
        public Guid CodigoReservacion { get; set; }
        public DateTime FechaCheckIn { get; set; }
        public DateTime FechaCheckOut { get; set; }
        public string EstatusReservacion { get; set; }
        public string TipoHabitacion { get; set; }
        public decimal PagoHospedaje { get; set; }
        public decimal PagoServicios { get; set; }
    }
}
