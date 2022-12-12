using Delivery.SelfServiceKioskApi.DbModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Delivery.SelfServiceKioskApi.Concrete;
using Delivery.SelfServiceKioskApi.Concrete.AsPrestige;
using Delivery.SelfServiceKioskApi.Concrete.GreenApple;
using Delivery.SelfServiceKioskApi.Concrete.Malish;
using Delivery.SelfServiceKioskApi.Models.GreenApple.GreenAppleModels;
using Delivery.SelfServiceKioskApi.Models.Malish.MalishModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Delivery.SelfServiceKioskApi.Helpers;
using Delivery.SelfServiceKioskApi.Models.AsPrestige.AsPrestigeModels;
using Microsoft.AspNetCore.Authorization;
using Delivery.SelfServiceKioskApi.Concrete.Celitel;
using Delivery.SelfServiceKioskApi.Models.Celitel.CelitelModels;
using Sentry;

namespace Delivery.SelfServiceKioskApi.Controllers
{
    [Route(template: Routes.ControllerRoute)]
    [ApiController]
    public class CelitelController : ControllerBase
    {
        private readonly INomenclatureService _nomenclatureService;
        private readonly CelitelService _celitelService;

        public CelitelController(DeliveryKioskApiContext dbContext, INomenclatureService nomenclatureService)
        {
            _nomenclatureService = nomenclatureService;
            _celitelService = new CelitelService(dbContext);
        }

        [HttpPost]
        [Route("sendCategories")]
        public async Task<IActionResult> SendCategories([FromBody]List<CelitelCategory> categories)
        {
            try
            {
                await _celitelService.SaveCategories(categories);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
            
            try
            {
                var nomenclature = await _celitelService.GetNomenclature();
                await _nomenclatureService.UpdateNomenclature(Organisations.CelitelId);
            }
            catch (Exception ex)
            {
                SentrySdk.CaptureException(ex);
            }

            return Ok();
        }
        
        [HttpPost]
        [Route("sendProducts")]
        public async Task<IActionResult> SendProducts([FromBody]List<CelitelProduct> products)
        {
            try
            {
                await _celitelService.SaveProducts(products);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
            
            try
            {
                var nomenclature = await _celitelService.GetNomenclature();
                await _nomenclatureService.UpdateNomenclature(Organisations.CelitelId);
            }
            catch (Exception ex)
            {
                SentrySdk.CaptureException(ex);
            }

            return Ok();
        }
        
        [HttpGet]
        [Route("getNomenclature")]
        public async Task<IActionResult> GetNomenclature()
        {
            try
            {
                var nomenclature = await _celitelService.GetNomenclature();
                return Ok(nomenclature);
            }
            catch(Exception ex)
            {
                return BadRequest(JsonConvert.SerializeObject(ex));
            }
        }
    }
}
