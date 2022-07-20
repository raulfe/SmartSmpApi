using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBusinessAPI.Models
{
    public class SocioSearch
    {

        SocioSearch()
        {
            nombre = "";
            id = 0;
            rango = 0;
            pagina = 0; 
        }
        public int id {get; set;}
        public string nombre { get; set; }
        public int rango { get; set; }
        public int pagina { get; set; }
    }
}
