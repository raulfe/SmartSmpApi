using System;


namespace SmartBusinessAPI.Models
{
    public partial class ProspectoMedia
    {
        public int Prospecto { get; set; }
        public int Video1_Visto { get; set; }
        public int Video2_Visto { get; set; }
        public int Video3_Visto { get; set; }
        public bool Manual_Preguntas { get; set; }
        public bool Pdf_Informativo { get; set; }
        public int Video4_Visto { get; set; }
        public DateTime Fecha_Insert { get; set; }
        public DateTime Fecha_Update { get; set; }
    }
}
