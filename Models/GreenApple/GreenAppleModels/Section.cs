using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Delivery.SelfServiceKioskApi.Models.GreenApple.GreenAppleModels
{
    public class Section
    {
        [JsonProperty("name")]
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonProperty("externalId")]
        public string ExternalId { get; set; }

        [JsonProperty("sort")]
        public int Sort { get; set; }
    }
}
