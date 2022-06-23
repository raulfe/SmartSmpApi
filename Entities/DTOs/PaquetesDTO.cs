using System;

namespace SmartBusinessAPI.Entities.DTOs
{
    public class PaquetesDTO
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Plan_Inicial { get; set; }
        public decimal Rendimiento { get; set; }
        public DateTime Fecha_ini { get; set; }
        public DateTime Fecha_fin { get; set; }
    }
}
