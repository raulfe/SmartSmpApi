namespace SmartBusinessAPI.Entities.Verification
{
    public class Step
    {
        public int status { get; set; }
        public string id { get; set; }
        public Error error { get; set; }
        public Data data { get; set; }
    }
}
