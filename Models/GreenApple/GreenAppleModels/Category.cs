using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Delivery.SelfServiceKioskApi.Models.GreenApple.GreenAppleModels
{
    public class Category
    {
        [JsonProperty("name_ru")]
        public string NameRu { get; set; }

        [JsonProperty("externalId")]
        public string ExternalId { get; set; }

        [JsonProperty("externalSectionId")]
        public string ExternalSectionId { get; set; }

        [JsonProperty("sort")]
        public int Sort { get; set; }
    }
}
