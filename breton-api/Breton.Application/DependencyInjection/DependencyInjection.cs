using System.Text;
using Breton.Application.Models;
using Breton.Application.Services.Email;
using Breton.Application.Services.JwtGenerator;
using Breton.Application.Services.PasswordHashing;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Breton.Application.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.RegisterMaps();

        services.Configure<JwtOptions>(configuration.GetSection("JwtOptions"));
        services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));

        services.AddScoped<IPasswordHashing, PasswordHashing>();
        services.AddScoped<IJwtGenerator, JwtGenerator>();
        services.AddScoped<IEmailService, EmailService>();

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

        // jwt
        var jwtOptions = configuration.GetSection("JwtOptions").Get<JwtOptions>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(opts =>
        {
            //convert the string signing key to byte array
            byte[] signingKeyBytes = Encoding.UTF8.GetBytes(jwtOptions.SigningKey);

            opts.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtOptions.Issuer,
                ValidAudience = jwtOptions.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(signingKeyBytes)
            };
        });

        return services;
    }
}