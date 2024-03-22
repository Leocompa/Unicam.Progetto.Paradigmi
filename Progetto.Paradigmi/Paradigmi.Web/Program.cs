using Microsoft.AspNetCore.Builder;
using Paradigmi.Application.Extensions;
using Paradigmi.Models.Extensions;
using Paradigmi.Web.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddWebServices(builder.Configuration)
    .AddApplicationServices(builder.Configuration)
    .AddModelServices(builder.Configuration);

var app = builder.Build();

app.AddWebMiddleware().AddApplicationMiddleware();

app.Run();