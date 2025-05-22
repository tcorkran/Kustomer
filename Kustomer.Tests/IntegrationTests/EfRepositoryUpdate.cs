using Kustomer.Domain.Entities;
using Kustomer.Tests.Data;

namespace Kustomer.Tests.IntegrationTests;

public class EfRepositoryUpdate : BaseEfRepoTestFixture
{
    [Fact]
    public async Task Update_Customer_Success()
    {
        // Arrange
        var testCustomer = new Customer
        {
            FirstName = "Update",
            LastName = "Me",
            Email = "updateme@dontemailme.com",
            Phone = "123-456-0987"
        };

        var repository = GetRepository();


        // Act
        await repository.AddAsync(testCustomer);

        // Fetch the Customer
        var newCustomer = (await repository.ListAsync())
            .FirstOrDefault(customer => customer.Id == testCustomer.Id);

        Assert.NotNull(newCustomer);

        // Update the Customer
        newCustomer.FirstName = "UpdatedFirstName";
        await repository.UpdateAsync(newCustomer);

        // Fetch the updated item
        var updatedCustomer = (await repository.ListAsync())
            .FirstOrDefault(customer => customer.Id == testCustomer.Id);

        // Assert
        Assert.NotNull(updatedCustomer);
        Assert.Equal(newCustomer.FirstName, updatedCustomer.FirstName);
        Assert.Equal(newCustomer.Id, updatedCustomer.Id);
    }
}