using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Delivery.SelfServiceKioskApi.Models.Delivery
{
    public class Product
    {
		public Guid Id { get; set; }
        public string ExternalId { get; set; }
        public string ExternalCategoryId { get; set; }
        public string Name { get; set; }
		public decimal Cost { get; set; } //?********
		public string Img { get; set; }
		public string Description { get; set; }
		public string PortionSize { get; set; }//?********
		public bool IsVisible { get; set; }
		public List<Additive> Additives { get; set; }
        public List<RequiredAdditiveGroup> RequiredAdditiveGroups { get; set; } = new List<RequiredAdditiveGroup>();
    }
}
