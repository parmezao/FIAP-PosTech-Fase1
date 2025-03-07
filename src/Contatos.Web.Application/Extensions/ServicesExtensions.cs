using Contatos.Web.Application.BackgroundServices;
using Contatos.Web.Application.Interfaces;
using Contatos.Web.Application.UseCases;
using Contatos.Web.Domain.Entities;
using Contatos.Web.Domain.Interfaces;
using Contatos.Web.Infrastructure.Data.Context;
using Contatos.Web.Infrastructure.Data.Repository;
using Contatos.Web.Service.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace Contatos.Web.Application.Extensions;

public static class ServicesExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IBaseRepository<Contato>, BaseRepository<Contato>>();
        services.AddScoped<IBaseService<Contato>, BaseService<Contato>>();
        services.AddScoped<IAuthenticationUseCase, AuthenticationUseCase>();

        services.AddHostedService<CpuMetricsCollector>();
        services.AddHostedService<MemoryMetricsCollector>();

        return services;
    }

    public static IServiceCollection AddMapping(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(Program));
        return services;
    }

    public static IServiceCollection AddDbConnection(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddDbContext<SqlServerDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        return services;
    }

    public static IServiceCollection AddDocs(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        // services.AddSwaggerGen(options =>
        // {
        //     var xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        //     options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFileName));

        //     options.SwaggerDoc("v1", new OpenApiInfo
        //     {
        //         Version = "v1",
        //         Title = "Contatos",
        //         Description = "API de Contatos Regionais - FIAP Fase 1 e Fase 2"
        //     });
        // });            

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Cadastro de Contatos", Version = "v1.0" });
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description =
                "JWT Authorization Header - utilizado com Bearer Authentication.\r\n\r\n" +
                "Digite 'Bearer' [espaço] e então seu token no campo abaixo.\r\n\r\n" +
                "Exemplo (informar sem as aspas): 'Bearer 12345abcdef'",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                    Array.Empty<string>()
                }
            });

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

            c.IncludeXmlComments(xmlPath);
        });

        return services;
    }
}

