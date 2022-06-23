using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SmartBusinessAPI.Models
{
    public partial class SocioInfo
    {
        public int Socio { get; set; }
        public string Contrasena { get; set; }
        public string TelCasa { get; set; }
        public string TelOficina { get; set; }
        public string TelCelular { get; set; }
        public int MedioContacto { get; set; }
        public string MedioContactoOtro { get; set; }
        public bool? IntFormacion { get; set; }
        public bool? IntAhorro { get; set; }
        public bool? IntNetworking { get; set; }
        public bool? IntEmprendimiento { get; set; }
        public string Calle { get; set; }
        public string NumExt { get; set; }
        public string NumInt { get; set; }
        public string Colonia { get; set; }
        public string Municipio { get; set; }
        public string Ciudad { get; set; }
        public string CodigoPostal { get; set; }
        public int Estado { get; set; }
        public int Pais { get; set; }
        public string Foto { get; set; }
        public string CveIdentidad { get; set; }
        public string CveFiscal { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int? Genero { get; set; }
        public DateTime FechaInsert { get; set; }
        public DateTime FechaUpdate { get; set; }

        public virtual Estados Estados { get; set; }
        public virtual Socios SocioNavigation { get; set; }
    }
}
