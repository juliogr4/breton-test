using Breton.Application.Features.Customer.Queries.GetCustomers;
using FluentValidation;

namespace Breton.Application.Features.Customer.Queries.GetCustomerById;

public class GetCustomersCommandValidator : AbstractValidator<GetCustomersCommand>
{
    public GetCustomersCommandValidator()
    {
        RuleFor(x => x.GetCustomersRequest.PageNumber)
            .NotEmpty()
            .NotNull()
            .WithMessage("Page number may not be null");

        RuleFor(x => x.GetCustomersRequest.PageSize)
            .NotEmpty()
            .NotNull()
            .WithMessage("Page size may not be null");
    }

}