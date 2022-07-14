﻿using System;


namespace SmartBusinessAPI.Models
{
    public partial class SocioDocumentacion
    {
        public int Anexo { get; set; }
        public int Validacion { get; set; }
        public int Socio { get; set; }
        public int Tipo { get; set; }
        public string Filename { get; set; }
        public DateTime FechaInsert { get; set; }
        public DateTime FechaUpdate { get; set; }
    }
}
