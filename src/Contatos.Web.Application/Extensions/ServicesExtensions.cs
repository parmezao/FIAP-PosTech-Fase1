using System.Reflection;
using Contatos.Web.Domain.Entities;
using Contatos.Web.Domain.Interfaces;
using Contatos.Web.Infrastructure.Data.Context;
using Contatos.Web.Infrastructure.Data.Repository;
using Contatos.Web.Service.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace Contatos.Web.Application.Extensions;

public static class ServicesExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IBaseRepository<Contato>, BaseRepository<Contato>>();
        services.AddScoped<IBaseService<Contato>, BaseService<Contato>>();

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
        services.AddSwaggerGen(options =>
        {
            var xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFileName));
            
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Contatos",
                Description = "API de Contatos Regionais - FIAP Fase 1"
            });
        });            

        return services;
    }
}

