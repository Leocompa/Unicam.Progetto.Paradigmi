using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Paradigmi.Application.Abstractions.Services;
using Paradigmi.Application.Services;

namespace Paradigmi.Application.Extensions;

public static class ServiceExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddValidatorsFromAssembly(
            AppDomain.CurrentDomain.GetAssemblies().SingleOrDefault(assembly =>
                assembly.GetName().Name == "Paradigmi.Application")
        );

        //TODO services
        services.AddScoped<ITokenService, TokenService>();
        return services;
    }
}