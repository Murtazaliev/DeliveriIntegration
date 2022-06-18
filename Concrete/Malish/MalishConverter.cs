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
        public async Task<MalishResponseData> ConvertNomenclatureAsync(List<MalishNomenclatureCategory> categories, List<MalishNomenclatureProduct> products)
        {
            return await Task.Run(() =>
            {
                var nomenclature = new MalishResponseData()
                {
                    ProductCategories = categories,
                    Products = products
                };
                return nomenclature;
            });
        }
        
        public async Task<List<MalishNomenclatureCategory>> ConvertCategoriesAsync(string categoriesJson)
        {
            return await Task.Run(() =>
            {
                var categories = JsonConvert.DeserializeObject<List<MalishCategory>>(categoriesJson);
                List<MalishNomenclatureCategory> productCategories = new List<MalishNomenclatureCategory>();
                categories.ForEach(category =>
                {
                    MalishNomenclatureCategory productCategory = new MalishNomenclatureCategory()
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
        
        public async Task<List<MalishNomenclatureProduct>> ConvertProductsAsync(string productsJson)
        {
            return await Task.Run(() =>
            {
                var products = JsonConvert.DeserializeObject<List<MalishProduct>>(productsJson);
                List <MalishNomenclatureProduct> deliveryProducts = new List<MalishNomenclatureProduct>();
                products.ForEach(product =>
                {
                    MalishNomenclatureProduct deliveryProduct = new MalishNomenclatureProduct()
                    {
                        StoreCode = product.StoreCode,
                        ExternalId = product.SkuCode,
                        ExternalCategoryId = product.KlsUnicode,
                        Name = product.CmpName,
                        Cost = product.Price,
                        OldPrice = product.Discount != 0 ? product.Price + product.Discount : 0,
                        Quantity = product.Quantity,
                        IsVisible = product.Quantity > 0
                    };
                    
                    deliveryProducts.Add(deliveryProduct);
                });
                return deliveryProducts;
            });
        }
    }
}
