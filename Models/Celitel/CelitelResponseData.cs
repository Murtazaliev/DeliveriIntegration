using System.Collections.Generic;

namespace Delivery.SelfServiceKioskApi.Models.Celitel
{
    public class CelitelResponseData
    {
        public List<CelitelNomenclatureCategory> ProductCategories { get; set; }
        public List<CelitelNomenclatureProduct> Products { get; set; }
    }
}

