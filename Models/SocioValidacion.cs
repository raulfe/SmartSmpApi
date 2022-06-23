using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SmartBusinessAPI.Models
{
    public partial class SocioValidacion
    {
        public int Socio { get; set; }
        public int Estatus { get; set; }
        public DateTime? FechaValidacion { get; set; }
        public int EstatusKyc { get; set; }
        public DateTime? FechaEmpresa { get; set; }
        public DateTime? FechaKyc { get; set; }
        public string ResultadoKyc { get; set; }
        public string Observaciones { get; set; }
        public string ValidadoPor { get; set; }
        public string AutorizadoPor { get; set; }
        public DateTime FechaInsert { get; set; }
        public DateTime FechaUpdate { get; set; }

        public virtual Socios SocioNavigation { get; set; }
    }
}
