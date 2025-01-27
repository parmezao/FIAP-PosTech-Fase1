using Contatos.Web.Application.Extensions;
using Contatos.Web.Service.Validators;
using Prometheus;

var builder = WebApplication.CreateBuilder(args);

#region Adiciona Logging
builder.Logging.ClearProviders().AddConsole();
#endregion

#region Configura serviço de Controllers e Routes
builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var result = new ValidationFailedResult(context.ModelState);
        return result;
    };
});

builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseQueryStrings = options.LowercaseUrls = true;
});
#endregion

#region Adiciona os Serviços
builder.Services.AddServices();
#endregion

#region Adiciona Mapeamentos
builder.Services.AddMapping();
#endregion

#region Adiciona a Conexão
builder.Services.AddDbConnection(builder);
#endregion

#region Adiciona a documentação (Swagger)
builder.Services.AddDocs();
#endregion

var app = builder.Build();

#region Aplica as Migrações (Migrations)
if (!WasInvoked) app.ApplyMigrations();
#endregion

#region Adiciona o endpoint de métricas
app.UseMetricServer();
app.UseHttpMetrics(); // Coleta métricas de requisições HTTP automaticamente
#endregion

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

public partial class Program
{
    public static bool WasInvoked { get; set; }

    public static void Main(string[] args)
    {
        WasInvoked = true; // Aplicação foi inicializada através dos testes
    }

}
