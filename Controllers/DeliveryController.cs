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
using Delivery.SelfServiceKioskApi.Helpers;

namespace Delivery.SelfServiceKioskApi.Controllers
{
    [Route(template: Routes.ControllerRoute)]
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
        [Route("get")]
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
        [Route("nomenclature")]
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
        public async Task<IActionResult> AddOrders(CreateOrderRequestData paramsModel)
        {
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {
                if (paramsModel != null)
                {
                    var response = await _delivery.AddOrder(paramsModel);
                    return Ok(response);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            httpResponse.StatusCode = HttpStatusCode.BadRequest;
            return BadRequest(httpResponse.ToString());
        }
    }
}
