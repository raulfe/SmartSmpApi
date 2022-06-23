using System;

namespace SmartBusinessAPI.Models
{
    public partial class Enum
    {
        public string Categoria { get; set; }
        public int Codigo { get; set; }
        public string Nombre { get; set; }
        public double? Nvalor { get; set; }
        public string Svalor { get; set; }
        public DateTime? Dvalor { get; set; }
        public string Descripcion { get; set; }
    }
}
