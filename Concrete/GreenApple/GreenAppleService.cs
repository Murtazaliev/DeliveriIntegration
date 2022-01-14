using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Delivery.SelfServiceKioskApi.DbModel;
using Delivery.SelfServiceKioskApi.Helpers;
using Delivery.SelfServiceKioskApi.Models.GreenApple.GreenAppleModels;
using Newtonsoft.Json;
using Product = Delivery.SelfServiceKioskApi.Models.GreenApple.GreenAppleModels.Product;

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

        public async Task SaveSections(List<Section> sections)
        {
            var result = await _converter.ConvertSectionsAsync(sections);

            var request = new QueueRequest()
            {
                Id = Guid.NewGuid(),
                RequestName = GreenAppleFileNames.Sections,
                RequestDate = DateTime.Now,
                IsProcessed = false,
                IdOrganization = Organisations.GreenAppleId, // Зеленое яблоко
                Answer = JsonConvert.SerializeObject(result),
            };
            await _dbContext.QueueRequests.AddAsync(request);
            await _dbContext.SaveChangesAsync();
        }
        
        public async Task SaveCategories(List<Category> categories)
        {
            var result = await _converter.ConvertCategoriesAsync(categories);

            var request = new QueueRequest()
            {
                Id = Guid.NewGuid(),
                RequestName = GreenAppleFileNames.Categories,
                RequestDate = DateTime.Now,
                IsProcessed = false,
                IdOrganization = Organisations.GreenAppleId, // Зеленое яблоко
                Answer = JsonConvert.SerializeObject(result),
            };
            await _dbContext.QueueRequests.AddAsync(request);
            await _dbContext.SaveChangesAsync();
        }
        
        public async Task SaveProducts(List<Product> products)
        {
            var result = await _converter.ConvertProductsAsync(products);

            var request = new QueueRequest()
            {
                Id = Guid.NewGuid(),
                RequestName = GreenAppleFileNames.Products,
                RequestDate = DateTime.Now,
                IsProcessed = false,
                IdOrganization = Organisations.GreenAppleId, // Зеленое яблоко
                Answer = JsonConvert.SerializeObject(result),
            };
            await _dbContext.QueueRequests.AddAsync(request);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<string> GetNomenclature()
        {
            var sections = _dbContext.QueueRequests
                .OrderBy(n => n.RequestDate)
                .FirstOrDefault(n =>
                    n.IdOrganization == Organisations.GreenAppleId && 
                    n.RequestDate.Date == DateTime.Today.Date && 
                    n.IsProcessed == false && 
                    n.RequestName == GreenAppleFileNames.Sections);

            var categories = _dbContext.QueueRequests
                .OrderBy(n => n.RequestDate)
                .FirstOrDefault(n =>
                    n.IdOrganization == Organisations.GreenAppleId && 
                    n.RequestDate.Date == DateTime.Today.Date && 
                    n.IsProcessed == false && 
                    n.RequestName == GreenAppleFileNames.Categories);

            var products = _dbContext.QueueRequests
                .OrderBy(n => n.RequestDate)
                .FirstOrDefault(n =>
                    n.IdOrganization == Organisations.GreenAppleId && 
                    n.RequestDate.Date == DateTime.Today.Date && 
                    n.IsProcessed == false && 
                    n.RequestName == GreenAppleFileNames.Products);

            if (string.IsNullOrEmpty(sections?.Answer) || string.IsNullOrEmpty(products?.Answer) || string.IsNullOrEmpty(categories?.Answer))
                throw new Exception("Одна или несколько записей номенклатуры отсутствуют или уже были загружены.");
            
            var productCategories = await _converter.ConvertNomenclatureAsync(sections.Answer, categories.Answer, products.Answer);

            sections.IsProcessed = true;
            sections.AnswerDate = DateTime.Now;
            sections.Answer = String.Empty;
            categories.IsProcessed = true;
            categories.AnswerDate = DateTime.Now;
            categories.Answer = String.Empty;
            products.IsProcessed = true;
            products.AnswerDate = DateTime.Now;
            products.Answer = String.Empty;

            await _dbContext.SaveChangesAsync();
            var result = JsonConvert.SerializeObject(productCategories);
            return result;
        }
    }
}