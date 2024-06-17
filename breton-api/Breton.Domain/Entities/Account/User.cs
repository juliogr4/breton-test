namespace Breton.Domain.Entities.Account;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public string Email { get; set; } = String.Empty;
    public string PasswordHash { get; set; } = String.Empty;
    public string EmailToken { get; set; } = String.Empty;
    public DateTime EmailTokenExpiresAt { get; set; }
    public DateTime? EmailTokenVerifiedAt { get; set; }
    public DateTime CreatedAt { get; set; }

    public User() {}

    public User(
        string name, 
        string email, 
        string passwordHash 
    )
    {
        Name = name;
        Email = email;
        PasswordHash = passwordHash;
        EmailTokenExpiresAt = DateTime.Now.AddDays(3);
        EmailToken = Guid.NewGuid().ToString();
    }
}