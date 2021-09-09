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
    public class RkeeperService : IRkeeper
    {
        private const string BaseUrl = "https://delivery.ucs.ru";

        private Repository _repository;

        public RkeeperService()
        {
            _repository = new Repository(BaseUrl);
        }

        public async Task<string> Authorize(string clientSecret, string clientId)
        {
            string method = "connect/token";
            try
            {
                var data = new { clientId = clientId, clientSecret = clientSecret};
                var token = await _repository.PostAsync(method, data,ContentTypes.FormData, string.Empty);   
                return token;
            }
            catch (Exception e)
            {
                return "Ошибка авторизации!";
            }
        }
        
        public async Task<string> GetMenu(string token)
        {
            string method = "menu/view";
            try
            {
                var result = await _repository.GetAsync(method, token);                
                return result;
            }
            catch (Exception e)
            {
                return "Ошибка выполнения! \n" + e.Message;
            }
        }  
    }
}
