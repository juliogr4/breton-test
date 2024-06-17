namespace Breton.Application.Models;

public class JwtOptions
{
    public string Issuer { get; set; } = String.Empty;
    public string Audience { get; set; } = String.Empty;
    public string SigningKey { get; set; } = String.Empty;
    public int ExpirationSeconds { get; set; }
}
