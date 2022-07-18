﻿using System;

namespace SmartBusinessAPI.Entities
{
    public class KycProspectosList
    {
        public int Prospecto { get; set; }
        public int Estatus { get; set; }
        public string Nombre { get; set; }
        public string Apellido_Pat { get; set; }
        public string Apellido_Mat { get; set; }
        public string Email { get; set; }
        public DateTime Fecha_kyc { get; set; }
    }
}