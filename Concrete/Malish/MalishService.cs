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

        public async Task SaveSections(List<GreenAppleSection> sections)
        {
            
        }
        
        public async Task SaveCategories(List<MalishCategory> categories)
        {
            
        }
        
        public async Task SaveProducts(List<MalishProduct> products)
        {
            
        }

        public async Task<MalishResponseData> GetNomenclature()
        {
            return null;
        }
    }
}