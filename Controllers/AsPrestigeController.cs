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
using Sentry;

namespace Delivery.SelfServiceKioskApi.Controllers
{
    [Route(template: Routes.ControllerRoute)]
    [ApiController]
    public class AsPrestigeController : ControllerBase
    {
        private readonly DeliveryKioskApiContext _dbContext;
        private readonly INomenclatureService _nomenclatureService;
        private readonly AsPrestigeService _asPrestigeService;

        public AsPrestigeController(DeliveryKioskApiContext dbContext, INomenclatureService nomenclatureService)
        {
            _dbContext = dbContext;
            _nomenclatureService = nomenclatureService;
            _asPrestigeService = new AsPrestigeService(dbContext);
        }

        [HttpPost]
        [Route("sendCategories")]
        public async Task<IActionResult> SendCategories([FromBody]List<AsPrestigeCategory> categories)
        {
            try
            {
                await _asPrestigeService.SaveCategories(categories);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }

            try
            {
                var nomenclature = await _asPrestigeService.GetNomenclature();
                await _nomenclatureService.UpdateNomenclature(Organisations.AsPrestigeId);
            }
            catch (Exception ex)
            {
                SentrySdk.CaptureException(ex);
            }

            return Ok();
        }
        
        [HttpPost]
        [Route("sendProducts")]
        public async Task<IActionResult> SendProducts([FromBody]List<AsPrestigeProduct> products)
        {
            try
            {
                await _asPrestigeService.SaveProducts(products);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
            
            try
            {
                var nomenclature = await _asPrestigeService.GetNomenclature();
                await _nomenclatureService.UpdateNomenclature(Organisations.AsPrestigeId);
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
                var nomenclature = await _asPrestigeService.GetNomenclature();
                return Ok(nomenclature);
            }
            catch(Exception ex)
            {
                return BadRequest(JsonConvert.SerializeObject(ex));
            }
        }
    }
}
