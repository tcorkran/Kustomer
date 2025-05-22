using Ardalis.Specification;

namespace Kustomer.Domain.Interfaces;

public interface IRepository<T> : IRepositoryBase<T> where T : class, IAggregateRoot
{
}