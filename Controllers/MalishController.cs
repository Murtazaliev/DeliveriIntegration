﻿using Delivery.SelfServiceKioskApi.DbModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Delivery.SelfServiceKioskApi.Concrete;
using Delivery.SelfServiceKioskApi.Concrete.GreenApple;
using Delivery.SelfServiceKioskApi.Concrete.Malish;
using Delivery.SelfServiceKioskApi.Models.GreenApple.GreenAppleModels;
using Delivery.SelfServiceKioskApi.Models.Malish.MalishModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Delivery.SelfServiceKioskApi.Helpers;
using Sentry;

namespace Delivery.SelfServiceKioskApi.Controllers
{
    [Route(template: Routes.ControllerRoute)]
    [ApiController]
    public class MalishController : ControllerBase
    {
        private readonly DeliveryKioskApiContext _dbContext;
        private readonly INomenclatureService _nomenclatureService;
        private readonly MalishService _malishService;

        public MalishController(DeliveryKioskApiContext dbContext, INomenclatureService nomenclatureService)
        {
            _dbContext = dbContext;
            _nomenclatureService = nomenclatureService;
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
                SentrySdk.CaptureException(ex);
                return BadRequest(ex);
            }
            
            try
            {
                if (_malishService.AnyNomenclature())
                {
                    await _nomenclatureService.UpdateNomenclature(Organisations.MalishId);
                    await _nomenclatureService.UpdateNomenclature(Organisations.KhasMalishId);
                }
            }
            catch (Exception ex)
            {
                SentrySdk.CaptureException(ex);
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
                SentrySdk.CaptureException(ex);
                return BadRequest(ex);
            }
            
            try
            {
                if (_malishService.AnyNomenclature())
                {
                    await _nomenclatureService.UpdateNomenclature(Organisations.MalishId);
                    await _nomenclatureService.UpdateNomenclature(Organisations.KhasMalishId);
                }
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
