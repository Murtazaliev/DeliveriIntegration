using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Delivery.SelfServiceKioskApi.Models.Celitel.CelitelModels
{
    public class CelitelCategory
    {
        [JsonProperty("externalid")]
        public int ExternalId { get; set; }

        [JsonProperty("name_ru")]
        public string NameRu { get; set; }
    }
}