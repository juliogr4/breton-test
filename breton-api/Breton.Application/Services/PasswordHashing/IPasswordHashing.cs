namespace Breton.Application.Services.PasswordHashing;

public interface IPasswordHashing
{
    string GenerateHash(string password);
    bool VerifyPassword(string password, string hashPassword);
}