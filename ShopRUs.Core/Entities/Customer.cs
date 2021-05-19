using System;
using System.Collections.Generic;

namespace ShopRUs.Core.Entities
{
    public class Customer : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; }
        public DateTime DateRegistered { get; set; }
        public ICollection<Invoice> Invoices { get; set; }
    }
}
