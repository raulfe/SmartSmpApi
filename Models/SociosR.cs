using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBusinessAPI.Models
{
    public class SociosR
    {
        public int Socio { get; set; }
        public string Nombre { get; set; }
        public string Apellido_Pat { get; set; }
        public string Apellido_Mat { get; set; }
        public string Email { get; set; }
        public int? Padre { get; set; }
        public int? Rango { get; set; }
        public string Email2 { get; set; }
        public int Cultura { get; set; }
        public int Estatus { get; set; }
        public string Tel_Celular { get; set; }
        public string Calle { get; set; }
        public string Num_ext { get; set; }
        public string Num_int { get; set; }
        public string Colonia { get; set; }
        public string Municipio { get; set; }
        public string Ciudad { get; set; }
        public string Codigo_postal { get; set; }
        public string Zona_Horaria { get; set; }
        public DateTime Last_Login { get; set; }
        public DateTime Fecha_Insert { get; set; }
        public DateTime Fecha_Update { get; set; }
        public bool Email_Verified { get; set; }
        public bool Etl { get; set; }
    }
}
