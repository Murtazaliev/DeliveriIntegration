using Delivery.SelfServiceKioskApi.DbModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Delivery.SelfServiceKioskApi.Concrete;
using Delivery.SelfServiceKioskApi.Concrete.GreenApple;
using Delivery.SelfServiceKioskApi.Models.GreenApple.GreenAppleModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Delivery.SelfServiceKioskApi.Helpers;
using Sentry;

namespace Delivery.SelfServiceKioskApi.Controllers
{
    [Route(template: Routes.ControllerRoute)]
    [ApiController]
    public class GreenAppleController : ControllerBase
    {
        private readonly DeliveryKioskApiContext _dbContext;
        private readonly INomenclatureService _nomenclatureService;
        private readonly GreenAppleService _appleService;

        public GreenAppleController(DeliveryKioskApiContext dbContext, INomenclatureService nomenclatureService)
        {
            _dbContext = dbContext;
            _nomenclatureService = nomenclatureService;
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
            
            try
            {
                var nomenclature = await _appleService.GetNomenclature();
                await _nomenclatureService.UpdateNomenclature(Organisations.GreenAppleId);
            }
            catch (Exception ex)
            {
                SentrySdk.CaptureException(ex);
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
            
            try
            {
                var nomenclature = await _appleService.GetNomenclature();
                await _nomenclatureService.UpdateNomenclature(Organisations.GreenAppleId);
            }
            catch (Exception ex)
            {
                SentrySdk.CaptureException(ex);
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
            
            try
            {
                var nomenclature = await _appleService.GetNomenclature();
                await _nomenclatureService.UpdateNomenclature(Organisations.GreenAppleId);
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
