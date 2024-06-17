using System.Reflection;
using Breton.Application.Features.Account.Commands;
using Breton.Application.Features.Account.Commands.ConfirmEmail;
using Breton.Application.Features.Account.Commands.Register;
using Breton.Application.Features.Account.Queries.Authentication;
using Breton.Application.Features.Customer.Commands.Create;
using Breton.Application.Features.Customer.Commands.Delete;
using Breton.Application.Features.Customer.Commands.Update;
using Breton.Application.Features.Customer.Queries.GetAddressByPostalCode;
using Breton.Application.Features.Customer.Queries.GetCustomerById;
using Breton.Application.Features.Customer.Queries.GetCustomers;
using FluentValidation;
using Mapster;
using Microsoft.Extensions.DependencyInjection;

public static class MappingConfiguration
{
    public static void RegisterMaps(this IServiceCollection services)
    {
        services.AddScoped<IValidator<RegisterCommand>, RegisterCommandValidator>();
        services.AddScoped<IValidator<CreateCustomerCommand>, CreateCustomerCommandValidator>();
        services.AddScoped<IValidator<AuthenticationQuery>, AuthenticationQueryValidator>();
        services.AddScoped<IValidator<UpdateCustomerCommand>, UpdateCustomerCommandValidator>();
        services.AddScoped<IValidator<DeleteCustomerCommand>, DeleteCustomerCommandValidator>();
        services.AddScoped<IValidator<GetAddressByPostalCodeCommand>, GetAddressByPostalCodeCommandValidator>();
        services.AddScoped<IValidator<GetCustomerByIdCommand>, GetCustomerByIdCommandValidator>();
        services.AddScoped<IValidator<GetCustomersCommand>, GetCustomersCommandValidator>();
        services.AddScoped<IValidator<ConfirmEmailCommand>, ConfirmEmailCommandValidator>();
    }
}