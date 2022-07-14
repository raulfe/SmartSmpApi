using System;

namespace SmartBusinessAPI.Entities.KycValidacion
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
