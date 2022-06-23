using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SmartBusinessAPI.Models
{
    public partial class Rangos
    {
        public Rangos()
        {
            Socios = new HashSet<Socios>();
        }

        public int Rango { get; set; }
        public string Nombre { get; set; }
        public string BonoMensual { get; set; }

        public virtual ICollection<Socios> Socios { get; set; }
    }
}
