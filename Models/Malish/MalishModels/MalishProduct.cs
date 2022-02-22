using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Delivery.SelfServiceKioskApi.Models.Malish.MalishModels
{
    public class MalishProduct
    {
        [JsonPropertyName("store_code")]
        public string StoreCode { get; set; }

        [JsonPropertyName("sku_name")]
        public string SkuName { get; set; }

        [JsonPropertyName("kls_unicode")]
        public string KlsUnicode { get; set; }

        [JsonPropertyName("sku_code")]
        public string SkuCode { get; set; }

        [JsonPropertyName("cmp_name")]
        public string CmpName { get; set; }

        [JsonProperty("price")]
        public int Price { get; set; }

        [JsonProperty("discount")]
        public int Discount { get; set; }

        [JsonProperty("percent")]
        public int Percent { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }
    }
}