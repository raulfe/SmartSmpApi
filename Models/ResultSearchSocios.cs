using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBusinessAPI.Models
{
    public class ResultSearchSocios
    {
        public  int socio { get; set; }
        public  string nombre { get; set; }
        public string email { get; set; }
        public string tel_celular { get; set; }
        public string nombre_rango { get; set; }
    }
}
