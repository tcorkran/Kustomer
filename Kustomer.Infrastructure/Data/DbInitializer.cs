using Kustomer.Domain.Entities;

namespace Kustomer.Infrastructure.Data;

public class DbInitializer
{
    public static async Task SeedData(KustomerDbContext context)
    {
        var customers = new List<Customer>
        {
            new()
            {
                FirstName = "Thomas",
                MiddleName = "Ryan",
                LastName = "Corkran",
                Email = "tcorkran@dontemailme.com",
                Phone = "123-456-7890"
            },
            new()
            {
                FirstName = "Test",
                LastName = "McTester",
                Email = "test@me.com",
                Phone = "555-123-4567"
            },
        };

        if (!context.Customers.Any())
        {
            await context.Customers.AddRangeAsync(customers);
            await context.SaveChangesAsync();
        }
    }
}