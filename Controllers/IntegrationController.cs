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
using Delivery.SelfServiceKioskApi.Models.GreenApple;

namespace Delivery.SelfServiceKioskApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IntegrationController : ControllerBase
    {
        readonly DeliveryKioskApiContext _context;
        private readonly GreenAppleService _appleService;

        public IntegrationController()
        {

        }

        [HttpPost]
        [Route("sendNomenclature")]
        public async Task<IActionResult> SendNomenclature([FromForm]NomenclatureRequestData model)
        {
            
            return BadRequest();
        }
    }
}
