using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pantallas_alto_volumen_de_datos.Entidades
{
    public class ReporteVentas
    {
        public string Ciudad { get; set; }
        public string Hotel { get; set; }
        public int Anio { get; set; }
        public int Mes { get; set; }
        public decimal IngresosHospedaje { get; set; }
        public decimal IngresosServiciosAdicionales { get; set; }
        public decimal IngresosTotales => IngresosHospedaje + IngresosServiciosAdicionales;
    }

}
