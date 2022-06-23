using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SmartBusinessAPI.Models
{
    public partial class Testecbr
    {
        public long? Id { get; set; }
        public int? Tipo { get; set; }
        public int? Mobile { get; set; }
        public string Nombre { get; set; }
        public string Solonombre { get; set; }
        public string Apellidopaterno { get; set; }
        public string Apellidomaterno { get; set; }
        public string Email { get; set; }
        public string Email2 { get; set; }
        public long? Userid { get; set; }
    }
}
