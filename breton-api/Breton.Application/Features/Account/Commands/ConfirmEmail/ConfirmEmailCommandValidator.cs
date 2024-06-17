using Breton.Application.Features.Account.DTO.Request;
using FluentValidation;

namespace Breton.Application.Features.Account.Commands.ConfirmEmail;

public class ConfirmEmailCommandValidator : AbstractValidator<ConfirmEmailCommand>
{
    public ConfirmEmailCommandValidator()
    {
        RuleFor(x => x.emailToken)
            .NotNull()
            .NotEmpty()
            .WithMessage("Email token is required");
    }
}