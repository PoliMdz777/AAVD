using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pantallas_alto_volumen_de_datos.Entidades
{
    public class Cliente
    {
        public int ID_Cliente { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Ciudad { get; set; }
        public string Estado { get; set; }
        public string Pais { get; set; }
        public string RFC { get; set; }
        public string Correo { get; set; }
        public string Celular { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string EstadoCivil { get; set; }
        public bool Status { get; set; }  // True = activo, False = eliminado
        public string Telefono { get; set; }


        public Cliente() { }

        public Cliente(int ID_Cliente, string Nombre, string Apellidos, string Ciudad, string Estado, string Pais, string RFC, string Correo,  string Celular, DateTime FechaNacimiento, string EstadoCivil, bool Status, string Telefono)
        {
            this.ID_Cliente = ID_Cliente;
            this.Nombre = Nombre;
            this.Apellidos = Apellidos;
            this.Ciudad = Ciudad;
            this.Estado = Estado;
            this.Pais = Pais;
            this.RFC = RFC;
            this. Correo = Correo;
            this.Celular = Celular;
            this.Status = Status;
            this.Telefono = Telefono;
        }
    }
}
