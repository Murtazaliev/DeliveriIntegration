using Newtonsoft.Json;

namespace Delivery.SelfServiceKioskApi.Models.Delivery.Order
{
    public class PaymentItem
    {
        [JsonProperty("sum")]
        public int Sum { get; set; }

        [JsonProperty("paymentType")]
        public PaymentType PaymentType { get; set; }

        [JsonProperty("additionalData")]
        public object AdditionalData { get; set; }

        [JsonProperty("isProcessedExternally")]
        public bool IsProcessedExternally { get; set; }

        [JsonProperty("isPreliminary")]
        public bool IsPreliminary { get; set; }

        [JsonProperty("isExternal")]
        public bool IsExternal { get; set; }
    }
}