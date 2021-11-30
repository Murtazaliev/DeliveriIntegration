using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Delivery.SelfServiceKioskApi.Models.Iiko.Order
{
    public class Modifier
    {
        public string id { get; set; }
        public string name { get; set; }
        public int amount { get; set; } = 1;
        public string groupId { get; set; }
        public string groupName { get; set; }
    }
}
