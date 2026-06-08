using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pantallas_alto_volumen_de_datos
{
    class additionalService
    {
        public int id { get; set; }
        public string name { get; set; }
        public int price { get; set; }

        public override string ToString()
        {
            return name;
        }
    }
}
