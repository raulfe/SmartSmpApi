using System;

namespace SmartBusinessAPI.Models
{
    public class Paquetes
    {
        public int Smart_pack { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Plan_Inicial { get; set; }
        public decimal Rendimiento { get; set; }
        public DateTime Fecha_ini { get; set; }
        public DateTime Fecha_fin { get; set; }
        public DateTime Fecha_Update { get; set; }
        public DateTime Fecha_Insert { get; set; }
    }
}
