namespace ShopRUs.Web.Models
{
    public class DiscountDTO
    {
        public int Id { get; set; }
        public string DiscountAppliesTo { get; set; }
        public string DiscountValueType { get; set; }
        public decimal DiscountValue { get; set; }
        public decimal Units { get; set; }
    }
}
