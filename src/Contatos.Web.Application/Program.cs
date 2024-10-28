using Contatos.Web.Application.Extensions;
using Contatos.Web.Service.Validators;
using System.Net.Mime;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

#region Configura servi�o de Controllers
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

#region Adiciona os Servi�os
builder.Services.AddServices();
#endregion

#region Adiciona a Conex�o
builder.Services.AddDbConnection(builder);
#endregion

#region Adiciona a documenta��o (Swagger)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFileName));
});
#endregion

var app = builder.Build();

#region Aplica as Migra��es (Migrations)
app.ApplyMigrations();
#endregion


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
