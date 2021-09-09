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

        public async Task<string> Authorize(string user_id, string user_secret)
        {
            string method = "auth/access_token";
            string token = string.Empty;
            try
            {
                var data = new {user_id = user_id, user_secret = user_secret};;
                token = await _repository.GetAsync(method, data);
                return token;
            }
            catch (Exception e)
            {
                return "Ошибка авторизации!";
            }
        }

        public string GetNomenclature(Guid? organization_id, string access_token)
        {
            string RelativeUrl = "nomenclature/";
            string result = string.Empty;
            try
            {
                var DATA = organization_id + "?access_token=" + access_token.ToString().Replace("\"", "").Replace("\\", "");
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(BaseUrl + RelativeUrl + DATA);
                request.Method = "GET";
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    result = reader.ReadToEnd();
                }
                var preResult = System.Text.Json.JsonSerializer.Deserialize<IikoNomenclatureViewModel>(result);



                return result;
            }
            catch (Exception e)
            {
                return "Ошибка выполнения! \n" + e.Message;
            }
        }

        public List<OrganizationModel> GetOrganizations(string access_token, string request_timeout)
        {
            throw new NotImplementedException();
        }

        public async Task<string> AddOrderAsync<T>(Guid? organization_id, string access_token, T root) where T : class
        {
            try
            {
                var json = JsonConvert.SerializeObject(root);

                var data = new StringContent(json, Encoding.UTF8, "application/json");

                string RelativeUrl = String.Format("orders/add?access_token={0}&requestTimeout=00:02:00", access_token);

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
