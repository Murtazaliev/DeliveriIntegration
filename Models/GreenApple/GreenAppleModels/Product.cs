using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Delivery.SelfServiceKioskApi.Models.GreenApple.GreenAppleModels
{
    public class Product
    {
        [JsonProperty("name_ru")]
        public string NameRu { get; set; }

        [JsonProperty("externalCategoryId")]
        public string ExternalCategoryId { get; set; }

        [JsonProperty("externalId")]
        public string ExternalId { get; set; }

        [JsonProperty("cost")]
        public int Cost { get; set; }

        [JsonProperty("oldPrice")]
        public int OldPrice { get; set; }

        [JsonProperty("hidden")]
        public bool Hidden { get; set; }
    }
}
