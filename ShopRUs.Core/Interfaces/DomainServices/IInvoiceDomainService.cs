using System;
using System.Threading.Tasks;

namespace ShopRUs.Core.Interfaces.DomainServices
{
    public interface IInvoiceDomainService
    {
        Task<decimal> CalculateDiscount(decimal totalAmount, decimal totaAmountWithoutGroceries, string customerRole, DateTime customerRegistrationDate);
    }
}