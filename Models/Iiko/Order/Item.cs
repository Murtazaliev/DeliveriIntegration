using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Delivery.SelfServiceKioskApi.Models.Iiko.Order
{
    public class Item
    {
        public string id { get; set; }
        public string name { get; set; }
        public int amount { get; set; }
        public string code { get; set; }
        public decimal sum { get; set; }
        public List<object> modifiers { get; set; }
    }
}
