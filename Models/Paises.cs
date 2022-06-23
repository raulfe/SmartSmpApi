using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SmartBusinessAPI.Models
{
    public partial class Paises
    {
        public int Pais { get; set; }
        public int Continente { get; set; }
        public string Nombre { get; set; }
        public int? Phonecode { get; set; }
        public string CountryCode { get; set; }
    }
}
