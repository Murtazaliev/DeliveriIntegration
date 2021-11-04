using Delivery.SelfServiceKioskApi.Domain;
using Delivery.SelfServiceKioskApi.Models.Iiko;
using Delivery.SelfServiceKioskApi.Models.Iiko.Order;
using Delivery.SelfServiceKioskApi.Models.Iiko.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Delivery.SelfServiceKioskApi.Helpers;

namespace Delivery.SelfServiceKioskApi.Concrete.Iiko
{
    public class IikoService : IKiosk
    {
        private readonly string BaseUrl = "https://iiko.biz:9900/api/0/";
        private Repository _repository;

        public IikoService()
        {
            _repository = new Repository(BaseUrl);
        }

        public async Task<string> Authorize(string userId, string userSecret)
        {
            string method = "auth/access_token";
            string token = string.Empty;
            try
            {
                var data = new {user_id = userId, user_secret = userSecret};;
                token = await _repository.GetAsync(method, data, String.Empty);
                return token;
            }
            catch (Exception e)
            {
                return "Ошибка авторизации!";
            }
        }

        public async Task<string> GetNomenclature(Guid? organizationId, string accessToken)
        {
            string method = $"nomenclature/{organizationId}?";
            try
            {
                var data = new {access_token= accessToken};
                var result = await _repository.GetAsync(method,data, string.Empty);
                return result;
            }
            catch (Exception e)
            {
                return "Ошибка выполнения! \n" + e.Message;
            }
        }

        public List<OrganizationModel> GetOrganizations(string accessToken, string requestTimeout)
        {
            throw new NotImplementedException();
        }

        public async Task<string> AddOrderAsync(Guid? organization_id, string access_token, string root)
        {
            try
            {
                var data = new StringContent(root, Encoding.UTF8, "application/json");

                string RelativeUrl = String.Format(BaseUrl + "orders/add?access_token={0}&requestTimeout=00:02:00", access_token.Trim('"'));

                using var client = new HttpClient();

                var response = await client.PostAsync(RelativeUrl, data);

                string result = response.Content.ReadAsStringAsync().Result;

                return result;
            }
            catch (Exception e)
            {
                return "Ошибка выполнения! \n" + e.Message;
            }
        }
    }
}
