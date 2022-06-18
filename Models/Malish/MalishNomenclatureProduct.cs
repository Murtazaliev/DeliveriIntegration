namespace Delivery.SelfServiceKioskApi.Models.Malish
{
    public class MalishNomenclatureProduct
    {
        public string ExternalId { get; set; }
        public string ExternalCategoryId { get; set; }
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public decimal OldPrice { get; set; }
        public string StoreCode { get; set; }
        public int Quantity { get; set; }
        public bool IsVisible { get; set; }
    }
}