using FluentValidation;

namespace Breton.Application.Features.Customer.Queries.GetAddressByPostalCode;

public class GetAddressByPostalCodeCommandValidator : AbstractValidator<GetAddressByPostalCodeCommand>
{
    public GetAddressByPostalCodeCommandValidator()
    {
        RuleFor(x => x.PostalCode)
            .NotEmpty()
            .NotNull()
            .WithMessage("Postal code is required");
    }
}