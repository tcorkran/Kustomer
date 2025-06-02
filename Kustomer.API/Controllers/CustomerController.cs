using Kustomer.Application.Customers.Commands;
using Kustomer.Application.Customers.Queries;
using Kustomer.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Kustomer.API.Controllers;

#region CustomerController
[ApiController]
[Route("[controller]")]
public class CustomerController(IMediator mediator) : ControllerBase
{
    #region GetCustomers
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
    {
          return await mediator.Send(new GetCustomerList.Query());
    }
    #endregion

    #region GetCustomer
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Customer>> GetCustomer(Guid id)
    {
        return await mediator.Send(new GetCustomerDetails.Query { Id = id });
    }
    #endregion

    #region CreateCustomer
    [HttpPost]
    public async Task<ActionResult<Customer>> CreateCustomer([FromBody] Customer customer)
    {
        return await mediator.Send(new CreateCustomer.Command { Customer = customer });
    }
    #endregion

    #region UpdateCustomer
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<Customer>> UpdateCustomer(Guid id, [FromBody] Customer customer)
    {
        return await mediator.Send(new UpdateCustomer.Command { Id = id, Customer = customer });
    }
    #endregion

    #region DeleteCustomer
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteCustomer(Guid id)
    {
        await mediator.Send(new DeleteCustomer.Command { Id = id });

        return Ok();
    }
    #endregion

#endregion
}