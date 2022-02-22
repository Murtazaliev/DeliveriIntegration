using Delivery.SelfServiceKioskApi.DbModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Delivery.SelfServiceKioskApi.Concrete.GreenApple;
using Delivery.SelfServiceKioskApi.Concrete.Malish;
using Delivery.SelfServiceKioskApi.Models.GreenApple.GreenAppleModels;
using Delivery.SelfServiceKioskApi.Models.Malish.MalishModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Delivery.SelfServiceKioskApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MalishController : ControllerBase
    {
        private readonly DeliveryKioskApiContext _dbContext;
        private readonly MalishService _malishService;

        public MalishController(DeliveryKioskApiContext dbContext)
        {
            _dbContext = dbContext;
            _malishService = new MalishService(dbContext);
        }

        [HttpPost]
        [Route("sendCategories")]
        public async Task<IActionResult> SendCategories([FromBody]List<MalishCategory> categories)
        {
            try
            {
                await _malishService.SaveCategories(categories);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }

            return Ok();
        }
        
        [HttpPost]
        [Route("sendProducts")]
        public async Task<IActionResult> SendProducts([FromBody]List<MalishProduct> products)
        {
            try
            {
                await _malishService.SaveProducts(products);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }

            return Ok();
        }
        
        [HttpGet]
        [Route("getNomenclature")]
        public async Task<IActionResult> GetNomenclature()
        {
            try
            {
                var nomenclature = await _malishService.GetNomenclature();
                return Ok(nomenclature);
            }
            catch(Exception ex)
            {
                return BadRequest(JsonConvert.SerializeObject(ex));
            }
        }
    }
}
