using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShopRUs.Core.Interfaces.Data;
using ShopRUs.Core.Interfaces.DomainServices;
using ShopRUs.Core.Services;
using ShopRUs.Infrastructure.Data;
using ShopRUs.Infrastructure.Data.Repositories;
using ShopRUs.Web.Interfaces;
using ShopRUs.Web.Services;

namespace ShopRUs.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<AppDbContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("ShopRUs")));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IDiscountRepository, DiscountRepository>();
            services.AddScoped<IInvoiceRepository, InvoiceRepository>();

            services.AddScoped<IInvoiceDomainService, InvoiceDomainService>();

            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<IDiscountService, DiscountService>();
            services.AddTransient<IInvoiceService, InvoiceService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
