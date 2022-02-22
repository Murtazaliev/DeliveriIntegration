namespace Delivery.SelfServiceKioskApi.Models.GreenApple
{
    public class GreenAppleNomenclatureProduct
    {
        public string ExternalId { get; set; }
        public string ExternalCategoryId { get; set; }
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public decimal OldPrice { get; set; }
        public bool IsVisible { get; set; }
    }
}