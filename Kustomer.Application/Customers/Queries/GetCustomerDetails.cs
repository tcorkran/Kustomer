using Kustomer.Application.Specifications;
using Kustomer.Domain.Entities;
using Kustomer.Domain.Interfaces;
using MediatR;

namespace Kustomer.Application.Customers.Queries;

public class GetCustomerDetails
{
    public class Query : IRequest<Customer>
    {
        public required Guid Id { get; set; }
    }

    public class Handler(IRepository<Customer> repository) : IRequestHandler<Query, Customer>
    {
        public async Task<Customer> Handle(Query request, CancellationToken cancellationToken)
        {
            var spec = new CustomerByIdSpec(request.Id);
            var customer = await repository
                .FirstOrDefaultAsync(spec, cancellationToken);
                
            if (customer == null)
            {
                throw new Exception("Customer not found");
            }
                
            return customer;
        }
    }
}