using System.Collections.Generic;

namespace SmartBusinessAPI.Entities.Verification
{
    public class Document
    {
        public string country { get; set; }
        public object region { get; set; }
        public string type { get; set; }
        public List<Step> steps { get; set; }
        public Fields fields { get; set; }
        public List<string> photos { get; set; }
    }
}
