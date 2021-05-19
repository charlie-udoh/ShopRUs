using ShopRUs.Core.Entities;
using System.Threading.Tasks;

namespace ShopRUs.Core.Interfaces.Data
{
    public interface IDiscountRepository : IRepository<Discount>
    {
        Task<Discount> GetByType(string type);
    }
}