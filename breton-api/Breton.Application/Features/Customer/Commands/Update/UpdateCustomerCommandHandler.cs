using Breton.Domain.Entities.Customer.AggregateRoot;
using Breton.Domain.Repository;
using FluentValidation;
using Mapster;
using MediatR;

namespace Breton.Application.Features.Customer.Commands.Update;

public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IValidator<UpdateCustomerCommand> _validator;
    public UpdateCustomerCommandHandler(ICustomerRepository customerRepository, IValidator<UpdateCustomerCommand> validator)
    {
        _customerRepository = customerRepository;
        _validator = validator;
    }
    public async Task Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);
        
        var customer = await _customerRepository.GetCustomerByCpf(request.UpdateCustomerRequest.CPF);

        if(customer is null) throw new Exception("Customer do not exist");

        var customerMapping = request.UpdateCustomerRequest.Adapt<CustomerEntity>();

        await _customerRepository.UpdateCustomer(request.Id, customerMapping);
    }
}