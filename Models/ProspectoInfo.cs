using System;
using System.Collections.Generic;


namespace SmartBusinessAPI.Models
{
    public partial class ProspectoInfo
    {
        public int Prospecto { get; set; }
        public int? Pais { get; set; }
        public int? Estado { get; set; }
        public string Tel_Casa { get; set; }
        public string Tel_Oficina { get; set; }
        public string Tel_Celular { get; set; }
        public int? Medio_Contacto { get; set; }
        public string Medio_Contacto_Otro { get; set; }
        public bool? Int_Formacion { get; set; }
        public bool? Int_Ahorro { get; set; }
        public bool? Int_Networking { get; set; }
        public bool? Int_Emprendimiento { get; set; }
        public string Calle { get; set; }
        public string Num_Ext { get; set; }
        public string Num_Int { get; set; }
        public string Colonia { get; set; }
        public string Municipio { get; set; }
        public string Ciudad { get; set; }
        public string Codigo_Postal { get; set; }
        public string Direccion { get; set; }
        public string Foto { get; set; }
        public string Cve_Identidad { get; set; }
        public string Cve_Fiscal { get; set; }
        public DateTime? Fecha_Nacimiento { get; set; }
        public int? Genero { get; set; }
        public DateTime Fecha_Insert { get; set; }
        public DateTime Fecha_Update { get; set; }
    }
}
