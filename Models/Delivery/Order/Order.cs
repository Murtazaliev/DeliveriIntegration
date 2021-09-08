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
    }

}
