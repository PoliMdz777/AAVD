using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pantallas_alto_volumen_de_datos.Entidades
{
    public class Usuario
    {
        //public int id;
        //public string password;
        //public string name;
        //public string email;
        //public string phone;
        //public string cellPhone;
        //public bool admin;
        //public string address;
        //public Cassandra.LocalDate birthdate;
        //public Cassandra.LocalDate entryDate;
        //public bool state;


        public int ID_Usuario { get; set; }
        public string Correo { get; set; }
        public string Password { get; set; }
        public string Nombre { get; set; }
        public string NumeroNomina { get; set; } 
        public string TipoUsuario { get; set; }
        public string Telefonos { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int NumSeguro { get; set; }
        public string RFC {  get; set; }
        public string UserReg { get; set; }
        public DateTime FechaHora { get; set; }
        public bool Activo { get; set; }
        public string TelCasa { get; set; }

        public Usuario() { }
        public Usuario(int ID_Usuario, string Correo, string Password, string Nombre, string NumeroNomina, string TipoUsuario, string Telefonos, DateTime FechaNacimiento,
           int NumSeguro, string RFC, string UserReg, DateTime FechaHora, bool Activo,string telCasa)
        {
            this.ID_Usuario = ID_Usuario;
            this.Correo = Correo;
            this.Password = Password;
            this.Nombre = Nombre;
            this.NumeroNomina = NumeroNomina;
            this.TipoUsuario = TipoUsuario;
            this.Telefonos = Telefonos;
            this.FechaNacimiento = FechaNacimiento;
            this.NumSeguro = NumSeguro;
            this.RFC = RFC;
            this.UserReg = UserReg;
            this.FechaHora = FechaHora;
            this.Activo= Activo;
            this.TelCasa = telCasa;
        }
    }
}
