using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cassandra;

namespace Pantallas_alto_volumen_de_datos
{
    public class EmpleadosCassandra
    {

            public int id { get; set; }
            public string password { get; set; }
            public string name { get; set; }
            public string email { get; set; }
            public string phone { get; set; }
            public string cellphone { get; set; }
            public bool admin { get; set; }
            public string address { get; set; }
            public Cassandra.LocalDate birthdate { get; set; }
            //public DateTimeOffset entrydate { get; set; }
        public Cassandra.LocalDate entrydate { get; set; }
        public bool status { get; set; }
          public string created_by { get; set; }
        public string old_password1 { get; set; }
        public string old_password2 { get; set; }




    }
}
