using SmartBusinessAPI.Models;
using System.Collections.Generic;

namespace SmartBusinessAPI.Entities.NuevaPromocion
{
    public class NewPromocionMembresia
    {
        public Promociones Promocion { get; set; }
        public List<PromocionMembresias> Membresias { get; set; }
        public List<PromocionPaises> Paises { get; set; }
    }
}
