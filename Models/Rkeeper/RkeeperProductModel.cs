using System.Collections.Generic;
using Newtonsoft.Json;

namespace Delivery.SelfServiceKioskApi.Models.Rkeeper
{
    public class Measure
    {
        [JsonProperty("value")]
        public int Value { get; set; }

        [JsonProperty("unit")]
        public string Unit { get; set; }
    }

    public class RkeeperProductModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("categoryId")]
        public string CategoryId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("price")]
        public int Price { get; set; }

        [JsonProperty("schemeId")]
        public string SchemeId { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("imageUrls")]
        public List<string> ImageUrls { get; set; }

        [JsonProperty("measure")]
        public Measure Measure { get; set; }

        [JsonProperty("isContainInStopList")]
        public List<string> IsContainInStopList { get; set; }

        [JsonProperty("calories")]
        public int Calories { get; set; }

        [JsonProperty("energyValue")]
        public int EnergyValue { get; set; }

        [JsonProperty("proteins")]
        public int Proteins { get; set; }

        [JsonProperty("fats")]
        public int Fats { get; set; }

        [JsonProperty("carbohydrates")]
        public int Carbohydrates { get; set; }
    }
}