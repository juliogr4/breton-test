using Breton.Application.Features.Customer.Queries.GetByPostalCode;
using FluentValidation;

namespace Breton.Application.Features.Customer.Commands.Update;

public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
{
    public UpdateCustomerCommandValidator()
    {

        RuleFor(x => x.UpdateCustomerRequest.id).NotNull().WithMessage("Id is required.");
        RuleFor(x => x.UpdateCustomerRequest.Name).NotEmpty().WithMessage("Name is required.");
        RuleFor(x => x.UpdateCustomerRequest.CPF).NotEmpty().WithMessage("CPF is required.");
        RuleFor(x => x.UpdateCustomerRequest.BirthDate).NotNull().WithMessage("Birthdate is required.");
        RuleFor(x => x.UpdateCustomerRequest.Phone).NotEmpty().WithMessage("Phone is required.");
        RuleFor(x => x.UpdateCustomerRequest.Address).NotNull().WithMessage("Address is required.");
        RuleFor(x => x.UpdateCustomerRequest.Address).SetValidator(new AddressRequestValidator());
    }
}