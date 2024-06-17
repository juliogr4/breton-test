using Breton.Application.Features.Account.DTO.Request;
using FluentValidation;

namespace Breton.Application.Features.Account.Commands.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.RegisterRequest.Name)
            .NotNull()
            .NotEmpty()
            .WithMessage("Name field is required");
        
        RuleFor(x => x.RegisterRequest.Password)
            .NotNull()
            .NotEmpty()
            .Matches(x => x.RegisterRequest.ConfirmPassword)
            .WithMessage("Passwords are not equal");

        RuleFor(x => x.RegisterRequest.Email)
            .NotNull()
            .NotEmpty()
            .EmailAddress()
            .WithMessage("Invalid Email Address");
    }
}