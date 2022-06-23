using SmartBusinessAPI.Entities.DTOs;
using SmartBusinessAPI.Models;
using System.Collections.Generic;

namespace SmartBusinessAPI.Entities.NuevosProducto
{
    public class NewProduct
    {
        public ProductosDTO Plan { get; set; }
        public List<ProductoPais> Paises { get; set; }
        public PaquetesDTO Smartpack { get; set; }
        public List<Adicionales> Adicionales { get; set; }
    }
}
