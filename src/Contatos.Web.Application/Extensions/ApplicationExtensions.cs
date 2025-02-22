using Contatos.Web.Application.Middlewares;
using Contatos.Web.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Prometheus;

namespace Contatos.Web.Application.Extensions;

public static class ApplicationExtensions
{
    public static IApplicationBuilder ApplyMigrations(this WebApplication app)
    {
        using var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var context = serviceScope.ServiceProvider.GetService<SqlServerDbContext>();
        context!.Database.Migrate();

        return app;
    }

    public static IApplicationBuilder UseMetrics(this IApplicationBuilder builder) 
    {
        builder.UseMiddleware<RequestMetricsMiddleware>(); // Adiciona o monitoramento de latência
        builder.UseMetricServer();
        builder.UseHttpMetrics(); // Coleta métricas de requisições HTTP automaticamente

        return builder;
    }
}
