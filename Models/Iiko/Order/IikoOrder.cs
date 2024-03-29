﻿using Delivery.SelfServiceKioskApi.Models.Delivery.Order;
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
        public List<PaymentItem> paymentItems { get; set; } = new List<PaymentItem>();
        public string marketingSource { get; set; }
        public string marketingSourceId { get; set; }
        public int personsCount { get; set; }
        public string comment { get; set; }
    }
}
