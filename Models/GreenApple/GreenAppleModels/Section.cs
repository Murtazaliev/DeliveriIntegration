using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Delivery.SelfServiceKioskApi.Models.GreenApple.GreenAppleModels
{
    public class Section
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("externalId")]
        public string ExternalId { get; set; }

        [JsonProperty("sort")]
        public int Sort { get; set; }
    }
}
