using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SmartBusinessAPI.Models
{
    public partial class Estados
    {
        public Estados()
        {
            ProspectoInfo = new HashSet<ProspectoInfo>();
            SocioBeneficiario = new HashSet<SocioBeneficiario>();
            SocioInfo = new HashSet<SocioInfo>();
        }

        public int Estado { get; set; }
        public int Pais { get; set; }
        public string Nombre { get; set; }

        public virtual Paises PaisNavigation { get; set; }
        public virtual ICollection<ProspectoInfo> ProspectoInfo { get; set; }
        public virtual ICollection<SocioBeneficiario> SocioBeneficiario { get; set; }
        public virtual ICollection<SocioInfo> SocioInfo { get; set; }
    }
}
