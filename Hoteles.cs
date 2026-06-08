using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pantallas_alto_volumen_de_datos.Entidades
{
    public class Hoteles
    {

        public int ID_Hotel { get; set; }
        public string CodigoSAT { get; set; }
        public string Nombre { get; set; }
        public string UnidadServicio { get; set; }
        public int NumPisos { get; set; }
        public string Vista { get; set; }
        public int Piscina { get; set; }
        public bool SalonEventos { get; set; }
        public DateTime FechaInicioOps { get; set; }
        public bool ZonaTuristica { get; set; }
        public string Domicilio { get; set; }
        public string Usuario { get; set; }
        public DateTime FechaHoraRegistro { get; set; }
        public string Ciudad { get; set; }
        public string Estado { get; set; }
        public string Pais { get; set; }
        public bool Activo { get; set; }
        public Hoteles() { }

        public Hoteles(int ID_Hotel, string CodigoSAT, string Nombre, string UnidadServicio,int NumPisos,string Vista, int Piscina, bool SalonEventos, DateTime FechaInicioOps,
            bool ZonaTuristica, string Domicilio, string Usuario, DateTime FechaHoraRegistro, string Ciudad, string Estado, string Pais, bool Activo)
        {
            this.ID_Hotel = ID_Hotel;
            this.CodigoSAT = CodigoSAT;
            this.Nombre = Nombre;
            this.UnidadServicio = UnidadServicio;
            this.NumPisos = NumPisos;
            this.Vista = Vista;
            this.Piscina = Piscina;
            this.SalonEventos = SalonEventos;
            this.FechaInicioOps = FechaInicioOps;
            this.ZonaTuristica = ZonaTuristica;
            this.Domicilio = Domicilio;
            this.Usuario = Usuario;
            this.FechaHoraRegistro = FechaHoraRegistro;
            this.Ciudad = Ciudad;
            this.Estado = Estado;
            this.Pais = Pais;
            this.Activo = Activo;
        }

        public override string ToString()
        {
            return Nombre;
        }
    }
}
