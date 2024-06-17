using Breton.Domain.Entities.Account;

namespace Breton.Domain.Repository;

public interface IUserRepository
{
    Task Register(User user);
    Task ConfirmEmail(string emailToken);
    Task<User?> GetUserByEmail(string email);
    Task<User?> GetUserByEmailToken(string emailToken);
}