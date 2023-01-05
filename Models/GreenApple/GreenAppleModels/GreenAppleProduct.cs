using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Delivery.SelfServiceKioskApi.Models.GreenApple.GreenAppleModels
{
    public class GreenAppleProduct
    {
        [JsonProperty("name_ru")]
        public string NameRu { get; set; }

        [JsonProperty("externalCategoryId")]
        public string ExternalCategoryId { get; set; }

        [JsonProperty("externalId")]
        public string ExternalId { get; set; }

        [JsonProperty("cost")]
        public decimal Cost { get; set; }

        [JsonProperty("oldPrice")]
        public decimal OldPrice { get; set; }

        [JsonProperty("hidden")]
        public bool Hidden { get; set; }
    }
}
