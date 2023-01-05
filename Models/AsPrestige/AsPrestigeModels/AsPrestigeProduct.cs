using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Delivery.SelfServiceKioskApi.Models.AsPrestige.AsPrestigeModels
{
    public class AsPrestigeProduct
    {
        [JsonProperty("name_ru")]
        [JsonPropertyName("name_ru")]
        public string NameRu { get; set; }

        [JsonProperty("weight")]
        [JsonPropertyName("weight")]
        public string Weight { get; set; }

        [JsonProperty("externalCategoryId")]
        [JsonPropertyName("externalCategoryId")]
        public string ExternalCategoryId { get; set; }

        [JsonProperty("externalId")]
        [JsonPropertyName("externalId")]
        public string ExternalId { get; set; }

        [JsonProperty("Quantity")]
        [JsonPropertyName("Quantity")]
        public int Quantity { get; set; }

        [JsonProperty("cost")]
        [JsonPropertyName("cost")]
        public int Cost { get; set; }
        
        [JsonProperty("oldPrice")]
        [JsonPropertyName("oldPrice")]
        public int OldPrice { get; set; }

        [JsonProperty("hidden")]
        [JsonPropertyName("hidden")]
        public bool Hidden { get; set; }

        [JsonProperty("description")]
        [JsonPropertyName("description")]
        public string Description { get; set; }
    }
}