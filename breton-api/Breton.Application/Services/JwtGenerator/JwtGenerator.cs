using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Breton.Application.Models;
using Breton.Domain.Entities.Account;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Breton.Application.Services.JwtGenerator;

public class JwtGenerator : IJwtGenerator
{
    private readonly JwtOptions _jwtOptions;
    public JwtGenerator(IOptions<JwtOptions> jwtOptions) 
    {
        _jwtOptions = jwtOptions.Value;
    }
    public string GenerateToken(User user)
    {
        var keyBytes = Encoding.UTF8.GetBytes(_jwtOptions.SigningKey);
        var symmetricKey = new SymmetricSecurityKey(keyBytes);

        var signingCredentials = new SigningCredentials(
            symmetricKey,
            SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>()
        {
            new Claim("userID", user.Id.ToString()),
            new Claim("name", user.Name),
            new Claim("aud", _jwtOptions.Audience)
        };

        var token = new JwtSecurityToken(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: signingCredentials);

        var rawToken = new JwtSecurityTokenHandler().WriteToken(token);
        return rawToken;
    }
}