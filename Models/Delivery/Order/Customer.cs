using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Delivery.SelfServiceKioskApi.Models.Delivery.Order
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string Phonenumber { get; set; }
        public string Name { get; set; }
    }
}
