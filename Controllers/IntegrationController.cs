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
using Delivery.SelfServiceKioskApi.Concrete.GreenApple;
using Delivery.SelfServiceKioskApi.Concrete.Rkeeper;
using Delivery.SelfServiceKioskApi.Models.GreenApple;

namespace Delivery.SelfServiceKioskApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IntegrationController : ControllerBase
    {
        private readonly DeliveryKioskApiContext _dbContext;
        private readonly GreenAppleService _appleService;

        public IntegrationController(DeliveryKioskApiContext dbContext)
        {
            _dbContext = dbContext;
            _appleService = new GreenAppleService(dbContext);
        }

        [HttpPost]
        [Route("sendNomenclature")]
        public async Task<IActionResult> SendNomenclatureAsync([FromForm]NomenclatureRequestData model)
        {
            try
            {
                await _appleService.SaveNomenclature(model);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }

            return Ok();
        }
        
        [HttpGet]
        [Route("getNomenclature")]
        public IActionResult GetNomenclature()
        {
            try
            {
                return Ok(_appleService.GetNomenclature());
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
