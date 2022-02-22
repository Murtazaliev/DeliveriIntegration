using Newtonsoft.Json;

namespace Delivery.SelfServiceKioskApi.Models.Malish.MalishModels
{
    public class MalishProduct
    {
        [JsonProperty("store_code")]
        public string StoreCode { get; set; }

        [JsonProperty("sku_name")]
        public string SkuName { get; set; }

        [JsonProperty("kls_unicode")]
        public string KlsUnicode { get; set; }

        [JsonProperty("sku_code")]
        public string SkuCode { get; set; }

        [JsonProperty("cmp_name")]
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