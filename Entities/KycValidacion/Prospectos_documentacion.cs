using System;

namespace SmartBusinessAPI.Entities.KycValidacion
{
    public class Prospectos_documentacion
    {
        public string Nombre { get; set; }
        public int Anexo { get; set; }
        public int Validacion { get; set; }
        public int Prospecto { get; set; }
        public int Tipo { get; set; }
        public string Filename { get; set; }
        public DateTime Fecha_Insert { get; set; }
        public DateTime Fecha_Update { get; set; }
    }
}
