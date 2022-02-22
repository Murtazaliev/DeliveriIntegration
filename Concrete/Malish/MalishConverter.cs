using System.Collections.Generic;
using System.Threading.Tasks;
using Delivery.SelfServiceKioskApi.Models.GreenApple;
using Newtonsoft.Json;
using GreenAppleModels = Delivery.SelfServiceKioskApi.Models.GreenApple.GreenAppleModels;

namespace Delivery.SelfServiceKioskApi.Concrete.Malish
{
    public class MalishConverter
    {
        public async Task<GreenAppleResponseData> ConvertNomenclatureAsync(string sectionsJson,
            string categoriesJson,
            string productsJson)
        {
            return await Task.Run(() =>
            {
                var sections = JsonConvert.DeserializeObject<List<NomenclatureCategory>>(sectionsJson);
                var categories = JsonConvert.DeserializeObject<List<NomenclatureCategory>>(categoriesJson);
                var products = JsonConvert.DeserializeObject<List<NomenclatureProduct>>(productsJson);

                var nomenclature = new GreenAppleResponseData()
                {
                    Sections = sections,
                    ProductCategories = categories,
                    Products = products
                };
                return nomenclature;
            });
        }

        public async Task<List<NomenclatureCategory>> ConvertSectionsAsync(List<GreenAppleModels.GreenAppleSection> sections)
        {
            return await Task.Run(() =>
            {
                List<NomenclatureCategory> productCategories = new List<NomenclatureCategory>();
                sections.ForEach(section =>
                {
                    NomenclatureCategory productCategory = new NomenclatureCategory()
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
        
        public async Task<List<NomenclatureCategory>> ConvertCategoriesAsync(List<GreenAppleModels.GreenAppleCategory> categories)
        {
            return await Task.Run(() =>
            {
                List<NomenclatureCategory> productCategories = new List<NomenclatureCategory>();
                categories.ForEach(category =>
                {
                    NomenclatureCategory productCategory = new NomenclatureCategory()
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
        
        public async Task<List<NomenclatureProduct>> ConvertProductsAsync(List<GreenAppleModels.GreenAppleProduct> products)
        {
            return await Task.Run(() =>
            {
                List<NomenclatureProduct> deliveryProducts = new List<NomenclatureProduct>();
                products.ForEach(product =>
                {
                    NomenclatureProduct deliveryProduct = new NomenclatureProduct()
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
