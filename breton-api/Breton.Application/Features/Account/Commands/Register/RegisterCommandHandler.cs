using Breton.Application.Services.Email;
using Breton.Application.Services.PasswordHashing;
using Breton.Domain.Entities.Account;
using Breton.Domain.Repository;
using FluentValidation;
using MediatR;

namespace Breton.Application.Features.Account.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHashing _passwordHashing;
    private readonly IValidator<RegisterCommand> _validator;
    private readonly IEmailService _emailService;
    public RegisterCommandHandler(
        IUserRepository userRepository, 
        IPasswordHashing passwordHashing,
        IValidator<RegisterCommand> validator,
        IEmailService emailService)
    {
        _userRepository = userRepository;
        _passwordHashing = passwordHashing;
        _validator = validator;
        _emailService = emailService;
    }
    public async Task Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var user = await _userRepository.GetUserByEmail(request.RegisterRequest.Email);

        if(user is not null) { throw new Exception("There is already an user registered if this email"); }

        var newUser = new User(
            request.RegisterRequest.Name,
            request.RegisterRequest.Email,
            _passwordHashing.GenerateHash(request.RegisterRequest.Password)
        );

        await _userRepository.Register(newUser);
        
        _emailService.SendEmail(newUser.Email, "Email Confirmation", $"http://localhost:4200/account/confirm-email/{newUser.EmailToken}");
    }
}