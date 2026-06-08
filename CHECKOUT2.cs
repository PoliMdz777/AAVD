using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pantallas_alto_volumen_de_datos
{
    public class CHECKOUT2
    {
        public Guid ID_factura { get; set; }
        public Guid ID_Reserva { get; set; }
        public int ID_Hotel { get; set; }
        public DateTime Fecha_Check_Out { get; set; }
        public string RFCliente { get; set; }
        public List<string> Habitaciones { get; set; } = new List<string>();
        public decimal Total { get; set; }
        public decimal Anticipo { get; set; }
        public string Formadepago { get; set; }
        public List<string> Servicios { get; set; } = new List<string>();
        public decimal TotalServicios { get; set; }
        public string FormapagoServicios { get; set; }
        public decimal TotalCobrado { get; set; }
        public string RUTAPDF { get; set; }
    }

}
