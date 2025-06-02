using Kustomer.Application.Customers.Commands;
using Kustomer.Application.Specifications;
using Kustomer.Domain.Entities;
using Kustomer.Domain.Interfaces;
using NSubstitute;

namespace Kustomer.Tests.UnitTests;

public class CreateCustomerCommandHandlerTests
{
    private readonly IRepository<Customer> _repository = Substitute.For<IRepository<Customer>>();
    private readonly CreateCustomer.Handler _handler;

    public CreateCustomerCommandHandlerTests()
    {
        _handler = new CreateCustomer.Handler(_repository);
    }

    [Fact]
    public async Task Returns_Created_Customer()
    {
        // Arrange
        var testCustomer = new Customer
        {
            FirstName = "Integration",
            LastName = "Test",
            Email = "itest@dontemailme.com",
            Phone = "123-456-7890"
        };

        // Act
        _repository.AddAsync(Arg.Any<Customer>(), Arg.Any<CancellationToken>())
            .Returns(testCustomer);
        var result = await _handler.Handle(new CreateCustomer.Command { Customer = testCustomer }, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(result, testCustomer);
        Assert.Equal(testCustomer.FirstName, result.FirstName);
        Assert.Equal(testCustomer.LastName, result.LastName);
        Assert.Equal(testCustomer.Email, result.Email);
        Assert.Equal(testCustomer.Phone, result.Phone);
    }

    [Fact]
    public async Task Returns_Error_WhenEmailExists()
    {
        // Arrange
        var existingCustomer = new Customer
        {
            FirstName = "Existing",
            LastName = "Guy",
            Email = "itest@dontemailme.com",
            Phone = "123-456-7890"
        };

        var testCustomer = new Customer
        {
            FirstName = "Integration",
            LastName = "Test",
            Email = "itest@dontemailme.com",
            Phone = "123-456-7890"
        };

        // Act
        _repository.FirstOrDefaultAsync(Arg.Any<CustomerByEmailSpec>(), Arg.Any<CancellationToken>())
            .Returns(existingCustomer);
        _repository.AddAsync(Arg.Any<Customer>(), Arg.Any<CancellationToken>())
            .Returns(testCustomer);

        // Assert
        await Assert.ThrowsAsync<Exception>(async() => await _handler.Handle(new CreateCustomer.Command { Customer = testCustomer }, CancellationToken.None));
    }
}