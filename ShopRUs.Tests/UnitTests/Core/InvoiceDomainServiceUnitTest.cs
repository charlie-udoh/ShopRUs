using Moq;
using ShopRUs.Core.Entities;
using ShopRUs.Core.Interfaces.Data;
using ShopRUs.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ShopRUs.Tests.UnitTests.Core
{
    public class InvoiceDomainServiceUnitTest
    {
        private decimal _totalAmount;
        private decimal _totalAmountWithoutGroceries;
        private DateTime _customerRegistrationDate;
        private Mock<IDiscountRepository> _discountRepoMock;

        public InvoiceDomainServiceUnitTest()
        {
            _discountRepoMock = new Mock<IDiscountRepository>();
            _discountRepoMock.Setup(repo => repo.GetByType("Affiliate")).ReturnsAsync(GetTestDiscounts().FirstOrDefault(d => d.DiscountAppliesTo == "Affiliate"));
            _discountRepoMock.Setup(repo => repo.GetByType("Employee")).ReturnsAsync(GetTestDiscounts().FirstOrDefault(d => d.DiscountAppliesTo == "Employee"));
            _discountRepoMock.Setup(repo => repo.GetByType("Loyalty")).ReturnsAsync(GetTestDiscounts().FirstOrDefault(d => d.DiscountAppliesTo == "Loyalty"));
            _discountRepoMock.Setup(repo => repo.GetByType("General")).ReturnsAsync(GetTestDiscounts().FirstOrDefault(d => d.DiscountAppliesTo == "General"));
            _totalAmount = 10000;
            _totalAmountWithoutGroceries = 8000;
            _customerRegistrationDate = new DateTime(2019, 01, 01);
        }

        [Fact]
        public async Task CalculateDiscount_ReturnsTenPercentPlusFiveOnEveryHundredOfAmount_WhenCustomerIsAffliate()
        {
            // Arrange            
            var invoiceService = new InvoiceDomainService(_discountRepoMock.Object);
            var customerRole = "Affiliate";
            //Act
            var result = await invoiceService.CalculateDiscount(_totalAmount, _totalAmountWithoutGroceries, customerRole, _customerRegistrationDate);
            //Assert
            Assert.Equal(1300m, result);
        }

        [Fact]
        public async Task CalculateDiscount_ReturnsThirtyPercentPlusFiveOnEveryHundredOfAmount_WhenCustomerIsEmployee()
        {
            // Arrange            
            var invoiceService = new InvoiceDomainService(_discountRepoMock.Object);
            var customerRole = "Employee";
            //Act
            var result = await invoiceService.CalculateDiscount(_totalAmount, _totalAmountWithoutGroceries, customerRole, _customerRegistrationDate);
            //Assert
            Assert.Equal(2900m, result);
        }

        [Fact]
        public async Task CalculateDiscount_ReturnsFivePercentPlusFiveOnEveryHundredOfAmount_WhenCustomerForOverTwoYears()
        {
            // Arrange            
            var invoiceService = new InvoiceDomainService(_discountRepoMock.Object);
            var customerRole = "General";
            //Act
            var result = await invoiceService.CalculateDiscount(_totalAmount, _totalAmountWithoutGroceries, customerRole, _customerRegistrationDate);
            //Assert
            Assert.Equal(900m, result);
        }

        [Fact]
        public async Task CalculateDiscount_ReturnsFiveOnEveryHundredOfAmount_WhenCustomerRoleIsCustomerForLessThanTwoYears()
        {
            // Arrange            
            var invoiceService = new InvoiceDomainService(_discountRepoMock.Object);
            var customerRole = "Customer";
            var customerRegDate = DateTime.Now;
            //Act
            var result = await invoiceService.CalculateDiscount(_totalAmount, _totalAmountWithoutGroceries, customerRole, customerRegDate);
            //Assert
            Assert.Equal(500m, result);
        }

        [Fact]
        public async Task CalculateDiscount_ReturnsZero_WhenCustomerRoleIsCustomerForLessThanTwoYearsAndAmountIsLessThan100()
        {
            // Arrange            
            var invoiceService = new InvoiceDomainService(_discountRepoMock.Object);
            var customerRole = "Customer";
            var customerRegDate = DateTime.Now;
            var amt = 90;
            //Act
            var result = await invoiceService.CalculateDiscount(amt, amt, customerRole, customerRegDate);
            //Assert
            Assert.Equal(0m, result);
        }

        private List<Discount> GetTestDiscounts()
        {
            return new List<Discount>()
            {
                new Discount
                {
                    Id = 1,
                    DiscountAppliesTo = "Affiliate",
                    DiscountValueType = "Percentage",
                    DiscountValue = 10
                },
                new Discount
                {
                    Id = 2,
                    DiscountAppliesTo = "Employee",
                    DiscountValueType = "Percentage",
                    DiscountValue = 30
                },
                new Discount
                {
                    Id = 3,
                    DiscountAppliesTo = "Loyalty",
                    DiscountValueType = "Percentage",
                    DiscountValue = 5
                },
                new Discount
                {
                    Id = 4,
                    DiscountAppliesTo = "General",
                    DiscountValueType = "Value",
                    DiscountValue = 5,
                    Units = 100
                }
            };
        }
    }
}
