using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SmartBusinessAPI.Models
{
    public partial class SocioWallet
    {
        public int Socio { get; set; }
        public int? Criptomoneda { get; set; }
        public string Wallet { get; set; }
        public DateTime? FechaAlta { get; set; }
        public DateTime? FechaInsert { get; set; }
        public DateTime? FechaUpdate { get; set; }

        public virtual Socios SocioNavigation { get; set; }
    }
}
