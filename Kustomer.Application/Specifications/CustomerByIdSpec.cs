using Ardalis.Specification;
using Kustomer.Domain.Entities;

namespace Kustomer.Application.Specifications;

public class CustomerByIdSpec : Specification<Customer>
{
    public CustomerByIdSpec(Guid id)
    {
        Query.Where(c => c.Id == id);
    }
}