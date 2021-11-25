using Delivery.SelfServiceKioskApi.Models.Iiko.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Delivery.SelfServiceKioskApi.Models.Delivery.Order
{
    public class OrderItem
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public int Amount { get; set; }
		public decimal Sum { get; set; }
		public List<Modifier> Additives { get; set; }
	}
}
