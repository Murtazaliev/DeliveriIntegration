using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Delivery.SelfServiceKioskApi.Models.Iiko
{
    public class GroupModel
    {
        public string address { get; set; }
        public string additionalInfo { get; set; }
        public string code { get; set; }
        public string description { get; set; }
        public string id { get; set; }
        public bool? isDeleted { get; set; }
        public string name { get; set; }
        public string seoDescription { get; set; }
        public string seoKeywords { get; set; }
        public string seoText { get; set; }
        public string seoTitle { get; set; }
        public string tags { get; set; }
        public List<Images> images { get; set; }
        public bool? isIncludedInMenu { get; set; }
        public int? order { get; set; }
        public string parentGroup { get; set; }
    }
}
