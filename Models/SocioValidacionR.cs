using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBusinessAPI.Models
{
    public class SocioValidacionR
    {
        public int Validacion { get; set; }
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
        public string Payload { get; set; }
        public string IdValidation { get; set; }
        public string IdRelated { get; set; }
        public DateTime FechaInsert { get; set; }
        public DateTime FechaUpdate { get; set; }
    }
}
