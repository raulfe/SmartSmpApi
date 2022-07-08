using System;

namespace SmartBusinessAPI.Models
{
    public partial class Membresias
    {
        public int Membresia { get; set; }
        public int Producto { get; set; }
        public int Tipo { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public string Moneda { get; set; }
        public int Vigencia { get; set; }
        public DateTime Fecha_ini { get; set; }
        public DateTime Fecha_fin { get; set; }
        public bool Pago_btc { get; set; }
        public bool Pago_tdc { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }
        public bool Es_borrador { get; set; }
    }
}
