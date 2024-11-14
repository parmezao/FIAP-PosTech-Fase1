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

    /// <summary>
    /// Endpoint utilizado para cadastrar um novo Contato
    /// </summary>
    /// <param name="contato">Objeto Contato. Necessário informar o Nome, Endereço de Email, Telefone e DDD do Contato para o cadastro</param>
    /// <returns>Retorna o objeto Contato informado com o Id preenchido</returns>
    [HttpPost]
    [ValidateModel]
    public async Task<ActionResult<ResponseModel>> Create(Contato contato)
    {
        var responseModel = new ResponseModel();

        var result = await _baseService.AddAsync(contato);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, 
            responseModel.Result(StatusCodes.Status201Created, "Created", result));
    }

    /// <summary>
    /// Endpoint utilizado para listar todos os Contatos cadastrados
    /// </summary>
    /// <returns>Retorna a lista de objetos do tipo Contato</returns>
    [HttpGet]
    public async Task<ActionResult<ResponseModel>> GetAll()
    {
        var responseModel = new ResponseModel();

        var contatos = await _baseService.GetAllAsync();
        return Ok(responseModel.Result(StatusCodes.Status200OK, "OK", contatos));
    }

    /// <summary>
    /// Endpoint utilizado para localizar o Contato de acordo com o Id informado
    /// </summary>
    /// <param name="id">Id do objeto Contato. Necessário informar para localizar o Contato</param>
    /// <returns>Retorna o objeto do tipo Contato</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<ResponseModel>> GetById(int id)
    {
        var responseModel = new ResponseModel();

        var contato = await _baseService.GetByIdAsync(id);
        return Ok(responseModel.Result(StatusCodes.Status200OK, "OK", contato));
    }

    /// <summary>
    /// Endpoint utilizado para localizar o Contato de acordo com o DDD informado
    /// </summary>
    /// <param name="ddd">DDD do objeto Contato. Necessário informar para localizar o Contato</param>
    /// <returns>Retorna o objeto do tipo Contato</returns>
    [HttpGet, Route("ddd/{ddd}")]
    public async Task<ActionResult<ResponseModel>> GetByDDD(int ddd)
    {
        var responseModel = new ResponseModel();

        var contatos = await _baseService.GetAllAsync();
        
        var contatosDdd = contatos.ToList().Where(c => c.DDD == ddd);
        return Ok(responseModel.Result(StatusCodes.Status200OK, "OK", contatosDdd));
    }

    /// <summary>
    /// Endpoint utilizado para alterar um Contato existente
    /// </summary>
    /// <param name="id">Id do objeto Contato. Necessário informar para localizar o Contato que será alterado</param>
    /// <param name="contato">Objeto Contato. Necessário informar para aplicar as alterações no Contato que será alterado</param>
    /// <returns>Retorna o objeto Contato informado com o Id preenchido</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<ResponseModel>> Update(int id, Contato contato)
    {
        var responseModel = new ResponseModel();

        if (id != contato.Id)
            return BadRequest(responseModel.Result(StatusCodes.Status400BadRequest, "Erro na Requisição", contato));

        var contatoExistente = await _baseService.GetByIdAsync(id);
        contatoExistente.ChangeContatoData(contato);
        await _baseService.UpdateAsync(contatoExistente);
        
        return NoContent();
    }

    /// <summary>
    /// Endpoint utilizado para excluir o Contato de acordo com o Id informado
    /// </summary>
    /// <param name="id">Id do objeto Contato. Necessário informar para localizar o Contato que será excluído</param>
    /// <returns>Retorna o objeto do tipo Contato</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var responseModel = new ResponseModel();

        var contatoExistente = await _baseService.GetByIdAsync(id);
        await _baseService.DeleteAsync(contatoExistente.Id);
        
        return NoContent();
    }
}
