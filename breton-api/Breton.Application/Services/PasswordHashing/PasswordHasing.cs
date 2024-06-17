using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Breton.Application.Services.PasswordHashing;

public class PasswordHashing : IPasswordHashing
{
    private readonly char delimiter  = ';';
    private readonly int saltSize = 128 / 8;
    private readonly int hashSize = 256 / 8;
    private readonly int iterationCount = 100000;
    private readonly KeyDerivationPrf hashAlgorithm = KeyDerivationPrf.HMACSHA256;
    
    public string GenerateHash(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(saltSize);
        byte[] hash = KeyDerivation.Pbkdf2(password, salt, hashAlgorithm, iterationCount, hashSize);
        return string.Join(delimiter , Convert.ToBase64String(hash), Convert.ToBase64String(salt));
    }

    public bool VerifyPassword(string password, string hashPassword)
    {
        string[] elements = hashPassword.Split(';');
        byte[] hashDB = Convert.FromBase64String(elements[0]);
        byte[] saltDB = Convert.FromBase64String(elements[1]);

        byte[] hash = KeyDerivation.Pbkdf2(password, saltDB, hashAlgorithm, iterationCount, hashSize);
        return CryptographicOperations.FixedTimeEquals(hash, hashDB);
    }
}