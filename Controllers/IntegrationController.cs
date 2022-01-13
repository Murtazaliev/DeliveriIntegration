using Delivery.SelfServiceKioskApi.DbModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Delivery.SelfServiceKioskApi.Concrete.GreenApple;
using Delivery.SelfServiceKioskApi.Models.GreenApple.GreenAppleModels;
using Product = Delivery.SelfServiceKioskApi.Models.GreenApple.GreenAppleModels.Product;

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
        [Route("sendSections")]
        public async Task<IActionResult> SendSections([FromBody]List<Section> sections)
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
        public async Task<IActionResult> SendCategories([FromBody]List<Category> categories)
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
        public async Task<IActionResult> SendProducts([FromBody]List<Product> products)
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
