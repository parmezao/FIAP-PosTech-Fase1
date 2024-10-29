using Contatos.Web.Application.Extensions;
using Contatos.Web.Service.Validators;

var builder = WebApplication.CreateBuilder(args);

#region Configura serviço de Controllers
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

#region Adiciona a Conexão
builder.Services.AddDbConnection(builder);
#endregion

#region Adiciona a documentação (Swagger)
builder.Services.AddDocs();
#endregion

var app = builder.Build();

#region Aplica as Migrações (Migrations)
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
