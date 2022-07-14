namespace SmartBusinessAPI.Entities.ValidationStatus
{
    public class SocioData
    {
        public int Socio { get; set; }
        public string Nombre { get; set; }
        public string Apellido_Pat { get; set; }
        public string Apellido_Mat { get; set; }
        public string Email { get; set; }
        public bool Email_verified { get; set; }
        public bool Phone_verified { get; set; }
        public string Tel_Celular { get; set; }
    }
}
