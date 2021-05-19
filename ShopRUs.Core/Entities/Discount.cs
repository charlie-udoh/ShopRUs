namespace ShopRUs.Core.Entities
{
    public class Discount : BaseEntity
    {
        public string DiscountAppliesTo { get; set; }
        public decimal DiscountValue { get; set; }
        public string DiscountValueType { get; set; }
        public decimal Units { get; set; }
    }
}
