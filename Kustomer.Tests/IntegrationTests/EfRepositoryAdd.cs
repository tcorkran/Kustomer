using Kustomer.Domain.Entities;
using Kustomer.Tests.Data;

namespace Kustomer.Tests.IntegrationTests;

public class EfRepositoryAdd : BaseEfRepoTestFixture
{
    [Fact]
    public async Task Add_Customer_Success()
    {
        // Arrange
        var testCustomer = new Customer {
            FirstName = "Integration",
            LastName = "Test",
            Email = "itest@dontemailme.com",
            Phone = "123-456-7890"
        };

        var repository = GetRepository();

        // Act
        await repository.AddAsync(testCustomer);

        var newCustomer = (await repository.ListAsync()).FirstOrDefault();

        // Assert
        Assert.NotNull(newCustomer);
        Assert.True(newCustomer.Id != Guid.Empty);
        Assert.Equal(testCustomer.FirstName, newCustomer.FirstName);
        Assert.Equal(testCustomer.LastName, newCustomer.LastName);
        Assert.Equal(testCustomer.Email, newCustomer.Email);
        Assert.Equal(testCustomer.Phone, newCustomer.Phone);
    }
}
