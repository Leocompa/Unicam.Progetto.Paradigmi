using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Paradigmi.Application.Abstractions.Services;
using Paradigmi.Application.Services;
using Paradigmi.Models.Entities;

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

        
        services.AddScoped<IUtenteService, UtenteService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IOrdineService, OrdineService>();
        services.AddScoped<IPortateService, PortateService>();
        services.AddScoped<IPortateOrdinateService, PortateOrdinateService>();
        
        return services;
    }
}