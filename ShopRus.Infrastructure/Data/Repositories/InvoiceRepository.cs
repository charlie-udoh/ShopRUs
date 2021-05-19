using Microsoft.EntityFrameworkCore;
using ShopRUs.Core.Entities;
using ShopRUs.Core.Interfaces.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopRUs.Infrastructure.Data.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private AppDbContext _dbContext;

        public InvoiceRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Invoice>> GetAll()
        {
            return await _dbContext.Invoices.ToListAsync();
        }

        public async Task<Invoice> GetById(object id)
        {
            return await _dbContext.Invoices.FindAsync(id);
        }

        public async Task Insert(Invoice entity)
        {
            await _dbContext.Invoices.AddAsync(entity);
        }
    }
}
