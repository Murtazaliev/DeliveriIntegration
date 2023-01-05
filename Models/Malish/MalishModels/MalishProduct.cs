using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Delivery.SelfServiceKioskApi.Models.Malish.MalishModels
{
    public class MalishProduct
    {
        [JsonProperty("store_code")]
        [JsonPropertyName("store_code")]
        public string StoreCode { get; set; }

        [JsonProperty("sku_name")]
        [JsonPropertyName("sku_name")]
        public string SkuName { get; set; }

        [JsonProperty("kls_unicode")]
        [JsonPropertyName("kls_unicode")]
        public string KlsUnicode { get; set; }

        [JsonProperty("sku_code")]
        [JsonPropertyName("sku_code")]
        public string SkuCode { get; set; }

        [JsonProperty("cmp_name")]
        [JsonPropertyName("cmp_name")]
        public string CmpName { get; set; }

        [JsonProperty("price")]
        [JsonPropertyName("price")]
        public int Price { get; set; }

        [JsonProperty("discount")]
        [JsonPropertyName("discount")]
        public int Discount { get; set; }

        [JsonProperty("percent")]
        [JsonPropertyName("percent")]
        public int Percent { get; set; }

        [JsonProperty("quantity")]
        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }
    }
}