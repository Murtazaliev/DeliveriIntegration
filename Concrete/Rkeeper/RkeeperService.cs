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
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Delivery.SelfServiceKioskApi.Helpers;

namespace Delivery.SelfServiceKioskApi.Concrete.Rkeeper
{
    public class RkeeperService : IKiosk
    {
        private const string BaseUrl = "https://delivery.ucs.ru";

        private Repository _repository;

        public RkeeperService()
        {
            _repository = new Repository(BaseUrl);
        }

        public async Task<string> Authorize(string userId, string userSecret)
        {
            string method = "connect/token";
            try
            {
                var data = new { clientId = userId, clientSecret = userSecret};
                var token = await _repository.PostAsync(method, data,ContentTypes.FormData, string.Empty);   
                return token;
            }
            catch (Exception e)
            {
                return "Ошибка авторизации!";
            }
        }
        
        public async Task<string> GetNomenclature(Guid? organizationId, string accessToken)
        {
            string method = "menu/view";
            try
            {
                var result = await _repository.GetAsync(method, accessToken);                
                return result;
            }
            catch (Exception e)
            {
                return "Ошибка выполнения! \n" + e.Message;
            }
        }

        public Task<string> AddOrderAsync(Guid? organization_id, string access_token, string root)
        {
            throw new NotImplementedException();
        }

        public List<OrganizationModel> GetOrganizations(string accessToken, string requestTimeout)
        {
            throw new NotImplementedException();
        }
        

        public Task<string> AddOrderAsync<T>(Guid? organization_id, string access_token, T root) where T : class
        {
            throw new NotImplementedException();
        }
    }
}
