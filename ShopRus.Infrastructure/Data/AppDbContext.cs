using Microsoft.EntityFrameworkCore;
using ShopRUs.Core.Entities;
using ShopRUs.Infrastructure.Data.EntityConfigurations;

namespace ShopRUs.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        private readonly string connectionString;

        public AppDbContext(string connectionString) : base()
        {
            this.connectionString = connectionString;
        }

        public AppDbContext(DbContextOptions options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new InvoiceConfiguration());
            modelBuilder.ApplyConfiguration(new InvoiceItemConfiguration());
            modelBuilder.ApplyConfiguration(new DiscountConfiguration());
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceItem> InvoiceItems { get; set; }
        public DbSet<Discount> Discounts { get; set; }
    }
}
