using Ardalis.Specification.EntityFrameworkCore;
using Kustomer.Domain.Interfaces;

namespace Kustomer.Infrastructure.Data;

public class EfRepository<T>(KustomerDbContext context) : RepositoryBase<T>(context), IReadRepository<T>, IRepository<T>
    where T : class, IAggregateRoot;