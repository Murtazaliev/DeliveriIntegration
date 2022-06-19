using System.Collections.Generic;
using System.Threading.Tasks;
using Delivery.SelfServiceKioskApi.Models.AsPrestige;
using Delivery.SelfServiceKioskApi.Models.AsPrestige.AsPrestigeModels;
using Delivery.SelfServiceKioskApi.Models.GreenApple;
using Delivery.SelfServiceKioskApi.Models.Malish;
using Delivery.SelfServiceKioskApi.Models.Malish.MalishModels;
using Newtonsoft.Json;
using GreenAppleModels = Delivery.SelfServiceKioskApi.Models.GreenApple.GreenAppleModels;

namespace Delivery.SelfServiceKioskApi.Concrete.Malish
{
    public class AsPrestigeConverter
    {
        public async Task<AsPrestigeResponseData> ConvertNomenclatureAsync(List<AsPrestigeNomenclatureCategory> categories, List<AsPrestigeNomenclatureProduct> products)
        {
            return await Task.Run(() =>
            {
                var nomenclature = new AsPrestigeResponseData()
                {
                    ProductCategories = categories,
                    Products = products
                };
                return nomenclature;
            });
        }
        
        public async Task<List<AsPrestigeNomenclatureCategory>> ConvertCategoriesAsync(string categoriesJson)
        {
            return await Task.Run(() =>
            {
                var categories = JsonConvert.DeserializeObject<List<AsPrestigeCategory>>(categoriesJson);
                List<AsPrestigeNomenclatureCategory> productCategories = new List<AsPrestigeNomenclatureCategory>();
                categories.ForEach(category =>
                {
                    AsPrestigeNomenclatureCategory productCategory = new AsPrestigeNomenclatureCategory()
                    {
                        ExternalId = category.ExternalCategoryId,
                        CategoryName = category.NameRu,
                    };
                    productCategories.Add(productCategory);
                });
                return productCategories;
            });
        }
        
        public async Task<List<AsPrestigeNomenclatureProduct>> ConvertProductsAsync(string productsJson)
        {
            return await Task.Run(() =>
            {
                var products = JsonConvert.DeserializeObject<List<AsPrestigeProduct>>(productsJson);
                List <AsPrestigeNomenclatureProduct> deliveryProducts = new List<AsPrestigeNomenclatureProduct>();
                if (products != null)
                    products.ForEach(product =>
                    {
                        AsPrestigeNomenclatureProduct deliveryProduct = new AsPrestigeNomenclatureProduct()
                        {
                            ExternalId = product.ExternalId,
                            ExternalCategoryId = product.ExternalCategoryId,
                            Name = product.NameRu,
                            Cost = product.Cost,
                            OldPrice = product.OldPrice,
                            Quantity = product.Quantity,
                            Weight = product.Weight,
                            IsVisible = !product.Hidden
                        };

                        deliveryProducts.Add(deliveryProduct);
                    });
                return deliveryProducts;
            });
        }
    }
}
