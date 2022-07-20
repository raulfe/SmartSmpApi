using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBusinessAPI.Models
{
    public class SocioProductSearch
    {
        SocioProductSearch() 
        {
            pagina = 0;
        }

        public int id { get; set; }

        public int pagina { get; set; }

    }
}
