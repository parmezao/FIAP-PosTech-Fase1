using Contatos.Web.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Contatos.Web.Application.Extensions;

public static class ApplicationExtensions
{
    public static IApplicationBuilder ApplyMigrations(this WebApplication app)
    {
        using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
        {
            var context = serviceScope.ServiceProvider.GetService<SqlServerDbContext>();
            context!.Database.Migrate();
        }

        return app;
    }
}
