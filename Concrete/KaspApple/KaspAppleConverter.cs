using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Delivery.SelfServiceKioskApi.Models.GreenApple;
using Newtonsoft.Json;
using GreenAppleModels = Delivery.SelfServiceKioskApi.Models.GreenApple.GreenAppleModels;
using DeliveryModels = Delivery.SelfServiceKioskApi.Models.Delivery;

namespace Delivery.SelfServiceKioskApi.Concrete.GreenApple
{
    public class KaspAppleConverter
    {
        public async Task<GreenAppleResponseData> ConvertNomenclatureAsync(string sectionsJson,
            string categoriesJson,
            string productsJson)
        {
            return await Task.Run(() =>
            {
                var sections = JsonConvert.DeserializeObject<List<GreenAppleNomenclatureCategory>>(sectionsJson);
                var categories = JsonConvert.DeserializeObject<List<GreenAppleNomenclatureCategory>>(categoriesJson);
                var products = JsonConvert.DeserializeObject<List<GreenAppleNomenclatureProduct>>(productsJson);

                var nomenclature = new GreenAppleResponseData()
                {
                    Sections = sections,
                    ProductCategories = categories,
                    Products = products
                };
                return nomenclature;
            });
        }

        public async Task<List<GreenAppleNomenclatureCategory>> ConvertSectionsAsync(List<GreenAppleModels.GreenAppleSection> sections)
        {
            return await Task.Run(() =>
            {
                List<GreenAppleNomenclatureCategory> productCategories = new List<GreenAppleNomenclatureCategory>();
                sections.ForEach(section =>
                {
                    GreenAppleNomenclatureCategory productCategory = new GreenAppleNomenclatureCategory()
                    {
                        ExternalId = section.ExternalId,
                        CategoryPriority = section.Sort,
                        CategoryName = section.Name
                    };
                    productCategories.Add(productCategory);
                });
                return productCategories;
            });
        }
        
        public async Task<List<GreenAppleNomenclatureCategory>> ConvertCategoriesAsync(List<GreenAppleModels.GreenAppleCategory> categories)
        {
            return await Task.Run(() =>
            {
                List<GreenAppleNomenclatureCategory> productCategories = new List<GreenAppleNomenclatureCategory>();
                categories.ForEach(category =>
                {
                    GreenAppleNomenclatureCategory productCategory = new GreenAppleNomenclatureCategory()
                    {
                        ExternalId = category.ExternalId,
                        CategoryPriority = category.Sort,
                        CategoryName = category.NameRu,
                        ExternalParentId = category.ExternalSectionId
                    };
                    productCategories.Add(productCategory);
                });
                return productCategories;
            });
        }
        
        public async Task<List<GreenAppleNomenclatureProduct>> ConvertProductsAsync(List<GreenAppleModels.GreenAppleProduct> products)
        {
            return await Task.Run(() =>
            {
                List<GreenAppleNomenclatureProduct> deliveryProducts = new List<GreenAppleNomenclatureProduct>();
                products.ForEach(product =>
                {
                    GreenAppleNomenclatureProduct deliveryProduct = new GreenAppleNomenclatureProduct()
                    {
                        ExternalId = product.ExternalId,
                        ExternalCategoryId = product.ExternalCategoryId,
                        Name = product.NameRu,
                        Cost = product.Cost,
                        OldPrice = product.OldPrice,
                        IsVisible = !product.Hidden,
                    };
                    
                    deliveryProducts.Add(deliveryProduct);
                });
                return deliveryProducts;
            });
        }
    }
}
