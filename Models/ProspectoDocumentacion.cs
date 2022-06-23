using System;

namespace SmartBusinessAPI.Models
{
    public partial class ProspectoDocumentacion
    {
        public int Anexo { get; set; }
        public int Validacion { get; set; }
        public int Prospecto { get; set; }
        public int Tipo { get; set; }
        public string Filename { get; set; }
        public DateTime Fecha_Insert { get; set; }
        public DateTime Fecha_Update { get; set; }
    }
}
