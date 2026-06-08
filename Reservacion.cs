using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pantallas_alto_volumen_de_datos.Entidades
{
    public class Reservacion
    {
        public int ID_Reserva { get; set; }
        public string FormaPago { get; set; }
        public int ID_Hotel { get; set; }
        public int ID_Habitacion { get; set; }
        public DateTime FechaEntrada { get; set; }
        public DateTime FechaSalida { get; set; }
        public string ClienteRFC { get; set; }
        public string NomCli { get; set; }
        public decimal Total { get; set; }
        public decimal Anticipo { get; set; }
        public Guid CodigoReservacion { get; set; } = Guid.NewGuid(); // genera uno automático
        public DateTime FechaRegistro { get; set; }
        public string UsuarioRegistro { get; set; }
        public string Ciudad { get; set; }
        public string Estado { get; set; }

        public Reservacion() { }

        public Reservacion(int ID_Reserva, string FormaPago, int ID_Hotel, int ID_Habitacion, DateTime FechaEntrada, DateTime FechaSalida, string ClienteRFC, string NomCli, decimal Total, decimal Anticipo, Guid CodigoReservacion, DateTime FechaRegistro, string UsuarioRegistro, string ciudad, string estado)
        {
            this.ID_Reserva = ID_Reserva;
            this.FormaPago = FormaPago;
            this.ID_Hotel = ID_Hotel;
            this.ID_Habitacion = ID_Habitacion;
            this.FechaEntrada = FechaEntrada;
            this.FechaSalida = FechaSalida;
            this.ClienteRFC = ClienteRFC;
            this.NomCli = NomCli;
            this.Total = Total;
            this.Anticipo = Anticipo;
            this.CodigoReservacion = CodigoReservacion;
            this.FechaRegistro = FechaRegistro;
            this.UsuarioRegistro = UsuarioRegistro;
            this.Ciudad = ciudad;
            this.Estado = estado;
        }
    }
}
