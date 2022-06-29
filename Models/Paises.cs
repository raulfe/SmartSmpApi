using System;
using System.Collections.Generic;

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
