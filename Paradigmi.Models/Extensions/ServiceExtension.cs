using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Paradigmi.Models.Context;
using Paradigmi.Models.Repositories;

namespace Paradigmi.Models.Extensions;

public static class ServiceExtension
{
    public static IServiceCollection AddModelServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<MyDbContext>(conf =>
        {
            conf.UseSqlServer(configuration.GetConnectionString("MyDbContext"));
        });
        services.AddScoped<UtenteRepository>();
        services.AddScoped<PortataRepository>();
        services.AddScoped<OrdineRepository>();
        services.AddScoped<PortateOrdinateRepository>();

        return services;
    }
}