using Contatos.Web.Application.Extensions;
using Contatos.Web.Service.Validators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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

#region Adiciona a Autenticação
var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

var key = Encoding.ASCII.GetBytes(configuration.GetValue<string>("SecretJWT") ?? string.Empty);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(j =>
{
    j.RequireHttpsMetadata = false;
    j.SaveToken = true;
    j.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});
#endregion

var app = builder.Build();

#region Aplica as Migrações (Migrations)
if (!WasInvoked) app.ApplyMigrations();
#endregion

#region Utiliza Métricas
app.UseMetrics();
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
    public static bool WasInvoked { private get; set; }
}
