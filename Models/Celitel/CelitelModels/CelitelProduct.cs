using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Delivery.SelfServiceKioskApi.Models.Celitel.CelitelModels
{
    public class CelitelProduct
    {
        [JsonProperty("name_ru")]
        public string NameRu { get; set; }

        [JsonProperty("weight")]
        public string Weight { get; set; }

        [JsonProperty("externalCategoryid")]
        public int? ExternalCategoryId { get; set; }

        [JsonProperty("externalid")]
        public int ExternalId { get; set; }

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