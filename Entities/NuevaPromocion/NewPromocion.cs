using SmartBusinessAPI.Models;
using System.Collections.Generic;

namespace SmartBusinessAPI.Entities.NuevaPromocion
{
    public class NewPromocion
    {
        public Promociones Promocion { get; set; }
        public List<PromocionProductos> Planes { get; set; }
        public List<PromocionPaises> Paises { get; set; }
    }
}
