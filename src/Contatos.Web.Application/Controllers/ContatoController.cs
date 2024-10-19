using Contatos.Web.Domain.Entities;
using Contatos.Web.Domain.Interfaces;
using Contatos.Web.Service.Validators;
using Microsoft.AspNetCore.Mvc;

namespace Contatos.Web.Application.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContatoController(IBaseService<Contato> baseService) : ControllerBase
{
    private readonly IBaseService<Contato> _baseService = baseService;

    [HttpPost]
    public async Task<IActionResult> Create(Contato contato)
    {
        if (contato == null)
            return await Task.FromResult(NotFound());

        return await ExecuteAsync(() => _baseService.AddAsync<ContatoValidator>(contato));
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return await ExecuteAsync(() => _baseService.GetAllAsync().Result);
    }

    private async Task<IActionResult> ExecuteAsync(Func<object> func)
    {
        try
        {
            var result = func();
            return await Task.FromResult(Ok(result));
        }
        catch (Exception ex)
        {
            return await Task.FromResult(BadRequest(ex));
        }
    }
}
