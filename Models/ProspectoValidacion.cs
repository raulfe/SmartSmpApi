using SmartBusinessAPI.Entities;
using System;

namespace SmartBusinessAPI.Models
{
    public partial class ProspectoValidacion
    {
        public int Validacion { get; set; }
        public int Prospecto { get; set; }
        public int Estatus { get; set; }
        public DateTime? Fecha_Validacion { get; set; }
        public int Estatus_Kyc { get; set; }
        public DateTime? Fecha_Empresa { get; set; }
        public DateTime? Fecha_Kyc { get; set; }
        public string Resultado_Kyc { get; set; }
        public string Observaciones { get; set; }
        public string Validado_Por { get; set; }
        public string Autorizado_Por { get; set; }
        public DateTime Fecha_Insert { get; set; }
        public DateTime Fecha_Update { get; set; }
        public JsonParameter Payload { get; set; }
        public string Id_Validation { get; set; }
        public string Id_Related { get; set; }
    }
}
