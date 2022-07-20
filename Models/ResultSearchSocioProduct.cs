using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBusinessAPI.Models
{
    public class ResultSearchSocioProduct
    {

        public int socio { get; set; }

        public string nombre { get; set; }

        public string email { get; set;  }

        public string telefono { get; set; }
        
        public int total_planes { get; set;  }

    }
}
