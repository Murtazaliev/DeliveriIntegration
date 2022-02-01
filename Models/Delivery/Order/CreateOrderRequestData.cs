using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Delivery.SelfServiceKioskApi.Models.Delivery.Order
{
    public class CreateOrderRequestData
	{
		public Guid PartnerId { get; set; }
		public string Login { get; set; }
		public string Password { get; set; }
		public int Kiosk { get; set; }
		public DeliveryLocation DeliveryLocation { get; set; }
		public Customer customer { get; set; }
		public Order order { get; set; }
		public List<OrderItem> OrderItems { get; set; }

	}
}
