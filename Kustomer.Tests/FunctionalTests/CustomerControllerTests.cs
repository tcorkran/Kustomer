using Kustomer.API.Controllers;
using Kustomer.Application.Customers.Queries;
using Kustomer.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Moq;

namespace Kustomer.Tests.FunctionalTests;

public class CustomerControllerTests
{
    #region GetCustomers_Returns_Ok()
    [Fact]
    public async Task GetCustomers_Returns_Ok()
    {
        // Arrange
        var contextMock = new Mock<HttpContext>();
        var mediatorMock = new Mock<IMediator>();

        var customers = new List<Customer>
        {
            new()
            {
                FirstName = "First",
                LastName = "Tester",
                Email = "first_test@dontemailme.com",
                Phone = "123-456-7890"
            },
            new()
            {
                FirstName = "Second",
                LastName = "McTester",
                Email = "second_test@me.com",
                Phone = "555-123-4567"
            },
        };

        var controller = new CustomerController(mediatorMock.Object)
        {
            ControllerContext = { HttpContext = contextMock.Object },
        };

        var query = new GetCustomerList.Query();

        mediatorMock.Setup(m => m.Send(It.IsAny<GetCustomerList.Query>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(customers);

        // Act
        var result = await controller.GetCustomers();

        // Assert
        Assert.NotNull(result);
        Assert.IsType<List<Customer>>(result.Value);
        Assert.Equal(customers.Count, ((List<Customer>)result.Value).Count);
    }
    #endregion

    #region GetCustomer_Returns_Ok()
    [Fact]
    public async Task GetCustomer_Returns_Ok()
    {
        // Arrange
        var contextMock = new Mock<HttpContext>();
        var mediatorMock = new Mock<IMediator>();

        var testCustomer = new Customer
        {
                FirstName = "First",
                LastName = "Tester",
                Email = "first_test@dontemailme.com",
                Phone = "123-456-7890"
        };

        var controller = new CustomerController(mediatorMock.Object)
        {
            ControllerContext = { HttpContext = contextMock.Object },
        };

        var query = new GetCustomerDetails.Query { Id = testCustomer.Id };

        mediatorMock.Setup(m => m.Send(It.IsAny<GetCustomerDetails.Query>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(testCustomer);

        // Act
        var result = await controller.GetCustomer(testCustomer.Id);
        var returnedCustomer = result.Value;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<Customer>(returnedCustomer);
        Assert.Equal(returnedCustomer, testCustomer);
    }
    #endregion
}