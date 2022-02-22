using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Delivery.SelfServiceKioskApi.Concrete.GreenApple;
using Delivery.SelfServiceKioskApi.DbModel;
using Delivery.SelfServiceKioskApi.Helpers;
using Delivery.SelfServiceKioskApi.Models.GreenApple;
using Delivery.SelfServiceKioskApi.Models.GreenApple.GreenAppleModels;
using Delivery.SelfServiceKioskApi.Models.Malish;
using Delivery.SelfServiceKioskApi.Models.Malish.MalishModels;
using Newtonsoft.Json;

namespace Delivery.SelfServiceKioskApi.Concrete.Malish
{
    public class MalishService
    {
        private DeliveryKioskApiContext _dbContext;
        private MalishConverter _converter;

        public MalishService(DeliveryKioskApiContext dbContext)
        {
            _dbContext = dbContext;
            _converter = new MalishConverter();
        }

        public async Task SaveCategories(List<MalishCategory> categories)
        {
            var result = await _converter.ConvertCategoriesAsync(categories);

            var request = new QueueRequest()
            {
                Id = Guid.NewGuid(),
                RequestName = MalishFileNames.Kls,
                RequestDate = DateTime.Now,
                IsProcessed = false,
                IdOrganization = Organisations.MalishId, // Малыш
                Answer = JsonConvert.SerializeObject(result),
            };
            await _dbContext.QueueRequests.AddAsync(request);
            await _dbContext.SaveChangesAsync();
        }
        
        public async Task SaveProducts(List<MalishProduct> products)
        {
            var result = await _converter.ConvertProductsAsync(products);

            var request = new QueueRequest()
            {
                Id = Guid.NewGuid(),
                RequestName = MalishFileNames.Goods,
                RequestDate = DateTime.Now,
                IsProcessed = false,
                IdOrganization = Organisations.MalishId, // Малыш
                Answer = JsonConvert.SerializeObject(result),
            };
            await _dbContext.QueueRequests.AddAsync(request);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<MalishResponseData> GetNomenclature()
        {
            var categories = _dbContext.QueueRequests
                .OrderByDescending(n => n.RequestDate)
                .FirstOrDefault(n =>
                    n.IdOrganization == Organisations.MalishId && 
                    n.RequestDate.Date == DateTime.Today.Date && 
                    n.IsProcessed == false && 
                    n.RequestName == MalishFileNames.Kls);

            var products = _dbContext.QueueRequests
                .OrderByDescending(n => n.RequestDate)
                .FirstOrDefault(n =>
                    n.IdOrganization == Organisations.MalishId && 
                    n.RequestDate.Date == DateTime.Today.Date && 
                    n.IsProcessed == false && 
                    n.RequestName == MalishFileNames.Goods);

            if (string.IsNullOrEmpty(products?.Answer) || string.IsNullOrEmpty(categories?.Answer))
                throw new Exception("Одна или несколько записей номенклатуры отсутствуют или уже были загружены.");
            
            var nomenclature = await _converter.ConvertNomenclatureAsync(categories.Answer, products.Answer);
/*
            categories.IsProcessed = true;
            categories.AnswerDate = DateTime.Now;
            products.IsProcessed = true;
            products.AnswerDate = DateTime.Now;*/

            await _dbContext.SaveChangesAsync();
            return nomenclature;
        }
    }
}