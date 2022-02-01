using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Delivery.SelfServiceKioskApi.Models.Delivery.Order
{
    public class Order
    {
        public Guid Id { get; set; }
        public string Number { get; set; }
        public DateTime CreateDatetime { get; set; }
        public decimal Sum { get; set; }
        public string Comment { get; set; }
        public List<PaymentItem> PaymentItems { get; set; }
        public string MarketingSource { get; set; }
        public string MarketingSourceId { get; set; }
        public int PersonsCount { get; set; }
    }
}
