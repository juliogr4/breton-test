using Breton.Domain.Repository;
using FluentValidation;
using MediatR;

namespace Breton.Application.Features.Customer.Commands.Delete;

public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IValidator<DeleteCustomerCommand> _validator;
    public DeleteCustomerCommandHandler(ICustomerRepository customerRepository, IValidator<DeleteCustomerCommand> validator)
    {
        _customerRepository = customerRepository;
        _validator = validator;
    }
    public async Task Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);
        
        var customer = await _customerRepository.GetCustomerById(request.Id);

        if(customer is null) throw new Exception("This customer do not exist on our database");

        await _customerRepository.DeleteCustomer(request.Id);
    }
}