using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Delivery.SelfServiceKioskApi.Models.GreenApple.GreenAppleModels
{
    public class GreenAppleProduct
    {
        [JsonProperty("name_ru")]
        [JsonPropertyName("name_ru")]
        public string NameRu { get; set; }

        [JsonProperty("externalCategoryId")]
        [JsonPropertyName("externalCategoryId")]
        public string ExternalCategoryId { get; set; }

        [JsonProperty("externalId")]
        [JsonPropertyName("externalId")]
        public string ExternalId { get; set; }

        [JsonProperty("cost")]
        [JsonPropertyName("cost")]
        public decimal Cost { get; set; }

        [JsonProperty("oldPrice")]
        [JsonPropertyName("oldPrice")]
        public decimal OldPrice { get; set; }

        [JsonProperty("hidden")]
        [JsonPropertyName("hidden")]
        public bool Hidden { get; set; }
    }
}
