namespace SmartBusinessAPI.Models.Metamap
{
    public class Auth
    {
        public string access_token { get; set; }
        public int expiresIn { get; set; }
        public Payload payload { get; set; }
    }
}
