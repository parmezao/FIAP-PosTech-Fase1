using Contatos.Web.Domain.Entities;
using Contatos.Web.Domain.Interfaces;
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

        var result = await _baseService.AddAsync(contato);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var contatos = await _baseService.GetAllAsync();
        if (contatos == null)
            return NotFound();

        return Ok(contatos);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var contato = await _baseService.GetByIdAsync(id);
        if (contato is null)
            return NotFound();

        return Ok(contato);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Contato contato)
    {
        if (id != contato.Id)
            return BadRequest();

        var contatoExistente = await _baseService.GetByIdAsync(id);
        if (contatoExistente is null)
            return NotFound();

        contatoExistente.Nome = contato.Nome;
        contatoExistente.Email = contato.Email;
        contatoExistente.Telefone = contato.Telefone;
        contatoExistente.DDD = contato.DDD;

        await _baseService.UpdateAsync(contatoExistente);
        return NoContent();
    }
}
