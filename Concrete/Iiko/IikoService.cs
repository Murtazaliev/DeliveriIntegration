﻿using Delivery.SelfServiceKioskApi.Domain;
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

        public IikoService()
        {
            
        }

        public async Task<string> Authorize(string userId, string userSecret)
        {
            string relativeUrl = $"auth/access_token?user_id={userId}&user_secret={userSecret}";
            string token = string.Empty;
            try
            {
                using var client = new HttpClient();
                var response = await client.GetAsync(BaseUrl + relativeUrl);
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception e)
            {
                return "Ошибка авторизации!";
            }
        }

        public async Task<string> GetNomenclature(Guid? organizationId, string accessToken)
        {
            string relativeUrl = $"nomenclature/{organizationId}?access_token={accessToken.Trim('"')}";
            try
            {
                var client = new HttpClient();
                var response = await client.GetAsync(BaseUrl + relativeUrl);
                return await response.Content.ReadAsStringAsync();
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
            var data = new StringContent(root, Encoding.UTF8, "application/json");

            string RelativeUrl = String.Format(BaseUrl + "orders/add?access_token={0}&requestTimeout=00:02:00", access_token.Trim('"'));

            using var client = new HttpClient();

            var response = await client.PostAsync(RelativeUrl, data);

            string result = response.Content.ReadAsStringAsync().Result;

            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception(result);

            return result;
        }
    }
}
