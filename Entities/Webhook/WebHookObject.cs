using System;

namespace SmartBusinessAPI.Entities.Webhook
{
    public class WebHookObject
    {
        public string resource { get; set; }
        public DeviceFingerprint deviceFingerprint { get; set; }
        public string identityStatus { get; set; }
        public Details details { get; set; }
        public string matiDashboardUrl { get; set; }
        public string status { get; set; }
        public string eventName { get; set; }
        public string flowId { get; set; }
        public DateTime timestamp { get; set; }
    }
}
