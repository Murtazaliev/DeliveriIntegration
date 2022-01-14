using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using GreenAppleModels = Delivery.SelfServiceKioskApi.Models.GreenApple.GreenAppleModels;
using DeliveryModels = Delivery.SelfServiceKioskApi.Models.Delivery;

namespace Delivery.SelfServiceKioskApi.Concrete.GreenApple
{
    public class GreenAppleConverter
    {
        public async Task<List<DeliveryModels.ProductCategory>> ConvertNomenclatureAsync(string sectionsJson,
            string categoriesJson,
            string productsJson)
        {
            return await Task.Run(() =>
            {
                var sections = JsonConvert.DeserializeObject<List<DeliveryModels.ProductCategory>>(sectionsJson);
                var categories = JsonConvert.DeserializeObject<List<DeliveryModels.ProductCategory>>(categoriesJson);
                var products = JsonConvert.DeserializeObject<List<DeliveryModels.Product>>(productsJson);
                
                List<DeliveryModels.ProductCategory> productCategories = new List<DeliveryModels.ProductCategory>();
                categories.ForEach(category =>
                {
                    productCategories.Add(category);
                });
                sections.ForEach(section =>
                {
                    productCategories.Add(section);
                });
                products.ForEach(product =>
                {
                    var category = productCategories.FirstOrDefault(n => n.ExternalId == product.ExternalCategoryId);
                    category?.Products.Add(product);
                });
                return productCategories;
            });
        }

        public async Task<List<DeliveryModels.ProductCategory>> ConvertSectionsAsync(List<GreenAppleModels.Section> sections)
        {
            return await Task.Run(() =>
            {
                List<DeliveryModels.ProductCategory> productCategories = new List<DeliveryModels.ProductCategory>();
                sections.ForEach(section =>
                {
                    DeliveryModels.ProductCategory productCategory = new DeliveryModels.ProductCategory()
                    {
                        ExternalId = section.ExternalId,
                        CategoryPriority = section.Sort,
                        Name = section.Name
                    };
                    productCategories.Add(productCategory);
                });
                return productCategories;
            });
        }
        
        public async Task<List<DeliveryModels.ProductCategory>> ConvertCategoriesAsync(List<GreenAppleModels.Category> categories)
        {
            return await Task.Run(() =>
            {
                List<DeliveryModels.ProductCategory> productCategories = new List<DeliveryModels.ProductCategory>();
                categories.ForEach(category =>
                {
                    DeliveryModels.ProductCategory productCategory = new DeliveryModels.ProductCategory()
                    {
                        ExternalId = category.ExternalId,
                        CategoryPriority = category.Sort,
                        Name = category.NameRu,
                        ExternalParentId = category.ExternalSectionId
                    };
                    productCategories.Add(productCategory);
                });
                return productCategories;
            });
        }
        
        public async Task<List<DeliveryModels.Product>> ConvertProductsAsync(List<GreenAppleModels.Product> products)
        {
            return await Task.Run(() =>
            {
                List<DeliveryModels.Product> deliveryProducts = new List<DeliveryModels.Product>();
                products.ForEach(product =>
                {
                    DeliveryModels.Product deliveryProduct = new DeliveryModels.Product()
                    {
                        ExternalId = product.ExternalId,
                        ExternalCategoryId = product.ExternalCategoryId,
                        Name = product.NameRu,
                        Cost = product.Cost,
                        IsVisible = !product.Hidden,
                    };
                    
                    deliveryProducts.Add(deliveryProduct);
                });
                return deliveryProducts;
            });
        }
    }
}
