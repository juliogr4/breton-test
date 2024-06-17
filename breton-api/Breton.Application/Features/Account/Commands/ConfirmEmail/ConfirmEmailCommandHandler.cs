using Breton.Application.Services.PasswordHashing;
using Breton.Domain.Entities.Account;
using Breton.Domain.Repository;
using FluentValidation;
using MediatR;

namespace Breton.Application.Features.Account.Commands.ConfirmEmail;

public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHashing _passwordHashing;
    private readonly IValidator<ConfirmEmailCommand> _validator;
    public ConfirmEmailCommandHandler(
        IUserRepository userRepository, 
        IPasswordHashing passwordHashing,
        IValidator<ConfirmEmailCommand> validator)
    {
        _userRepository = userRepository;
        _passwordHashing = passwordHashing;
        _validator = validator;
    }
    public async Task Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var user = await _userRepository.GetUserByEmailToken(request.emailToken);

        if(user is null) throw new Exception("This token do not exist or expired");

        if(user.EmailTokenVerifiedAt is DateTime) throw new Exception("This email was already verified");

        await _userRepository.ConfirmEmail(user.EmailToken);
    }
}