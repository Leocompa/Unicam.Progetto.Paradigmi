using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Paradigmi.Application.Abstractions.Services;

namespace Paradigmi.Application.Middlewares;

public class MiddlewareExample
{
    private RequestDelegate _next;

    public MiddlewareExample(RequestDelegate next)
    {
        _next = next;
    }
    
    //TODO
    public async Task Invoke(HttpContext context, IOrdineService ordineService
        , IConfiguration configuration, IPortateService portateService, IUtenteService utenteService)
    {
        context.RequestServices.GetRequiredService<IOrdineService>();
        context.RequestServices.GetRequiredService<IPortateService>();
        context.RequestServices.GetRequiredService<IUtenteService>();

        await _next.Invoke(context);
    }
}