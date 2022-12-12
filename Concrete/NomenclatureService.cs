using System;
using System.Threading.Tasks;
using Delivery.SelfServiceKioskApi.Models.AdminPanel;
using Microsoft.Extensions.Configuration;
using RestSharp;

namespace Delivery.SelfServiceKioskApi.Concrete
{
    public class NomenclatureService : INomenclatureService
    {
        private readonly IConfiguration _configuration;
        private readonly RestClient _restClient;
        private readonly string _baseUrl;

        public NomenclatureService(IConfiguration configuration)
        {
            _configuration = configuration;
            _baseUrl = _configuration.GetValue<string>("AdminPanelUrl");
            _restClient = new RestClient();
        }

        public async Task UpdateNomenclature(Guid partnerId)
        {
            var token = await Login(_configuration.GetValue<string>("AdminPanelLogin"), _configuration.GetValue<string>("AdminPanelPassword"));
            _restClient.AddDefaultHeader("Authorization", $"Bearer {token.Token}");

            var request = new RestRequest(_baseUrl + "Nomenclature/UpdateNomenclature", Method.POST);
            request.AddJsonBody(new { partnerId = partnerId });
            var response = await _restClient.ExecuteAsync(request);
        }

        public async Task<LoginResponseModel> Login(string login, string password)
        {
            var request = new RestRequest(_baseUrl + "User/LogIn", Method.POST);
            request.AddJsonBody(new
            {
                Login = login,
                Password = password
            });
            var response = await _restClient.ExecuteAsync<LoginResponseModel>(request);

            if (response.IsSuccessful)
                return response.Data;
            throw new Exception($"Ошибка в запросе авторизации \n {response.Content}");
        }
    }
}