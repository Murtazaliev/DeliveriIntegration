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
        public Task<string> Authorize(string userId, string userSecret);
        public List<OrganizationModel> GetOrganizations(string accessToken, string requestTimeout);
        public Task<string> GetNomenclature(Guid? organizationId, string accessToken);
        public Task<string> AddOrderAsync<T>(Guid? organization_id, string access_token, T root) where T : class;

    }
}
