using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopRUs.Web.Models
{
    public class InvoiceDTO
    {
        [Range(1, int.MaxValue)]
        public int CustomerId { get; set; }
        public int InvoiceId { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal InvoiceTotal { get { return TotalAmount - DiscountAmount; } }
        public string Customer { get; set; }
        public string ShippingAddress { get; set; }
        public DateTime InvoiceDate { get; set; }
        public List<InvoiceItemDTO> Items { get; set; }
    }

    public class InvoiceItemDTO
    {
        public int ItemId { get; set; }
        [Required]
        public string ItemDescription { get; set; }
        [Required]
        public string ItemType { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "The field {0} must be greater than {1}.")]
        public int Quantity { get; set; }
        [Range(0.1, double.MaxValue, ErrorMessage = "The field {0} must be greater than {1}.")]
        public decimal UnitPrice { get; set; }
    }
}
