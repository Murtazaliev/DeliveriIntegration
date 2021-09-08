using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Delivery.SelfServiceKioskApi.Models.Delivery.Order
{
    public class DeliveryLocation
    {
        public string City { get; set; }
        public string Street { get; set; }
        public string Home { get; set; }
        public string Apartment { get; set; }
        public string Comment { get; set; }
    }
}
