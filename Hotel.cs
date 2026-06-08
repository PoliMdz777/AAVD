using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pantallas_alto_volumen_de_datos
{
    public class Hotel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string country { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string address { get; set; }
        public int totalFloors { get; set; }
        public string phone { get; set; }
        public string views { get; set; }
        public bool turisticZone { get; set; }
        public bool eventsSalon { get; set; }
        public int numPools { get; set; }
        public List<string> aditionalService { get; set; }
        public List<string> roomTypes { get; set; }
        public string sat { get; set; }
        public string unitservice { get; set; }
        public int userId { get; set; }
        public Cassandra.LocalDate dateRegistered { get; set; }
        public Cassandra.LocalDate dateOperation { get; set; }
        public bool status { get; set; }

        // En tu archivo de entidades (donde tienes Hotel, Client, etc.)
        public class hotelsbyid
        {
            public int id { get; set; }
            public string country { get; set; }
            public string city { get; set; }
            public string state { get; set; }
            public string name { get; set; }
        }
    }
}
