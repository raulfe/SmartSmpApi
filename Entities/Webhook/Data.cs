using Newtonsoft.Json;

namespace SmartBusinessAPI.Entities.Webhook
{
    public class Data
    {
        [JsonProperty("national-id")]
        public bool NationalId { get; set; }

        [JsonProperty("proof-of-residency")]
        public bool ProofOfResidency { get; set; }
    }
}
