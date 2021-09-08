using Delivery.SelfServiceKioskApi.Models.Delivery;
using Delivery.SelfServiceKioskApi.Models.Delivery.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Delivery.SelfServiceKioskApi.Domain
{
    public abstract class IConverter
    {
        #region [[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[Реализация в производных классах]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]
        public abstract string ConverterResponseNomenclatureData<T>(T entity, DateTime createRequestDate, DateTime createResponseDate, Guid partnerId, Guid requestId, Guid categoryId) where T : class;

        public abstract string ConverterOrderForKiosk(CreateOrderRequestData data);
        #endregion

        #region [[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[Реализация по умолчанию для всех]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]

         List<ProductCategory> Categories;
        public void ResponseModel(GetPartnerProductsResponseData data, Guid categoryId)
        {
            var pcList = data.ProductCategories.Where(x => x.ParentId == categoryId).ToList();
            if (pcList != null)
            {
                ResponseModelBuilder(pcList);
            }
        }

        public void ResponseModelBuilder(List<ProductCategory> productCategories)
        {
            List<ProductCategory> subCategoryList = new List<ProductCategory>();

            foreach (var item in productCategories)
            {
                Categories.Add(item);
                if (productCategories.Where(x => x.ParentId == item.Id).Count() > 0)
                {
                    subCategoryList.AddRange(productCategories.Where(x => x.ParentId == item.Id).ToList());
                }
            }
            if (subCategoryList.Count() > 0)
            {
                ResponseModelBuilder(subCategoryList);
            }

        }
        #endregion
    }
}
