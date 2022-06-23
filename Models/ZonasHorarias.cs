using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SmartBusinessAPI.Models
{
    public partial class ZonasHorarias
    {
        public string ZonaHoraria { get; set; }
        public TimeSpan? UtcOffset { get; set; }
        public string Pais { get; set; }
    }
}
