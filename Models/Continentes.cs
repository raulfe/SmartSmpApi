using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SmartBusinessAPI.Models
{
    public partial class Continentes
    {
        public Continentes()
        {
            Paises = new HashSet<Paises>();
        }

        public int Continente { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Paises> Paises { get; set; }
    }
}
