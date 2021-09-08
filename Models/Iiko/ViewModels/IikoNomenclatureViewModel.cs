using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Delivery.SelfServiceKioskApi.Models.Iiko.ViewModels
{
    public class IikoNomenclatureViewModel
    {
        public List<GroupModel> groups { get; set; }
        public List<ProductCategoriesModel> productCategories { get; set; }
        public List<ProductModel> products { get; set; }
        public int? revision { get; set; }
        public string uploadDate { get; set; }
    }
}
