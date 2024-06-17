using Breton.Application.Services.JwtGenerator;
using Breton.Application.Services.PasswordHashing;
using Breton.Domain.Repository;
using FluentValidation;
using MediatR;

namespace Breton.Application.Features.Account.Queries.Authentication;

public class AuthenticationQueryHandler : IRequestHandler<AuthenticationQuery, string>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHashing _passwordHashing;
    private readonly IJwtGenerator _jwtGenerator;
    private readonly IValidator<AuthenticationQuery> _validator;

    public AuthenticationQueryHandler(
        IUserRepository userRepository, 
        IPasswordHashing passwordHashing,
        IJwtGenerator jwtGenerator,
        IValidator<AuthenticationQuery> validator)
    {
        _userRepository = userRepository;
        _passwordHashing = passwordHashing;
        _jwtGenerator = jwtGenerator;
        _validator = validator;
    }

    public async Task<string> Handle(AuthenticationQuery request, CancellationToken cancellationToken)
    {

        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        // check if email exists
        var user = await _userRepository.GetUserByEmail(request.AuthenticationRequest.Email);

        if(user is null) 
            throw new Exception("Email or password is incorrect");

        // verify password
        var isPasswordEqual = _passwordHashing.VerifyPassword(
            request.AuthenticationRequest.Password, 
            user.PasswordHash);

        if(!isPasswordEqual) 
            throw new Exception("Email or password is incorrect");

        if(user.EmailTokenVerifiedAt is null) 
            throw new Exception("You must confirm your email to proceed");

        // generate and return jwt
        return _jwtGenerator.GenerateToken(user);
    }
}