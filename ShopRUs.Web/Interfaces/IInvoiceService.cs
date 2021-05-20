using ShopRUs.Web.Models;
using System.Threading.Tasks;

namespace ShopRUs.Web.Services
{
    public interface IInvoiceService
    {
        Task<InvoiceDTO> GetInvoiceById(int id);
        Task<ServiceResponse> CreateInvoice(InvoiceDTO bill);
    }
}
