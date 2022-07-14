using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBusinessAPI.Entities.Socios
{
    public class Socios_documentacion
    {
        public string Nombre { get; set; }
        public int Anexo { get; set; }
        public int Socio { get; set; }
        public int Tipo { get; set; }
        public string Filename { get; set; }
        public DateTime FechaInsert { get; set; }
        public DateTime FechaUpdate { get; set; }

    }
}
