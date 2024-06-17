using Breton.Domain.Repository;
using Breton.Infrastructure.Data;
using Breton.Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Breton.Infrastructure.DependencyInjection;

public static class DependencyInjection 
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<DapperContext>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        return services;
    }
}