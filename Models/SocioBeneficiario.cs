using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SmartBusinessAPI.Models
{
    public partial class SocioBeneficiario
    {
        public int Id { get; set; }
        public int Socio { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPat { get; set; }
        public string ApellidoMat { get; set; }
        public string Email { get; set; }
        public string Email2 { get; set; }
        public string TelCasa { get; set; }
        public string TelOficina { get; set; }
        public string TelCelular { get; set; }
        public string Calle { get; set; }
        public string NumExt { get; set; }
        public string NumInt { get; set; }
        public string Colonia { get; set; }
        public string Municipio { get; set; }
        public string Ciudad { get; set; }
        public string CodigoPostal { get; set; }
        public int? Estado { get; set; }
        public int? Pais { get; set; }
        public DateTime? FechaInsert { get; set; }
        public DateTime? FechaUpdate { get; set; }

        public virtual Estados Estados { get; set; }
        public virtual Socios SocioNavigation { get; set; }
    }
}
