using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Delivery.SelfServiceKioskApi.Models.Iiko.Order
{
    public class Address
    {
        public string city { get; set; }
        public string street { get; set; }
        public string home { get; set; }
        public string apartment { get; set; }
        public string comment { get; set; }
    }
}
