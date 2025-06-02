using Kustomer.Application.Specifications;
using Kustomer.Domain.Entities;
using Kustomer.Domain.Interfaces;
using MediatR;

namespace Kustomer.Application.Customers.Commands;

public class UpdateCustomer
{
    public class Command : IRequest<Customer>
    {
        public required Guid Id { get; set; }
        public required Customer Customer { get; set; }
    }

    public class Handler(IRepository<Customer> repository) : IRequestHandler<Command, Customer>
    {
        public async Task<Customer> Handle(Command request, CancellationToken cancellationToken)
        {
            var customer = await repository.GetByIdAsync(request.Id, cancellationToken); ;
                
            if (customer == null)
            {
                throw new KeyNotFoundException($"Customer with ID {request.Id} not found.");
            }

            // Check if the email already exists
            var spec = new CustomerByEmailSpec(request.Customer.Email);
            var existingEmail = await repository
                .FirstOrDefaultAsync(spec, cancellationToken);
            if (existingEmail != null)
            {
                throw new Exception("Email already exists");
            }

            customer.FirstName = request.Customer.FirstName;
            customer.MiddleName = request.Customer.MiddleName;
            customer.LastName = request.Customer.LastName;
            customer.Email = request.Customer.Email;
            customer.Phone = request.Customer.Phone;

            await repository.UpdateAsync(customer, cancellationToken);
                
            return customer;
        }
    }
}