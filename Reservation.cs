using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cassandra;

namespace Pantallas_alto_volumen_de_datos
{
    public class Reservation
    {
        public TimeUuid Id { get; set; }
        public string RfcClient { get; set; }
        public string name { get; set; }
        public string EmailUser { get; set; }
        public DateTime DateReserved { get; set; }
        public int IdHotel { get; set; }
        public short Nights { get; set; }
        public LocalDate EntryDate { get; set; }
        public short NumRooms { get; set; }
        public List<string> RoomType { get; set; } = new List<string>();
        public short NumGuests { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public string PayForm { get; set; }
        public int SubTotal { get; set; }
        public LocalDate EndDate { get; set; }
        public decimal anticipo  { get; set; }
        public decimal total { get; set; }
        public string Status { get; set; }

        public Reservation()
        {
            RoomType = new List<string>();
        }
    }

}
