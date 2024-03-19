using Microsoft.AspNetCore.Builder;
using Paradigmi.Abstraction;
using Paradigmi.Application.Extensions;
using Paradigmi.Models.Extensions;
using Paradigmi.Web.Extensions;
using Progetto.Paradigmi.Test.Example;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddWebServices(builder.Configuration)
    .AddApplicationServices(builder.Configuration)
    .AddModelServices(builder.Configuration);

var app = builder.Build();

app.AddWebMiddleware().AddApplicationMiddleware();

app.Run();