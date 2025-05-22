using Kustomer.Domain.Entities;
using Kustomer.Domain.Interfaces;
using MediatR;

namespace Kustomer.Application.Customers.Commands;

public class DeleteCustomer
{
    public class Command : IRequest
    {
        public required Guid Id { get; set; }
    }

    public class Handler(IRepository<Customer> repository) : IRequestHandler<Command>
    {
        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            var customer = await repository.GetByIdAsync(request.Id, cancellationToken);
                
            if (customer == null)
            {
                throw new KeyNotFoundException($"Customer with ID {request.Id} not found.");
            }

            await repository.DeleteAsync(customer, cancellationToken);
        }
    }
}