namespace SmartBusinessAPI.Entities.Webhook
{
    public class DeviceFingerprint
    {
        public string ua { get; set; }
        public Browser browser { get; set; }
        public Engine engine { get; set; }
        public Os os { get; set; }
        public Cpu cpu { get; set; }
        public App app { get; set; }
        public string ip { get; set; }
        public bool vpnDetectionEnabled { get; set; }
        public string ipRestrictionEnabled { get; set; }
    }
}
