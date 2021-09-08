using System.Threading.Tasks;

namespace Delivery.SelfServiceKioskApi.Domain
{
    public interface IRkeeper
    {
            public Task<string> Authorize(string clientSecret, string clientId);
    }
}