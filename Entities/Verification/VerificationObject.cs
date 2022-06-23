using System.Collections.Generic;

namespace SmartBusinessAPI.Entities.Verification
{
    public class VerificationObject
    {
        public Computed computed { get; set; }
        public List<Document> documents { get; set; }
        public bool expired { get; set; }
        public Flow flow { get; set; }
        public Identity identity { get; set; }
        public Metadata metadata { get; set; }
        public List<Step> steps { get; set; }
        public string id { get; set; }
        public DeviceFingerprint deviceFingerprint { get; set; }
        public bool hasProblem { get; set; }
    }
}
