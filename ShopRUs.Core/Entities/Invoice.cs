using System;
using System.Collections.Generic;

namespace ShopRUs.Core.Entities
{
    public class Invoice : BaseEntity
    {
        public DateTime InvoiceDate { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public ICollection<InvoiceItem> InvoiceItems { get; set; }
    }
}
