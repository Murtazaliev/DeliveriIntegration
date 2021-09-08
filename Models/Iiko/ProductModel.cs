using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Delivery.SelfServiceKioskApi.Models.Iiko
{
    public class ProductModel
    {
        public string additionalInfo { get; set; }
        public string code { get; set; }
        public string description { get; set; }
        public Guid id { get; set; }
        public bool? isDeleted { get; set; }
        public string name { get; set; }
        public string seoDescription { get; set; }
        public string seoKeywords { get; set; }
        public string seoText { get; set; }
        public string seoTitle { get; set; }
        public List<string> tags { get; set; }
        public int? carbohydrateAmount { get; set; }
        public int? carbohydrateFullAmount { get; set; }
        public List<DifferentPricesOn> differentPricesOn { get; set; }
        public bool? doNotPrintInCheque { get; set; }
        public int? energyAmount { get; set; }
        public int? energyFullAmount { get; set; }
        public int? fatAmount { get; set; }
        public int? fatFullAmount { get; set; }
        public int? fiberFullAmount { get; set; }
        public Guid? groupId { get; set; }
        public List<GroupModifiers> groupModifiers { get; set; }
        public string measureUnit { get; set; }
        public List<Modifiers> modifiers { get; set; }
        public int? price { get; set; }
        public string productCategoryId { get; set; }
        public List<ProhibitedToSaleOn> prohibitedToSaleOn { get; set; }
        public string type { get; set; }
        public bool? useBalanceForSell { get; set; }
        public double weight { get; set; }
        public List<Images> images { get; set; }
        public bool? isIncludedInMenu { get; set; }
        public int? order { get; set; }
        public string parentGroup { get; set; }
        public int? warningType { get; set; }
    }

    public class ProhibitedToSaleOn
    {
        public Guid terminalId { get; set; }
    }
    public class DifferentPricesOn
    {
        public decimal? price { get; set; }
        public Guid? priceCategory { get; set; }
        public Guid? terminalId { get; set; }
    }
    public class Modifiers
    {
        public int? maxAmount { get; set; }
        public int? minAmount { get; set; }
        public Guid? modifierId { get; set; }
        public bool? required { get; set; }
        public int? defaultAmount { get; set; }
    }

    public class GroupModifiers
    {
        public int? maxAmount { get; set; }
        public int? minAmount { get; set; }
        public Guid? modifierId { get; set; }
        public bool? required { get; set; }
        public List<ChildModifiers> childModifiers { get; set; }
        public bool childModifiersHaveMinMaxRestrictions { get; set; }
    }
    public class ChildModifiers
    {
        public int? maxAmount { get; set; }
        public int? minAmount { get; set; }
        public Guid? modifierId { get; set; }
        public bool? required { get; set; }
        public int? defaultAmount { get; set; }
        public bool hideIfDefaultAmount { get; set; }
    }
}
