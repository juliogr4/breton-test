using Breton.Application.Features.Customer.Queries.GetByPostalCode;
using FluentValidation;

namespace Breton.Application.Features.Customer.Commands.Create;

public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidator()
    {

        RuleFor(x => x.CreateCustomerRequest.CreatedBy).NotNull().WithMessage("CreatedBy is required.");
        RuleFor(x => x.CreateCustomerRequest.Name).NotEmpty().WithMessage("Name is required.");
        RuleFor(x => x.CreateCustomerRequest.CPF).NotEmpty().WithMessage("CPF is required.");
        RuleFor(x => x.CreateCustomerRequest.BirthDate).NotNull().WithMessage("Birthdate is required.");
        RuleFor(x => x.CreateCustomerRequest.Phone).NotEmpty().WithMessage("Phone is required.");
        RuleFor(x => x.CreateCustomerRequest.Address).NotNull().WithMessage("Address is required.");
        RuleFor(x => x.CreateCustomerRequest.Address).SetValidator(new AddressRequestValidator());
    }
}