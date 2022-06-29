using System;

namespace SmartBusinessAPI.Models
{
    public partial class Prospectos
    {
        public int Prospecto { get; set; }
        public int Tipo { get; set; }
        public string Nombre { get; set; }
        public string Apellido_Pat { get; set; }
        public string Apellido_Mat { get; set; }
        public string Email { get; set; }
        public string Contrasena { get; set; }
        public int Padre { get; set; }
        public string Email2 { get; set; }
        public int Cultura { get; set; }
        public int Validacion { get; set; }
        public string Zona_Horaria { get; set; }
        public DateTime Last_Login { get; set; }
        public int? Nuevo_Socio { get; set; }
        public DateTime Fecha_Insert { get; set; }
        public DateTime Fecha_Update { get; set; }
        public bool Email_Verified { get; set; }
        public bool Phone_verified { get; set; }
    }
}
