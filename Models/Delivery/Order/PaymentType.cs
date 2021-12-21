using Newtonsoft.Json;

namespace Delivery.SelfServiceKioskApi.Models.Delivery.Order
{
    public class PaymentType
    {
        [JsonProperty("applicableMarketingCampaigns")]
        public object ApplicableMarketingCampaigns { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("combinable")]
        public bool Combinable { get; set; }

        [JsonProperty("comment")]
        public string Comment { get; set; }

        [JsonProperty("deleted")]
        public bool Deleted { get; set; }

        [JsonProperty("externalRevision")]
        public int ExternalRevision { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}