using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Delivery.SelfServiceKioskApi.Models.AsPrestige.AsPrestigeModels
{
    public class AsPrestigeProduct
    {
        [JsonPropertyName("name_ru")]
        public string NameRu { get; set; }

        [JsonPropertyName("weight")]
        public string Weight { get; set; }

        [JsonPropertyName("externalCategoryId")]
        public string ExternalCategoryId { get; set; }

        [JsonPropertyName("externalId")]
        public string ExternalId { get; set; }

        [JsonPropertyName("Quantity")]
        public int Quantity { get; set; }

        [JsonProperty("cost")]
        public int Cost { get; set; }
        
        [JsonProperty("oldPrice")]
        public int OldPrice { get; set; }

        [JsonProperty("hidden")]
        public bool Hidden { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}