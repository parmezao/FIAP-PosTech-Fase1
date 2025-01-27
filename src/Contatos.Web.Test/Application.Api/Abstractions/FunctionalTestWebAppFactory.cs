using Contatos.Web.Infrastructure.Data.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Contatos.Web.Tests.Application.Api.Abstractions;

public class FunctionalTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var serviceProvider = services.BuildServiceProvider();
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            Program.WasInvoked = true; // Indica que a classe Program foi invocada para que as migrações não sejam aplicadas no contexto de testes (InMemoryDatabase)

            services.Remove(services.Single(a => typeof(DbContextOptions<SqlServerDbContext>) == a.ServiceType));
            // services.AddDbContext<SqlServerDbContext>(options => options
            //     .UseSqlServer(configuration.GetConnectionString("DefaultConnection")!));

            services.AddDbContext<SqlServerDbContext>(options => options
              .UseInMemoryDatabase("TestDb"));
        });
    }

    public async Task InitializeAsync()
    {
        await Task.CompletedTask;
    }

    public new async Task DisposeAsync()
    {
        await Task.CompletedTask;
    }
}