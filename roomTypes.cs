using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pantallas_alto_volumen_de_datos
{
    class roomTypes
    {
        public int id { get; set; }
        public int idHotel { get; set; }
        public string name { get; set; }
        public string bedTypes { get; set; }
        public int bedNums { get; set; }
        public decimal priceNight { get; set; }
        public int maxGuests { get; set; }
        public int maxRooms { get; set; }
        public string description { get; set; }
        public string viewType { get; set; }
        public string dimensions { get; set; }
        public string level { get; set; }
        public List<string> amenites { get; set; }
        public int habInitNum { get; set; }
        public int habEndNum { get; set; }
        public string emailUser { get; set; }
        public Cassandra.LocalDate dateRegistered { get; set; }
    }
}
