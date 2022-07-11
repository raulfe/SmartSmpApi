using System;

namespace SmartBusinessAPI.Models
{
    public class Promociones
    {
        public int Promocion { get; set; }
        public int Premio { get; set; }
        public int Insignia { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Rendimiento_extra { get; set; }
        public int Tiraje_limitado { get; set; }
        public int Monto_limitado { get; set; }
        public bool Exclusivo_rangos { get; set; }
        public decimal Descuento_porcentaje { get; set; }
        public int Descuento_monto { get; set; }
        public bool Insignias_and { get; set; }
        public int Ventas_conteo { get; set; }
        public double Ventas_monto { get; set; }
        public string Observaciones { get; set; }
        public bool Activo { get; set; }
        public bool Es_borrador { get; set; }
        public DateTime Fecha_ini { get; set; }
        public DateTime Fecha_fin { get; set; }
        public DateTime Fecha_insert { get; set; }
        public DateTime Fecha_update { get; set; }
    }
}
