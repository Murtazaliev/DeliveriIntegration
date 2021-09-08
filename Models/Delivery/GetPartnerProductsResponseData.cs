using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Delivery.SelfServiceKioskApi.Models.Delivery
{
    public class GetPartnerProductsResponseData
    {
        public Guid RequestId { get; set; }
        public Guid PartnerId { get; set; }
        public string PartnerName { get; set; }
        public DateTime CreateRequestDate { get; set; }
        public DateTime CreateResponseDate { get; set; }
        public List<ProductCategory> ProductCategories { get; set; }
    }
}
