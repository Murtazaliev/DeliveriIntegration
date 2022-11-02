using System.Collections.Generic;
using System.Threading.Tasks;
using Delivery.SelfServiceKioskApi.Models.Celitel;
using Delivery.SelfServiceKioskApi.Models.Celitel.CelitelModels;
using Newtonsoft.Json;

namespace Delivery.SelfServiceKioskApi.Concrete.Celitel
{
    public class CelitelConverter
    {
        public async Task<CelitelResponseData> ConvertNomenclatureAsync(List<CelitelNomenclatureCategory> categories, List<CelitelNomenclatureProduct> products)
        {
            return await Task.Run(() =>
            {
                var nomenclature = new CelitelResponseData()
                {
                    ProductCategories = categories,
                    Products = products
                };
                return nomenclature;
            });
        }
        
        public async Task<List<CelitelNomenclatureCategory>> ConvertCategoriesAsync(string categoriesJson)
        {
            return await Task.Run(() =>
            {
                var categories = JsonConvert.DeserializeObject<List<CelitelCategory>>(categoriesJson);
                List<CelitelNomenclatureCategory> productCategories = new List<CelitelNomenclatureCategory>();
                categories.ForEach(category =>
                {
                    CelitelNomenclatureCategory productCategory = new CelitelNomenclatureCategory()
                    {
                        ExternalId = category.ExternalId.ToString(),
                        CategoryName = category.NameRu,
                    };
                    productCategories.Add(productCategory);
                });
                return productCategories;
            });
        }
        
        public async Task<List<CelitelNomenclatureProduct>> ConvertProductsAsync(string productsJson)
        {
            return await Task.Run(() =>
            {
                var products = JsonConvert.DeserializeObject<List<CelitelProduct>>(productsJson);
                List <CelitelNomenclatureProduct> deliveryProducts = new List<CelitelNomenclatureProduct>();
                if (products != null)
                    products.ForEach(product =>
                    {
                        CelitelNomenclatureProduct deliveryProduct = new CelitelNomenclatureProduct()
                        {
                            ExternalId = product.ExternalId.ToString(),
                            ExternalCategoryId = product.ExternalCategoryId is null ? "" : product.ExternalCategoryId.ToString(),
                            Name = product.NameRu,
                            Cost = product.Cost,
                            OldPrice = product.OldPrice,
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
