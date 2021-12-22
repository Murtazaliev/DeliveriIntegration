using Delivery.SelfServiceKioskApi.Models.Delivery.Order;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Delivery.SelfServiceKioskApi.Models.Iiko.Order
{
    public class Root
    {
        public string organization { get; set; }
        public Customer customer { get; set; }
        public IikoOrder order { get; set; }

        [JsonProperty("paymentItems")]
        public List<PaymentItem> paymentItems { get; set; } = new List<PaymentItem>();
    }
}
