namespace Delivery.SelfServiceKioskApi.Models.GreenApple
{
    public class NomenclatureCategory
    {
        public string ExternalId { get; set; }
        public string ExternalParentId { get; set; }
        public string CategoryName { get; set; }
        public int? CategoryPriority { get; set; }
    }
}