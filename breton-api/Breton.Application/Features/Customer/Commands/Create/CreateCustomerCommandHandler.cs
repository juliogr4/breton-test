using Breton.Domain.Entities.Customer.AggregateRoot;
using Breton.Domain.Repository;
using FluentValidation;
using Mapster;
using MediatR;

namespace Breton.Application.Features.Customer.Commands.Create;

public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IValidator<CreateCustomerCommand> _validator;
    public CreateCustomerCommandHandler(ICustomerRepository customerRepository, IValidator<CreateCustomerCommand> validator)
    {
        _customerRepository = customerRepository;
        _validator = validator;
    }

    public async Task Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);
        
        var customer = await _customerRepository.GetCustomerByCpf(request.CreateCustomerRequest.CPF);

        if(customer is not null) throw new Exception("Customer already exists");

        var customerMapping = request.CreateCustomerRequest.Adapt<CustomerEntity>();
        
        await _customerRepository.CreateCustomer(customerMapping);
    }
}