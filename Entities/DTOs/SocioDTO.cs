using System;

namespace SmartBusinessAPI.Entities.DTOs
{
    public class SocioDTO
    {
        public string Nombre { get; set; }
        public string ApellidoPat { get; set; }
        public string ApellidoMat { get; set; }
        public string Email { get; set; }
        public int? Padre { get; set; }
        public int? Rango { get; set; }
        public int Cultura { get; set; }
        public string ZonaHoraria { get; set; }
    }
}
