using ShopRUs.Core.Entities;
using System.Threading.Tasks;

namespace ShopRUs.Core.Interfaces.Data
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<Customer> GetByName(string name);
    }
}