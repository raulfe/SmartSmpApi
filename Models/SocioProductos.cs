using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SmartBusinessAPI.Models
{
    public partial class SocioProductos
    {
        public int Plan { get; set; }
        public int Socio { get; set; }
        public int Producto { get; set; }
        public int Deposito { get; set; }
        public int Estatus { get; set; }
        public DateTime FechaAlta { get; set; }
        public DateTime? FechaPago { get; set; }
        public DateTime? FechaInicio { get; set; }
        public decimal Capital { get; set; }
        public DateTime? FechaCalculo { get; set; }
        public DateTime? FechaCierre { get; set; }

        public virtual Productos ProductoNavigation { get; set; }
        public virtual Socios SocioNavigation { get; set; }
    }
}
