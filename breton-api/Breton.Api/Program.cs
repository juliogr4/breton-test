using Breton.Application.DependencyInjection;
using Breton.Infrastructure.DependencyInjection;

var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddControllers();
    builder.Services
        .AddApplication(builder.Configuration)
        .AddInfrastructure();

    // cqrs
    builder.Services.AddCors(options => {
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy => {
            policy
                .WithOrigins()
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
    });
}

var app = builder.Build();
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
    app.UseCors(MyAllowSpecificOrigins);
    app.Run();
}

