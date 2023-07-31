using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Delivery.SelfServiceKioskApi.Models.Malish
{
    public class MalishNomenclatureProduct
    {
        [JsonProperty("ExternalId")]
        [JsonPropertyName("ExternalId")]
        public string ExternalId { get; set; }

        [JsonProperty("ExternalCategoryId")]
        [JsonPropertyName("ExternalCategoryId")]
        public string ExternalCategoryId { get; set; }

        [JsonProperty("Name")]
        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonProperty("Cost")]
        [JsonPropertyName("Cost")]
        public decimal Cost { get; set; }

        [JsonProperty("OldPrice")]
        [JsonPropertyName("OldPrice")]
        public decimal OldPrice { get; set; }

        [JsonProperty("StoreCode")]
        [JsonPropertyName("StoreCode")]
        public string StoreCode { get; set; }

        [JsonProperty("Quantity")]
        [JsonPropertyName("Quantity")]
        public int Quantity { get; set; }

        [JsonProperty("IsVisible")]
        [JsonPropertyName("IsVisible")]
        public bool IsVisible { get; set; }

        [JsonProperty("ProductImage")]
        [JsonPropertyName("ProductImage")]
        public string ProductImage { get; set; }
    }
}