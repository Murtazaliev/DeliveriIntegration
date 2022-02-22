using System.Collections.Generic;
using Delivery.SelfServiceKioskApi.Models.GreenApple;

namespace Delivery.SelfServiceKioskApi.Models.Malish
{
    public class MalishResponseData
    {
        public List<MalishNomenclatureCategory> ProductCategories { get; set; }
        public List<MalishNomenclatureProduct> Products { get; set; }
    }
}

