using System;
using System.Collections.Generic;

#nullable disable

namespace Delivery.SelfServiceKioskApi.DbModel
{
    public partial class QueueRequest
    {
        public Guid Id { get; set; }
        public string Request { get; set; }
        public string RequestName { get; set; }
        public DateTime RequestDate { get; set; }
        public string Answer { get; set; }
        public DateTime? AnswerDate { get; set; }
        public Guid? Code { get; set; }
        public bool? IsProcessed { get; set; }
        public int Description { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public Guid? IdOrganization { get; set; }
        public Guid? IdCategory { get; set; }
    }
}
