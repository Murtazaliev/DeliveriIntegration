using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Delivery.SelfServiceKioskApi.Models.Delivery
{
    public enum KioskName
    {
        Iiko = 1,
        RKeeper
    }
    public class RequestParamsModel
    {
        public Guid organizationId { get; set; }
        public Guid CategoryId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int KioskId { get; set; }
        public Guid RequestId { get; set; }
    }
}
