using Kustomer.Domain.Entities;
using Kustomer.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Kustomer.Tests.Data;

#region BaseEfRepoTestFixture
public abstract class BaseEfRepoTestFixture
{
    protected KustomerDbContext _context;

    protected BaseEfRepoTestFixture()
    {
        var options = CreateNewContextOptions();

        _context = new KustomerDbContext(options);
    }

    protected static DbContextOptions<KustomerDbContext> CreateNewContextOptions()
    {
        // Create a fresh service provider, and therefore a fresh
        // InMemory database instance.
        var serviceProvider = new ServiceCollection()
            .AddEntityFrameworkInMemoryDatabase()
            .BuildServiceProvider();

        // Create a new options instance telling the context to use an
        // InMemory database and the new service provider.
        var builder = new DbContextOptionsBuilder<KustomerDbContext>();
        builder.UseInMemoryDatabase("kustomertest")
            .UseInternalServiceProvider(serviceProvider);

        return builder.Options;
    }

    #region GetRepository
    protected EfRepository<Customer> GetRepository()
    {
        return new EfRepository<Customer>(_context);
    }
    #endregion
}
#endregion