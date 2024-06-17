using FluentValidation;

namespace Breton.Application.Features.Customer.Queries.GetCustomerById;

public class GetCustomerByIdCommandValidator : AbstractValidator<GetCustomerByIdCommand>
{
    public GetCustomerByIdCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .NotNull()
            .WithMessage("Customer id may not be null");
    }
}