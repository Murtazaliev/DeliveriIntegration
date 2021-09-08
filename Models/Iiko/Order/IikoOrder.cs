using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Delivery.SelfServiceKioskApi.Models.Iiko.Order
{
    public class IikoOrder
    {
        public string id { get; set; }
        public string date { get; set; }
        public string phone { get; set; }
        public string isSelfService { get; set; }
        public List<Item> items { get; set; }
        public Address address { get; set; }
    }
}
