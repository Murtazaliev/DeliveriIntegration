using System.Collections.Generic;
using Delivery.SelfServiceKioskApi.Models.Delivery;
using Delivery.SelfServiceKioskApi.Models.GreenApple.GreenAppleModels;
using Product = Delivery.SelfServiceKioskApi.Models.Delivery.Product;

namespace Delivery.SelfServiceKioskApi.Models.GreenApple
{
    public class GreenAppleResponseData
    {
        public List<GreenAppleNomenclatureCategory> Sections { get; set; }
        public List<GreenAppleNomenclatureCategory> ProductCategories { get; set; }
        public List<GreenAppleNomenclatureProduct> Products { get; set; }
    }
}