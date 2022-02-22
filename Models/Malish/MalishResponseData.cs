using System.Collections.Generic;
using Delivery.SelfServiceKioskApi.Models.GreenApple;

namespace Delivery.SelfServiceKioskApi.Models.Malish
{
    public class MalishResponseData
    {
        public List<NomenclatureCategory> ProductCategories { get; set; }
        public List<NomenclatureProduct> Products { get; set; }
    }
}

