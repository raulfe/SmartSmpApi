using SmartBusinessAPI.Models;
using System.Collections.Generic;

namespace SmartBusinessAPI.Entities.NuevosProducto
{
    public class UpdateProduct
    {
        public Productos Plan { get; set; }
        public List<ProductoPais> Paises { get; set; }
        public Paquetes Smartpack { get; set; }
        public List<Adicionales> Adicionales { get; set; }
    }
}
