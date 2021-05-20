using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopRUs.Core.Entities;

namespace ShopRUs.Infrastructure.Data.EntityConfigurations
{
    public class DiscountConfiguration : IEntityTypeConfiguration<Discount>
    {
        public void Configure(EntityTypeBuilder<Discount> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.DiscountAppliesTo).IsRequired(true).HasMaxLength(1000);
            builder.Property(s => s.DiscountValueType).IsRequired(true).HasMaxLength(1000);
            builder.Property(s => s.DiscountValue).IsRequired(true).HasPrecision(18, 2);
            builder.Property(s => s.Units).IsRequired(true).HasPrecision(18, 2).HasDefaultValue(0m);
            builder.HasData(new Discount
            {
                Id = 1,
                DiscountAppliesTo = "Affiliate",
                DiscountValueType = "Percentage",
                DiscountValue = 10
            },
            new Discount
            {
                Id = 2,
                DiscountAppliesTo = "Employee",
                DiscountValueType = "Percentage",
                DiscountValue = 30
            },
            new Discount
            {
                Id = 3,
                DiscountAppliesTo = "Loyalty",
                DiscountValueType = "Percentage",
                DiscountValue = 5
            },
            new Discount
            {
                Id = 4,
                DiscountAppliesTo = "General",
                DiscountValueType = "Value",
                DiscountValue = 5,
                Units = 100
            }
            );
        }
    }
}
