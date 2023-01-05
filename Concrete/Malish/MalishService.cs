using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            var answer = JsonConvert.SerializeObject(categories);
            var request = new QueueRequest()
            {
                Id = Guid.NewGuid(),
                RequestName = FileNames.MalishFileNames.Kls,
                RequestDate = DateTime.Now,
                IsProcessed = false,
                IdOrganization = Organisations.MalishId, // Малыш
                Answer = (answer),
            };
            await _dbContext.QueueRequests.AddAsync(request);
            await _dbContext.SaveChangesAsync();
        }
        
        public async Task SaveProducts(List<MalishProduct> products)
        {
            var answer = JsonConvert.SerializeObject(products);
            var request = new QueueRequest()
            {
                Id = Guid.NewGuid(),
                RequestName = FileNames.MalishFileNames.Goods,
                RequestDate = DateTime.Now,
                IsProcessed = false,
                IdOrganization = Organisations.MalishId, // Малыш
                Answer = (answer),
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
                    n.RequestDate >= DateTime.Today - TimeSpan.FromHours(2) && 
                    n.IsProcessed == false && 
                    n.RequestName == FileNames.MalishFileNames.Kls);

            var products = _dbContext.QueueRequests
                .OrderByDescending(n => n.RequestDate)
                .FirstOrDefault(n =>
                    n.IdOrganization == Organisations.MalishId && 
                    n.RequestDate >= DateTime.Today - TimeSpan.FromHours(2) && 
                    n.IsProcessed == false && 
                    n.RequestName == FileNames.MalishFileNames.Goods);

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