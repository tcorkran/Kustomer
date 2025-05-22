using Kustomer.Domain.Entities;
using Kustomer.Tests.Data;

namespace Kustomer.Tests.IntegrationTests;

public class EfRepositoryDelete : BaseEfRepoTestFixture
{
    [Fact]
    public async Task Delete_Customer_Success()
    {
        // Arrange
        var testCustomer = new Customer
        {
            FirstName = "Delete",
            LastName = "Me",
            Email = "deleteme@dontemailme.com",
            Phone = "555-444-3333"
        };

        var repository = GetRepository();

        // Act
        // Add the Customer
        await repository.AddAsync(testCustomer);
        // Delete the Customer
        await repository.DeleteAsync(testCustomer);
        var testList = await repository.ListAsync();

        // Assert
        // Verify Customer is deleted
        Assert.DoesNotContain(testList, x => x.Id == testCustomer.Id);
    }
}