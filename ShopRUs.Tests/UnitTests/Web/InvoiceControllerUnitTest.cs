using Microsoft.AspNetCore.Mvc;
using Moq;
using ShopRUs.Web.Controllers;
using ShopRUs.Web.Models;
using ShopRUs.Web.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ShopRUs.Tests.UnitTests.Web
{
    public class InvoiceControllerUnitTest
    {
        [Fact]
        public async Task InvoicePost_ReturnsBadRequestResult_WhenModelIsInvalid()
        {
            // Arrange
            var mockService = new Mock<IInvoiceService>();
            var controller = new InvoiceController(mockService.Object);
            controller.ModelState.AddModelError("CustomerId", "invalid");
            var newBill = GetBill();

            // Act
            var result = await controller.Post(newBill);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<SerializableError>(badRequestResult.Value);
        }

        private InvoiceDTO GetBill()
        {
            var billItems = new List<InvoiceItemDTO>()
            {
                new InvoiceItemDTO
                {
                    ItemDescription = "",
                    ItemType = "",
                    Quantity = 0,
                    UnitPrice = 0
                },
                new InvoiceItemDTO
                {
                    ItemDescription = "",
                    ItemType = "",
                    Quantity = 0,
                    UnitPrice = 0
                },
                new InvoiceItemDTO
                {
                    ItemDescription = "",
                    ItemType = "",
                    Quantity = 0,
                    UnitPrice = 0
                },
            };
            var bill = new InvoiceDTO()
            {
                CustomerId = 0,
                Items = billItems
            };
            return bill;
        }
    }
}
