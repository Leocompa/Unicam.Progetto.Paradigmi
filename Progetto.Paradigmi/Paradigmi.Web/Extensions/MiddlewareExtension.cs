using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Paradigmi.Application.Factories;

namespace Paradigmi.Web.Extensions;

public static class MiddlewareExtension
{
    public static WebApplication? AddWebMiddleware(this WebApplication? app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Progetto Paradigmi"); });
        }

        app.Use(async (context, next) => { await next.Invoke(); });

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature != null)
                {
                    var response = ResponseFactory.WithError(contextFeature.Error);
                    await context.Response.WriteAsJsonAsync(response);
                }
            });
        });

        app.MapControllers();
        return app;
    }
}