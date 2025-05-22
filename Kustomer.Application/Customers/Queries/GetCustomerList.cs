using Kustomer.Domain.Entities;
using Kustomer.Domain.Interfaces;
using MediatR;

namespace Kustomer.Application.Customers.Queries;

public class GetCustomerList
{
    public class Query : IRequest<List<Customer>>
    {
    }

    public class Handler(IRepository<Customer> repository) : IRequestHandler<Query, List<Customer>>
    {
        public async Task<List<Customer>> Handle(Query request, CancellationToken cancellationToken)
        {
            return await repository.ListAsync(cancellationToken);
        }
    }
}