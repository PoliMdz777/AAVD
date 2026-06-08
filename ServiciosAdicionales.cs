using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pantallas_alto_volumen_de_datos.Entidades
{
    public class ServicioAdicionales
    {
        public int ID_Servicio { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }

        public override string ToString()
        {
            return Nombre;
        }
    }
}
