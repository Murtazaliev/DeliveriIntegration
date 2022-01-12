using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Delivery.SelfServiceKioskApi.Models.Delivery
{
    public class ProductCategory
    {
        public Guid Id { get; set; }
        public string ExternalId { get; set; }
        public Guid ParentId { get; set; }
        public string ExternalParentId { get; set; }
        public string Name { get; set; }
        public string Img { get; set; }
        public int? CategoryPriority { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();
    }
}
