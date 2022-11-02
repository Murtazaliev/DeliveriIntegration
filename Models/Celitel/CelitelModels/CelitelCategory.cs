using System.Text.Json.Serialization;

namespace Delivery.SelfServiceKioskApi.Models.Celitel.CelitelModels
{
    public class CelitelCategory
    {
        [JsonPropertyName("externalid")]
        public int ExternalId { get; set; }

        [JsonPropertyName("name_ru")]
        public string NameRu { get; set; }
    }
}