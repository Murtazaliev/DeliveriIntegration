using Delivery.SelfServiceKioskApi.DbModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Delivery.SelfServiceKioskApi.Concrete.GreenApple;
using Delivery.SelfServiceKioskApi.Models.GreenApple.GreenAppleModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Delivery.SelfServiceKioskApi.Helpers;

namespace Delivery.SelfServiceKioskApi.Controllers
{
    [Route(template: Routes.ControllerRoute)]
    [ApiController]
    public class GreenAppleController : ControllerBase
    {
        private readonly DeliveryKioskApiContext _dbContext;
        private readonly GreenAppleService _appleService;

        public GreenAppleController(DeliveryKioskApiContext dbContext)
        {
            _dbContext = dbContext;
            _appleService = new GreenAppleService(dbContext);
        }

        [HttpPost]
        [Route("sendSections")]
        public async Task<IActionResult> SendSections([FromBody]List<GreenAppleSection> sections)
        {
            try
            {
                await _appleService.SaveSections(sections);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }

            return Ok();
        }
        
        [HttpPost]
        [Route("sendCategories")]
        public async Task<IActionResult> SendCategories([FromBody]List<GreenAppleCategory> categories)
        {
            try
            {
                await _appleService.SaveCategories(categories);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }

            return Ok();
        }
        
        [HttpPost]
        [Route("sendProducts")]
        public async Task<IActionResult> SendProducts([FromBody]List<GreenAppleProduct> products)
        {
            try
            {
                await _appleService.SaveProducts(products);
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
                var nomenclature = await _appleService.GetNomenclature();
                return Ok(nomenclature);
            }
            catch(Exception ex)
            {
                return BadRequest(JsonConvert.SerializeObject(ex));
            }
        }
    }
}
