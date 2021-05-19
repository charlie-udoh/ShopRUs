using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopRUs.Core.Entities;

namespace ShopRUs.Infrastructure.Data.EntityConfigurations
{
    public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.TotalAmount).IsRequired(true).HasPrecision(18, 2);
            builder.Property(s => s.DiscountAmount).IsRequired(true).HasPrecision(18, 2);
            builder.Property(s => s.InvoiceDate).IsRequired(true);
            builder.HasMany(s => s.InvoiceItems).WithOne(s => s.Invoice);
            builder.HasOne(s => s.Customer).WithMany(s => s.Invoices);
        }
    }
}
