using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopRUs.Core.Entities;

namespace ShopRUs.Infrastructure.Data.EntityConfigurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Name).IsRequired(true).HasMaxLength(100);
            builder.Property(s => s.Email).IsRequired(true).HasMaxLength(100);
            builder.Property(s => s.Address).IsRequired(true).HasMaxLength(1000);
            builder.Property(s => s.PhoneNumber).IsRequired(true).HasMaxLength(15);
            builder.Property(s => s.Role).IsRequired(true).HasMaxLength(50);
            builder.Property(s => s.DateRegistered).IsRequired(true);
            builder.HasData(new Customer
            {
                Id = 1,
                Name = "John",
                Address = "xyz stree, ABC City",
                Email = "abc@123.com",
                PhoneNumber = "12345678901",
                DateRegistered = new System.DateTime(2020, 01, 01),
                Role = "Affiliate"
            },
            new Customer
            {
                Id = 2,
                Name = "Paul",
                Address = "xyz stree, ABC City",
                Email = "abc@123.com",
                PhoneNumber = "12345678901",
                DateRegistered = new System.DateTime(2019, 05, 01),
                Role = "Employee"
            },
            new Customer
            {
                Id = 3,
                Name = "Jane",
                Address = "xyz stree, ABC City",
                Email = "abc@123.com",
                PhoneNumber = "12345678901",
                DateRegistered = new System.DateTime(2019, 01, 01),
                Role = "Customer"
            }
            );
        }
    }
}
