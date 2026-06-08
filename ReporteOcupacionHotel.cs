using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pantallas_alto_volumen_de_datos.Entidades
{
    namespace Pantallas_alto_volumen_de_datos.Entidades
    {
        public class ReporteOcupacionHotel
        {
            public string Ciudad { get; set; }
            public string NombreHotel { get; set; }
            public int Anio { get; set; }
            public int Mes { get; set; }
            public string TipoHabitacion { get; set; }
            public int CantidadReservas { get; set; }
            public int CantidadHabitaciones { get; set; }
            public int CantidadPersonas { get; set; }
            public decimal PorcentajeOcupacion { get; set; }
        }
    }

}
