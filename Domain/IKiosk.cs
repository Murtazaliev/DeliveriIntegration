using Delivery.SelfServiceKioskApi.Models.Iiko;
using Delivery.SelfServiceKioskApi.Models.Iiko.Order;
using Delivery.SelfServiceKioskApi.Models.Iiko.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Delivery.SelfServiceKioskApi.Domain
{
    interface IKiosk
    {
        public Task<string> Authorize(string user_id, string user_secret);
        public List<OrganizationModel> GetOrganizations(string access_token, string request_timeout);
        public string GetNomenclature(Guid? organization_id, string access_token);
        public Task<string> AddOrderAsync<T>(Guid? organization_id, string access_token, T root) where T : class;

    }
}
