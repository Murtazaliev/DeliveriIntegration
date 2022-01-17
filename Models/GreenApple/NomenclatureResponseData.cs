using System.Collections.Generic;
using Delivery.SelfServiceKioskApi.Models.Delivery;
using Delivery.SelfServiceKioskApi.Models.GreenApple.GreenAppleModels;
using Product = Delivery.SelfServiceKioskApi.Models.Delivery.Product;

namespace Delivery.SelfServiceKioskApi.Models.GreenApple
{
    public class NomenclatureResponseData
    {
        public List<NomenclatureCategory> Sections { get; set; }
        public List<NomenclatureCategory> ProductCategories { get; set; }
        public List<NomenclatureProduct> Products { get; set; }
    }
}