using ShopRUs.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopRUs.Core.Interfaces.Data
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAll();

        Task<T> GetById(object id);

        Task Insert(T entity);
    }
}
