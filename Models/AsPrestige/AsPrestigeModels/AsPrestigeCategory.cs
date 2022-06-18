using System.Text.Json.Serialization;

namespace Delivery.SelfServiceKioskApi.Models.AsPrestige.AsPrestigeModels
{
    public class AsPrestigeCategory
    {
        [JsonPropertyName("externalCategoryId")]
        public string ExternalCategoryId { get; set; }

        [JsonPropertyName("name_ru")]
        public string NameRu { get; set; }
    }
}