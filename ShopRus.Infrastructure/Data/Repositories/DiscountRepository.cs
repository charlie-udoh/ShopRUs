using Microsoft.EntityFrameworkCore;
using ShopRUs.Core.Entities;
using ShopRUs.Core.Interfaces.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopRUs.Infrastructure.Data.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private AppDbContext _dbContext;

        public DiscountRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Discount>> GetAll()
        {
            return await _dbContext.Discounts.ToListAsync();
        }

        public async Task<Discount> GetById(object id)
        {
            return await _dbContext.Discounts.FindAsync(id);
        }

        public async Task<Discount> GetByType(string type)
        {
            return await _dbContext.Discounts.FirstOrDefaultAsync(c => c.DiscountAppliesTo.ToLower() == type.Trim().ToLower());
        }

        public async Task Insert(Discount entity)
        {
            await _dbContext.Discounts.AddAsync(entity);
        }
    }
}
