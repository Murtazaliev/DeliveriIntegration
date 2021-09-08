using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Delivery.SelfServiceKioskApi.Models.Iiko
{
    public class OrganizationModel
    {
        public string address { get; set; }
        public string averageCheque { get; set; }
        public Contact contact { get; set; }
        public string currencyIsoName { get; set; }
        public string description { get; set; }
        public string fullName { get; set; }
        public string homePage { get; set; }
        public string id { get; set; }
        public bool? isActive { get; set; }
        public string latitude { get; set; }
        public string logo { get; set; }
        public string logoImage { get; set; }
        public string longitude { get; set; }
        public int? maxBonus { get; set; }
        public int? minBonus { get; set; }
        public string name { get; set; }
        public string networkId { get; set; }
        public int? organizationType { get; set; }
        public string phone { get; set; }
        public string timezone { get; set; }
        public string website { get; set; }
        public string workTime { get; set; }
    }
    public class Contact
    {
        public string email { get; set; }
        public string location { get; set; }
        public string phone { get; set; }
    }
}
