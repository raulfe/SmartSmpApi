using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SmartBusinessAPI.Models
{
    public partial class Socios
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
        public string Zona_Horaria { get; set; }
        public DateTime Last_Login { get; set; }
        public DateTime Fecha_Insert { get; set; }
        public DateTime Fecha_Update { get; set; }
        public bool Email_Verified { get; set; }
        public bool Etl { get; set; }

    }
}
