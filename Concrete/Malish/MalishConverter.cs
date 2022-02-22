using System.Collections.Generic;
using System.Threading.Tasks;
using Delivery.SelfServiceKioskApi.Models.GreenApple;
using Delivery.SelfServiceKioskApi.Models.Malish;
using Delivery.SelfServiceKioskApi.Models.Malish.MalishModels;
using Newtonsoft.Json;
using GreenAppleModels = Delivery.SelfServiceKioskApi.Models.GreenApple.GreenAppleModels;

namespace Delivery.SelfServiceKioskApi.Concrete.Malish
{
    public class MalishConverter
    {
        public async Task<MalishResponseData> ConvertNomenclatureAsync(string categoriesJson, string productsJson)
        {
            return null;
        }
        
        public async Task<List<GreenAppleNomenclatureCategory>> ConvertCategoriesAsync(List<MalishCategory> categories)
        {
            return await Task.Run(() =>
            {
                List<GreenAppleNomenclatureCategory> productCategories = new List<GreenAppleNomenclatureCategory>();
                categories.ForEach(category =>
                {
                    GreenAppleNomenclatureCategory productCategory = new GreenAppleNomenclatureCategory()
                    {
                        ExternalId = category.KlsUnicode,
                        CategoryName = category.KlsName,
                        ExternalParentId = category.KlsParent
                    };
                    productCategories.Add(productCategory);
                });
                return productCategories;
            });
        }
        
        public async Task<List<MalishNomenclatureProduct>> ConvertProductsAsync(List<MalishProduct> products)
        {
            return await Task.Run(() =>
            {
                List<MalishNomenclatureProduct> deliveryProducts = new List<MalishNomenclatureProduct>();
                products.ForEach(product =>
                {
                    MalishNomenclatureProduct deliveryProduct = new MalishNomenclatureProduct()
                    {
                        ExternalId = product.KlsUnicode,
                        ExternalCategoryId = product.KlsUnicode,
                        Name = product.CmpName,
                        Cost = product.Discount,
                        OldPrice = product.Price,
                    };
                    
                    deliveryProducts.Add(deliveryProduct);
                });
                return deliveryProducts;
            });
        }
    }
}
