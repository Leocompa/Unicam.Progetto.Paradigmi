using System.Text;
using System.Text.Json.Serialization;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Paradigmi.Application.Options;
using Paradigmi.Web.Results;

namespace Paradigmi.Web.Extensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddWebServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers().ConfigureApiBehaviorOptions(opt =>
            {
                opt.InvalidModelStateResponseFactory = (context => new BadRequestResultFactory(context));
            });

            services.AddControllersWithViews().AddJsonOptions(options => options.JsonSerializerOptions.Converters
                .Add(new JsonStringEnumConverter()));
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Progetto Paradigmi",
                    Version = "V1"
                });
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description =
                        "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\""
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            });
            services.AddFluentValidationAutoValidation();

            var jwtAuthenticationOption = new JwtAuthenticationOption();
            configuration.GetSection("JwtAuthentication").Bind(jwtAuthenticationOption);

            services.AddAuthentication(options =>
                {
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    string key = jwtAuthenticationOption.Key;
                    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateLifetime = true,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtAuthenticationOption.Issuer,
                        IssuerSigningKey = securityKey
                    };
                });
            services.AddOptions(configuration);

            return services;
        }

        private static void AddOptions(this IServiceCollection service, IConfiguration configuration)
        {
            service.Configure<JwtAuthenticationOption>(
                configuration.GetSection("JwtAuthentication")
            );
        }
    }
}