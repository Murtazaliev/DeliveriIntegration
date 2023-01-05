using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Delivery.SelfServiceKioskApi.Models.AsPrestige.AsPrestigeModels
{
    public class AsPrestigeCategory
    {
        [JsonProperty("externalCategoryId")]
        [JsonPropertyName("externalCategoryId")]
        public string ExternalCategoryId { get; set; }

        [JsonProperty("name_ru")]
        [JsonPropertyName("name_ru")]
        public string NameRu { get; set; }
    }
}