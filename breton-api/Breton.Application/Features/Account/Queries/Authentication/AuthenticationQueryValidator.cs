using Breton.Application.Features.Account.DTO.Request;
using FluentValidation;

namespace Breton.Application.Features.Account.Queries.Authentication;
public class AuthenticationQueryValidator : AbstractValidator<AuthenticationQuery>
{
    public AuthenticationQueryValidator()
    {
        RuleFor(x => x.AuthenticationRequest.Email)
        .NotEmpty()
        .NotNull()
        .EmailAddress()
        .WithMessage("Email invalid");

        RuleFor(x => x.AuthenticationRequest.Password)
        .NotEmpty()
        .NotNull();
    }
}