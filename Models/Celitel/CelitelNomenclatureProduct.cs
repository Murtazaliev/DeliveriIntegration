namespace Delivery.SelfServiceKioskApi.Models.Celitel
{
    public class CelitelNomenclatureProduct
    {
        public string ExternalId { get; set; }
        public string ExternalCategoryId { get; set; }
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public decimal OldPrice { get; set; }
        public string Weight { get; set; }
        public int Quantity { get; set; }
        public bool IsVisible { get; set; }
    }
}