using Ardalis.Specification;
using Kustomer.Domain.Entities;

namespace Kustomer.Application.Specifications;

public class CustomerByEmailSpec : Specification<Customer>
{
    public CustomerByEmailSpec(string email)
    {
        Query.Where(c => c.Email == email);
    }
}