using Microsoft.AspNetCore.Mvc;
using Moq;
using ShopRUs.Web.Controllers;
using ShopRUs.Web.Interfaces;
using ShopRUs.Web.Models;
using System.Threading.Tasks;
using Xunit;

namespace ShopRUs.Tests.UnitTests.Web
{
    public class CustomerControllerUnitTest
    {
        public async Task CustomerPost_ReturnsBadRequestResult_WhenModelIsInvalid()
        {
            // Arrange
            var mockService = new Mock<ICustomerService>();
            var controller = new CustomerController(mockService.Object);
            controller.ModelState.AddModelError("CustomerName", "Required");
            var newCustomer = GetCustomer();

            // Act
            var result = await controller.Post(newCustomer);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<SerializableError>(badRequestResult.Value);
        }

        private CustomerDTO GetCustomer()
        {
            var customer = new CustomerDTO()
            {
                Name = "",
                Address = "",
                Email = "",
                PhoneNumber = "",
                Role = ""
            };
            return customer;
        }
    }
}
