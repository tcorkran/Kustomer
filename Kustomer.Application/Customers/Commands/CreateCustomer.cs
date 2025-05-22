using Kustomer.Application.Specifications;
using Kustomer.Domain.Entities;
using Kustomer.Domain.Interfaces;
using MediatR;

namespace Kustomer.Application.Customers.Commands;

public class CreateCustomer
{
    public class Command : IRequest<Customer>
    {
        public required Customer Customer { get; set; }
    }

    public class Handler(IRepository<Customer> repository) : IRequestHandler<Command, Customer>
    {
        public async Task<Customer> Handle(Command request, CancellationToken cancellationToken)
        {
            // Check if the email already exists
            var spec = new CustomerByEmailSpec(request.Customer.Email);
            var existingEmail = await repository
                .FirstOrDefaultAsync(spec, cancellationToken);
            if (existingEmail != null)
            {
                throw new Exception("Email already exists");
            }

            var createdCustomer = await repository.AddAsync(request.Customer, cancellationToken);

            return createdCustomer;
        }
    }
}