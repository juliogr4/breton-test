using Breton.Application.Features.Customer.DTO.Request;
using FluentValidation;

namespace Breton.Application.Features.Customer.Queries.GetByPostalCode;

public class AddressRequestValidator : AbstractValidator<AddressRequest>
{
    public AddressRequestValidator()
    {
        RuleFor(x => x.PostalCode)
            .NotEmpty().WithMessage("Postal Code is required.")
            .NotNull().WithMessage("Postal Code cannot be null.");

        RuleFor(x => x.Street)
            .NotEmpty().WithMessage("Street is required.")
            .NotNull().WithMessage("Street cannot be null.");

        RuleFor(x => x.Neighborhood)
            .NotEmpty().WithMessage("Neighborhood is required.")
            .NotNull().WithMessage("Neighborhood cannot be null.");

        RuleFor(x => x.City)
            .NotEmpty().WithMessage("City is required.")
            .NotNull().WithMessage("City cannot be null.");

        RuleFor(x => x.State)
            .NotEmpty().WithMessage("State is required.")
            .NotNull().WithMessage("State cannot be null.");
    }
}