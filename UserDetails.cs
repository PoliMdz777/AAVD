using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;


namespace Pantallas_alto_volumen_de_datos
{
    public class UserDetails
    {
        public int id;
        public string password;
        public string name;
        public string email;
        public string phone;
        public string cellPhone;
        public bool admin;
        public string address;
        public Cassandra.LocalDate birthdate;
        public Cassandra.LocalDate entryDate;
        public bool status;
    }
}
