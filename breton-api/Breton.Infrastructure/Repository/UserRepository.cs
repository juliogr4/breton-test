using Breton.Domain.Entities.Account;
using Breton.Domain.Repository;
using Breton.Infrastructure.Data;
using Dapper;

namespace Breton.Infrastructure.Repository;

public class UserRepository : IUserRepository
{
    private readonly DapperContext _dapperContext;
    public UserRepository(DapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }
    public async Task ConfirmEmail(string emailToken)
    {
        using(var _conn = _dapperContext.CreateConnection())
        {
            var procedure = Procedures.pr_user_email_confirmation;

            var parameters = new DynamicParameters();
            parameters.Add("email_token", emailToken);

            await _conn.ExecuteAsync(procedure, parameters);
        }
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        using(var _conn = _dapperContext.CreateConnection())
        {
            var procedure = Procedures.pr_user_get_by_email;

            var parameters = new DynamicParameters();
            parameters.Add("email", email);

            return await _conn.QuerySingleOrDefaultAsync<User?>(procedure, parameters);
        }
    }

    public async Task<User?> GetUserByEmailToken(string emailToken)
    {
        using(var _conn = _dapperContext.CreateConnection())
        {
            var procedure = Procedures.pr_user_get_by_email_token;

            var parameters = new DynamicParameters();
            parameters.Add("email_token", emailToken);

            return await _conn.QuerySingleOrDefaultAsync<User?>(procedure, parameters);
        }
    }

    public async Task Register(User user)
    {
        using(var _conn = _dapperContext.CreateConnection())
        {
            var procedure = Procedures.pr_user_create;

            var parameters = new DynamicParameters();
            parameters.Add("name", user.Name);
            parameters.Add("email", user.Email);
            parameters.Add("password_hash", user.PasswordHash);
            parameters.Add("email_token", user.EmailToken);
            parameters.Add("email_token_expires_at", user.EmailTokenExpiresAt);

            await _conn.ExecuteAsync(procedure, parameters);
        }
    }
}