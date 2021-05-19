using ShopRUs.Core.Entities;
using ShopRUs.Core.Helpers;
using ShopRUs.Core.Interfaces.Data;
using ShopRUs.Core.Interfaces.DomainServices;
using System;
using System.Threading.Tasks;

namespace ShopRUs.Core.Services
{
    public class InvoiceDomainService : IInvoiceDomainService
    {
        private readonly IDiscountRepository _discountRepository;
        public InvoiceDomainService(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository;
        }

        public async Task<decimal> CalculateDiscount(decimal totalAmount, decimal totaAmountWithoutGroceries, string customerRole, DateTime customerRegistrationDate)
        {
            var discountAmount = 0m;
            var discount = new Discount();
            //Check how long user has been a customer
            var yearsRegistered = DateHelper.GetDateDifferenceInYears(customerRegistrationDate, DateTime.Now);

            if (customerRole == "Affiliate")
                discount = await _discountRepository.GetByType("Affiliate");
            else if (customerRole == "Employee")
                discount = await _discountRepository.GetByType("Employee");
            else if (yearsRegistered > 2.0)
                discount = await _discountRepository.GetByType("Loyalty");

            if (discount != null && discount.DiscountValueType == "Percentage")
                discountAmount = GetDiscountAmount(totaAmountWithoutGroceries, discount);

            //General discount
            discount = await _discountRepository.GetByType("General");
            if (discount != null)
                discountAmount += GetDiscountAmount(totalAmount, discount);

            return discountAmount;
        }

        private decimal GetDiscountAmount(decimal amount, Discount discount)
        {
            switch (discount.DiscountValueType)
            {
                case "Percentage":
                    return Math.Round(amount * (discount.DiscountValue / 100), 2);
                case "Value":
                    var slices = Math.Floor(amount / discount.Units);
                    return slices * discount.DiscountValue;
                default:
                    return 0m;
            }
        }
    }
}
