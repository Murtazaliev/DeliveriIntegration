using System;
using System.Threading.Tasks;
using Delivery.SelfServiceKioskApi.Models.AdminPanel;
using RestSharp;

namespace Delivery.SelfServiceKioskApi.Concrete
{
    public interface INomenclatureService
    {
        Task UpdateNomenclature(Guid partnerId);
        Task<LoginResponseModel> Login(string login, string password);
    }
}