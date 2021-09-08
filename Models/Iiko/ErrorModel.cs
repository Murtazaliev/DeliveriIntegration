using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Delivery.SelfServiceKioskApi.Models.Iiko
{
    public class ErrorModel
    {
        public string code { get; set; }
        public string message { get; set; }
        public string exception { get; set; }
        public string description { get; set; }
        public int? httpStatusCode { get; set; }
        public string uiMessage { get; set; }
    }
}
