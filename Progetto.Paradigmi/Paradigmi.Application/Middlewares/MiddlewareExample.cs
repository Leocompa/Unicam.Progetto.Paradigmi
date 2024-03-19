using Microsoft.AspNetCore.Http;

namespace Paradigmi.Application.Middlewares;

public class MiddlewareExample
{
    private RequestDelegate _next;

    public MiddlewareExample(RequestDelegate next)
    {
        _next = next;
    }
    
    //TODO
    public async Task Invoke(HttpContext context)
    {
        
    }
}