using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Delivery.SelfServiceKioskApi.Models.GreenApple.GreenAppleModels
{
    public class GreenAppleCategory
    {
        [JsonProperty("name_ru")]
        [JsonPropertyName("name_ru")]
        public string NameRu { get; set; }

        [JsonProperty("externalId")]
        [JsonPropertyName("externalId")]
        public string ExternalId { get; set; }

        [JsonProperty("externalSectionId")]
        [JsonPropertyName("externalSectionId")] 
        public string ExternalSectionId { get; set; }

        [JsonProperty("sort")]
        [JsonPropertyName("sort")]
        public int Sort { get; set; }
    }
}
