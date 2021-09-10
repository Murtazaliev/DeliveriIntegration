using Delivery.SelfServiceKioskApi.Concrete;
using Delivery.SelfServiceKioskApi.DbModel;
using Delivery.SelfServiceKioskApi.Models.Delivery;
using Delivery.SelfServiceKioskApi.Models.Delivery.Order;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Delivery.SelfServiceKioskApi.Concrete.Rkeeper;


namespace Delivery.SelfServiceKioskApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryController : ControllerBase
    {
        readonly DeliveryKioskApiContext _context;
        private DeliveryService _delivery;
        private RkeeperService _rkeeperService;

        public DeliveryController()
        {
            _delivery = new DeliveryService();
            _rkeeperService = new RkeeperService();
        }

        [HttpGet]
        public GetPartnerProductsResponseData Get(Guid code)
        {
            try
            {
                var result = _delivery.GetNomenclature(code);
                var x = JsonConvert.DeserializeObject<GetPartnerProductsResponseData>(result);

                return x;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        [HttpPost]
        public HttpResponseMessage Nomenclature(RequestParamsModel paramsModel)
        {
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {
                Task.Run(() => _delivery.AddRequest(paramsModel)).Wait();
                httpResponse.StatusCode = HttpStatusCode.OK;
                return httpResponse;
            }
            catch
            {
                httpResponse.StatusCode = HttpStatusCode.BadRequest;
                return httpResponse;
            }
        }
        [HttpPost]
        [Route("order")]
        public async Task<string> AddOrders(CreateOrderRequestData paramsModel)
        {
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            if(paramsModel != null)
            {
               var response =  await _delivery.AddOrder(paramsModel);
                httpResponse.StatusCode = HttpStatusCode.OK;
                return response;
            }
            else
            {
                httpResponse.StatusCode = HttpStatusCode.BadRequest;
                return httpResponse.ToString();
            }
        }
    }
}
