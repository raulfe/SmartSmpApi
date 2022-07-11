namespace SmartBusinessAPI.Models
{
    public class PromocionProductos
    {
        public int Producto { get; set; }
        public int Promocion { get; set; }
        public double Monto { get; set; }
        public decimal Rendimiento { get; set; }
        public decimal Comision { get; set; }
        public int Plazo_comision { get; set; }
    }
}
