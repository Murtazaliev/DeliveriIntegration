using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delivery.SelfServiceKioskApi.Concrete.Malish;
using Delivery.SelfServiceKioskApi.DbModel;
using Delivery.SelfServiceKioskApi.Helpers;
using Delivery.SelfServiceKioskApi.Models.AsPrestige;
using Delivery.SelfServiceKioskApi.Models.AsPrestige.AsPrestigeModels;
using Newtonsoft.Json;

namespace Delivery.SelfServiceKioskApi.Concrete.AsPrestige
{
    public class AsPrestigeService
    {
        private DeliveryKioskApiContext _dbContext;
        private AsPrestigeConverter _converter;

        public AsPrestigeService(DeliveryKioskApiContext dbContext)
        {
            _dbContext = dbContext;
            _converter = new AsPrestigeConverter();
        }

        public async Task SaveCategories(List<AsPrestigeCategory> categories)
        {
            var answer = JsonConvert.SerializeObject(categories);
            var request = new QueueRequest()
            {
                Id = Guid.NewGuid(),
                RequestName = FileNames.AsPrestigeFileNames.AsCategories,
                RequestDate = DateTime.Now,
                IsProcessed = false,
                IdOrganization = Organisations.AsPrestigeId, // Ас-Престиж
                Answer = DecodeToUtf8(answer),
            };
            await _dbContext.QueueRequests.AddAsync(request);
            await _dbContext.SaveChangesAsync();
        }
        
        public async Task SaveProducts(List<AsPrestigeProduct> products)
        {
            var answer = JsonConvert.SerializeObject(products);
            var request = new QueueRequest()
            {
                Id = Guid.NewGuid(),
                RequestName = FileNames.AsPrestigeFileNames.AsProducts,
                RequestDate = DateTime.Now,
                IsProcessed = false,
                IdOrganization = Organisations.AsPrestigeId, // Ас-Престиж
                Answer = answer,
            };
            await _dbContext.QueueRequests.AddAsync(request);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<AsPrestigeResponseData> GetNomenclature()
        {
            var categories = _dbContext.QueueRequests
                .OrderByDescending(n => n.RequestDate)
                .FirstOrDefault(n =>
                    n.IdOrganization == Organisations.AsPrestigeId && 
                    n.RequestDate.Date == DateTime.Today.Date && 
                    n.IsProcessed == false && 
                    n.RequestName == FileNames.AsPrestigeFileNames.AsCategories);

            var products = _dbContext.QueueRequests
                .OrderByDescending(n => n.RequestDate)
                .FirstOrDefault(n =>
                    n.IdOrganization == Organisations.AsPrestigeId && 
                    n.RequestDate.Date == DateTime.Today.Date && 
                    n.IsProcessed == false && 
                    n.RequestName == FileNames.AsPrestigeFileNames.AsProducts);

            if (string.IsNullOrEmpty(products?.Answer) || string.IsNullOrEmpty(categories?.Answer))
                throw new Exception("Одна или несколько записей номенклатуры отсутствуют или уже были загружены.");
            
            var productsAnswer = await _converter.ConvertProductsAsync(products?.Answer);
            var categoriesAnswer = await _converter.ConvertCategoriesAsync(categories?.Answer);

            var nomenclature = await _converter.ConvertNomenclatureAsync(categoriesAnswer, productsAnswer);
/*
            categories.IsProcessed = true;
            categories.AnswerDate = DateTime.Now;
            products.IsProcessed = true;
            products.AnswerDate = DateTime.Now;*/

            await _dbContext.SaveChangesAsync();
            return nomenclature;
        }

        private string DecodeToUtf8(string text)
        {
            Encoding utf8 = Encoding.GetEncoding("UTF-8");
            Encoding win1251 = Encoding.GetEncoding("Windows-1251");

            byte[] utf8Bytes = win1251.GetBytes(text);
            byte[] win1251Bytes = Encoding.Convert(utf8, win1251, utf8Bytes);
            return win1251.GetString(win1251Bytes);
        }
    }
}