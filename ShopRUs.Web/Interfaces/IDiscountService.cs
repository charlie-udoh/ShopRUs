using ShopRUs.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopRUs.Web.Interfaces
{
    public interface IDiscountService
    {
        Task<IEnumerable<DiscountDTO>> GetAllDiscounts();
        Task<DiscountDTO> GetDiscountById(int id);
        Task<DiscountDTO> GetDiscountByType(string type);
        Task<DiscountDTO> CreateDiscount(DiscountDTO discount);
    }
}
