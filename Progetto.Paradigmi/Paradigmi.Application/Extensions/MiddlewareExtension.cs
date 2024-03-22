using Microsoft.AspNetCore.Builder;
using Paradigmi.Application.Middlewares;

namespace Paradigmi.Application.Extensions;

public static class MiddlewareExtension
{
    public static WebApplication? AddApplicationMiddleware(this WebApplication? app)
    {
        app.UseMiddleware<MiddlewareExample>();
        return app;
    }
}