using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yachay.Entities
{
    public class NodoDTO
    {
        public string Nombre { get; set; }
        public string Direccion_texto { get; set; }
        public string Direccion_Latitud { get; set; }
        public string Direccion_Longitud { get; set; }
        public List<string> PalabrasClave { get; set; }
    }
}
