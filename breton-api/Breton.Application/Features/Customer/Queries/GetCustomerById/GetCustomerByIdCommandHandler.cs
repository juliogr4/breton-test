using Breton.Application.Features.Customer.DTO.Response;
using Breton.Domain.Repository;
using FluentValidation;
using Mapster;
using MediatR;

namespace Breton.Application.Features.Customer.Queries.GetCustomerById;

public class GetCustomerByIdCommandHandler : IRequestHandler<GetCustomerByIdCommand, CustomerResponse>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IValidator<GetCustomerByIdCommand> _validator;
    public GetCustomerByIdCommandHandler(ICustomerRepository customerRepository, IValidator<GetCustomerByIdCommand> validator)
    {
        _customerRepository = customerRepository;
        _validator = validator;
    }
    public async Task<CustomerResponse> Handle(GetCustomerByIdCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var customer = await _customerRepository.GetCustomerById(request.Id);

        if(customer is null) throw new Exception("Customer wasn't found");

        var customerMapping = customer.Adapt<CustomerResponse>();

        return customerMapping;
    }
}