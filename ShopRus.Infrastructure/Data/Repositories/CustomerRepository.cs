using Microsoft.EntityFrameworkCore;
using ShopRUs.Core.Entities;
using ShopRUs.Core.Interfaces.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopRUs.Infrastructure.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private AppDbContext _dbContext;

        public CustomerRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Customer>> GetAll()
        {
            return await _dbContext.Customers.ToListAsync();
        }

        public async Task<Customer> GetById(object id)
        {
            return await _dbContext.Customers.FindAsync(id);
        }

        public async Task<Customer> GetByName(string name)
        {
            return await _dbContext.Customers.FirstOrDefaultAsync(c => c.Name.ToLower() == name.Trim().ToLower());
        }

        public async Task Insert(Customer entity)
        {
            await _dbContext.Customers.AddAsync(entity);
        }
    }
}
