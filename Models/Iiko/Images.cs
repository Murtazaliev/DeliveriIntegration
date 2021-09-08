using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Delivery.SelfServiceKioskApi.Models.Iiko
{
    public class Images
    {
        public Guid imageId { get; set; }
        public string imageUrl { get; set; }
        public string uploadDate { get; set; }
    }
}
