using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBusinessAPI.Models
{
    public class SocioHistory
    {

        public int socio { get; set; }

        public DateTime fecha_inicio { get; set; }
        public int estatus { get; set; }
        public decimal capital_inicial { get; set; }
        public decimal capital { get; set;  }


    }
}
