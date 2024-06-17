using Breton.Application.Features.Customer.Commands.Create;
using Breton.Application.Features.Customer.Commands.Delete;
using Breton.Application.Features.Customer.Commands.Update;
using Breton.Application.Features.Customer.DTO.Request;
using Breton.Application.Features.Customer.Queries.GetAddressByPostalCode;
using Breton.Application.Features.Customer.Queries.GetCustomerById;
using Breton.Application.Features.Customer.Queries.GetCustomers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Breton.Api.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class CustomerController : ControllerBase
{
    private readonly IMediator _mediator;
    public CustomerController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCustomer(CreateCustomerRequest createCustomerRequest)
    {
        await _mediator.Send(new CreateCustomerCommand(createCustomerRequest));
        return Ok("customer created successfully");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCustomer(int id, UpdateCustomerRequest updateCustomerRequest)
    {
        await _mediator.Send(new UpdateCustomerCommand(id, updateCustomerRequest));
        return Ok("customer updated successfully");
    }

    [HttpGet("postal-code/{postalCode}")]
    public async Task<IActionResult> GetAddressByPostalCode(string postalCode)
    {
        var address = await _mediator.Send(new GetAddressByPostalCodeCommand(postalCode));
        return Ok(address);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCustomer(int id)
    {
        await _mediator.Send(new DeleteCustomerCommand(id));
        return Ok("customer deleted successfully");
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCustomerById(int id)
    {
        var customer = await _mediator.Send(new GetCustomerByIdCommand(id));
        return Ok(customer);
    }

    [HttpGet]
    public async Task<IActionResult> GetCustomers([FromQuery] GetCustomersRequest getCustomersRequest)
    {
        var customers = await _mediator.Send(new GetCustomersCommand(getCustomersRequest));
        return Ok(customers);
    }
}