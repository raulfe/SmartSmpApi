using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBusinessAPI.Models
{
    public class ProspectoValidacionR
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
        public string Payload { get; set; }
        public string Id_Validation { get; set; }
        public string Id_Related { get; set; }
    }
}
