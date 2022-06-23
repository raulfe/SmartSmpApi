using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SmartBusinessAPI.Models
{
    public partial class Parametros
    {
        public string Categoria { get; set; }
        public string Parametro { get; set; }
        public double? Nvalor { get; set; }
        public string Svalor { get; set; }
        public DateTime? Dvalor { get; set; }
        public string Descripcion { get; set; }
        public string Tipo { get; set; }
        public string Rango { get; set; }
        public bool Eseditable { get; set; }
    }
}
