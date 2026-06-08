using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pantallas_alto_volumen_de_datos.Entidades
{
    public class TipoHabitacion
    {
        public int ID_Tipo { get; set; }
        public string Nombre { get; set; }
        public int NumCamas { get; set; }
        public string TipoCama { get; set; }
        public int CantPersonas { get; set; }
        public decimal PrecioPorPersona { get; set; }
        public int CantHabitaciones { get; set; }
        public string NivelHabitacion { get; set; }
        public string Dimensiones { get; set; }
        public string Caracteristicas { get; set; }
        public string Amenidades { get; set; }
        public int NumInicial { get; set; }
        public int NumFinal { get; set; }
        public string UsuarioRegistra { get; set; }
        public DateTime FechaHoraRegistro { get; set; }
        public int ID_Hotel { get; set; }

        public string Vista { get; set; }

        public TipoHabitacion() { }

        public TipoHabitacion(int id_tipo, string nombre, int numCamas, string tipoCama, int cantPersonas, decimal precioPorPersona,
            int cantHabitaciones, string nivelHabitacion, string dimensiones, string caracteristicas, string amenidades,
            int numInicial, int numFinal, string usuarioRegistra, DateTime fechaHoraRegistro, int idHotel, string vista)
        {
            this.ID_Tipo = id_tipo;
            this.Nombre = nombre;
            this.NumCamas = numCamas;
            this.TipoCama = tipoCama;
            this.CantPersonas = cantPersonas;
            this.PrecioPorPersona = precioPorPersona;
            this.CantHabitaciones = cantHabitaciones;
            this.NivelHabitacion = nivelHabitacion;
            this.Dimensiones = dimensiones;
            this.Caracteristicas = caracteristicas;
            this.Amenidades = amenidades;
            this.NumInicial = numInicial;
            this.NumFinal = numFinal;
            this.UsuarioRegistra = usuarioRegistra;
            this.FechaHoraRegistro = fechaHoraRegistro;
            this.ID_Hotel = idHotel;
            this.Vista = vista;
        }
    }
}
