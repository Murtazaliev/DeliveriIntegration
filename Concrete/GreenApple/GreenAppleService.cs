using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Delivery.SelfServiceKioskApi.DbModel;
using Delivery.SelfServiceKioskApi.Helpers;
using Delivery.SelfServiceKioskApi.Models.GreenApple;
using Delivery.SelfServiceKioskApi.Models.GreenApple.GreenAppleModels;
using Newtonsoft.Json;

namespace Delivery.SelfServiceKioskApi.Concrete.GreenApple
{
    public class GreenAppleService
    {
        private DeliveryKioskApiContext _dbContext;
        private GreenAppleConverter _converter;

        public GreenAppleService(DeliveryKioskApiContext dbContext)
        {
            _dbContext = dbContext;
            _converter = new GreenAppleConverter();
        }

        public async Task SaveSections(List<GreenAppleSection> sections)
        {
            var result = await _converter.ConvertSectionsAsync(sections);

            var request = new QueueRequest()
            {
                Id = Guid.NewGuid(),
                RequestName = FileNames.GreenAppleFileNames.Sections,
                RequestDate = DateTime.Now,
                IsProcessed = false,
                IdOrganization = Organisations.GreenAppleId, // Зеленое яблоко
                Answer = JsonConvert.SerializeObject(result),
            };
            await _dbContext.QueueRequests.AddAsync(request);
            await _dbContext.SaveChangesAsync();
        }
        
        public async Task SaveCategories(List<GreenAppleCategory> categories)
        {
            var result = await _converter.ConvertCategoriesAsync(categories);

            var request = new QueueRequest()
            {
                Id = Guid.NewGuid(),
                RequestName = FileNames.GreenAppleFileNames.Categories,
                RequestDate = DateTime.Now,
                IsProcessed = false,
                IdOrganization = Organisations.GreenAppleId, // Зеленое яблоко
                Answer = JsonConvert.SerializeObject(result),
            };
            await _dbContext.QueueRequests.AddAsync(request);
            await _dbContext.SaveChangesAsync();
        }
        
        public async Task SaveProducts(List<GreenAppleProduct> products)
        {
            var result = await _converter.ConvertProductsAsync(products);

            var request = new QueueRequest()
            {
                Id = Guid.NewGuid(),
                RequestName = FileNames.GreenAppleFileNames.Products,
                RequestDate = DateTime.Now,
                IsProcessed = false,
                IdOrganization = Organisations.GreenAppleId, // Зеленое яблоко
                Answer = JsonConvert.SerializeObject(result),
            };
            await _dbContext.QueueRequests.AddAsync(request);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<GreenAppleResponseData> GetNomenclature()
        {
            var sections = _dbContext.QueueRequests
                .OrderByDescending(n => n.RequestDate)
                .FirstOrDefault(n =>
                    n.IdOrganization == Organisations.GreenAppleId && 
                    n.RequestDate.Date == DateTime.Today.Date && 
                    n.IsProcessed == false && 
                    n.RequestName == FileNames.GreenAppleFileNames.Sections);

            var categories = _dbContext.QueueRequests
                .OrderByDescending(n => n.RequestDate)
                .FirstOrDefault(n =>
                    n.IdOrganization == Organisations.GreenAppleId && 
                    n.RequestDate.Date == DateTime.Today.Date && 
                    n.IsProcessed == false && 
                    n.RequestName == FileNames.GreenAppleFileNames.Categories);

            var products = _dbContext.QueueRequests
                .OrderByDescending(n => n.RequestDate)
                .FirstOrDefault(n =>
                    n.IdOrganization == Organisations.GreenAppleId && 
                    n.RequestDate.Date == DateTime.Today.Date && 
                    n.IsProcessed == false && 
                    n.RequestName == FileNames.GreenAppleFileNames.Products);

            if (string.IsNullOrEmpty(sections?.Answer) || string.IsNullOrEmpty(products?.Answer) || string.IsNullOrEmpty(categories?.Answer))
                throw new Exception("Одна или несколько записей номенклатуры отсутствуют или уже были загружены.");
            
            var nomenclature = await _converter.ConvertNomenclatureAsync(sections.Answer, categories.Answer, products.Answer);
/*
            sections.IsProcessed = true;
            sections.AnswerDate = DateTime.Now;
            categories.IsProcessed = true;
            categories.AnswerDate = DateTime.Now;
            products.IsProcessed = true;
            products.AnswerDate = DateTime.Now;*/

            await _dbContext.SaveChangesAsync();
            return nomenclature;
        }
    }
}