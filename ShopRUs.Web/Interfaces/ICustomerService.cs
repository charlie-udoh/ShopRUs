using ShopRUs.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopRUs.Web.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDTO>> GetAllCustomers();
        Task<CustomerDTO> GetCustomerById(int id);
        Task<CustomerDTO> GetCustomerByName(string name);
        Task<CustomerDTO> CreateCustomer(CustomerDTO customer);
    }
}
