using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SmartBusinessAPI.Models
{
    public partial class SocioDocumentacion
    {
        public int Anexo { get; set; }
        public int Socio { get; set; }
        public int Tipo { get; set; }
        public string Filename { get; set; }
        public DateTime FechaInsert { get; set; }
        public DateTime FechaUpdate { get; set; }

        public virtual Socios SocioNavigation { get; set; }
    }
}
