using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopRUs.Core.Entities;

namespace ShopRUs.Infrastructure.Data.EntityConfigurations
{
    public class InvoiceItemConfiguration : IEntityTypeConfiguration<InvoiceItem>
    {
        public void Configure(EntityTypeBuilder<InvoiceItem> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Description).IsRequired(true).HasMaxLength(10);
            builder.Property(s => s.Units).IsRequired(true);
            builder.Property(s => s.UnitPrice).IsRequired(true).HasPrecision(18, 2);
            builder.Property(s => s.Category).IsRequired(false).HasMaxLength(100);
        }
    }
}
