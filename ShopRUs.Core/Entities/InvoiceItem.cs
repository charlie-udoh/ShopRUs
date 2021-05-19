namespace ShopRUs.Core.Entities
{
    public class InvoiceItem : BaseEntity
    {
        public string Description { get; set; }
        public string Category { get; set; }
        public int Units { get; set; }
        public decimal UnitPrice { get; set; }
        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }
    }
}
