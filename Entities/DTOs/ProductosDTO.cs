using System;

namespace SmartBusinessAPI.Entities.DTOs
{
    public class ProductosDTO
    {
        public int Tipo { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public decimal Monto { get; set; }
        public string Moneda { get; set; }
        public bool Es_monto_abierto { get; set; }
        public bool Pago_btc { get; set; }
        public bool Pago_tdc { get; set; }
        public decimal Rendimiento { get; set; }
        public int Regla_capitalizacion { get; set; }
        public decimal Comision { get; set; }
        public int Plazo_Forzoso { get; set; }
        public int Plazo_Comision { get; set; }
        public int Duracion { get; set; }
        public bool Activo { get; set; }
        public bool Es_borrador { get; set; }
        public DateTime Fecha_Ini { get; set; }
        public DateTime? Fecha_Fin { get; set; }
    }
}
