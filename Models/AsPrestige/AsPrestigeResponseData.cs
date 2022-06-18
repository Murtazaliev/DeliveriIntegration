using System.Collections.Generic;
using Delivery.SelfServiceKioskApi.Models.Malish;

namespace Delivery.SelfServiceKioskApi.Models.AsPrestige
{
    public class AsPrestigeResponseData
    {
        public List<AsPrestigeNomenclatureCategory> ProductCategories { get; set; }
        public List<AsPrestigeNomenclatureProduct> Products { get; set; }
    }
}

