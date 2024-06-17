using Breton.Domain.Entities.Account;

namespace Breton.Application.Services.JwtGenerator;

public interface IJwtGenerator
{
    string GenerateToken(User user);
};