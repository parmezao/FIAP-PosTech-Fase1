using AutoMapper;
using Contatos.Web.Domain.Entities;
using Contatos.Web.Domain.Interfaces;
using Contatos.Web.Service.Validators;
using Contatos.Web.Shared.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Contatos.Web.Application.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContatoController(IBaseService<Contato> baseService, IMapper mapper) : ControllerBase
{
    private readonly IBaseService<Contato> _baseService = baseService;
    private readonly IMapper _mapper = mapper;

    /// <summary>
    /// Endpoint utilizado para cadastrar um novo Contato
    /// </summary>
    /// <param name="contatoDto">Necessário informar o Nome, Endereço de Email, Telefone e DDD do Contato para o cadastro</param>
    /// <returns>Retorna o objeto ContatoDto informado com o Id preenchido</returns>
    [HttpPost]
    [ValidateModel]
    public async Task<ActionResult<ResponseModel>> Create(ContatoDto contatoDto)
    {
        var responseModel = new ResponseModel();

        var contato = _mapper.Map<Contato>(contatoDto);
        var result = await _baseService.AddAsync(contato);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, 
            responseModel.Result(StatusCodes.Status201Created, "Created", result));
    }

    /// <summary>
    /// Endpoint utilizado para listar todos os Contatos cadastrados
    /// </summary>
    /// <returns>Retorna a lista de objetos do tipo ContatoDto</returns>
    [HttpGet]
    public async Task<ActionResult<ResponseModel>> GetAll()
    {
        var responseModel = new ResponseModel();

        var contatos = await _baseService.GetAllAsync();
        var contatosDto = _mapper.Map<List<ContatoDto>>(contatos);
        return Ok(responseModel.Result(StatusCodes.Status200OK, "OK", contatosDto));
    }

    /// <summary>
    /// Endpoint utilizado para localizar o Contato de acordo com o Id informado
    /// </summary>
    /// <param name="id">Id do objeto Contato. Necessário informar para localizar o Contato</param>
    /// <returns>Retorna o objeto do tipo Contato</returns>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ResponseModel>> GetById(int id)
    {
        var responseModel = new ResponseModel();

        var contato = await _baseService.GetByIdAsync(id);
        var contatoDto = _mapper.Map<ContatoDto>(contato);
        return Ok(responseModel.Result(StatusCodes.Status200OK, "OK", contatoDto));
    }

    /// <summary>
    /// Endpoint utilizado para localizar o Contato de acordo com o DDD informado
    /// </summary>
    /// <param name="ddd">DDD do objeto Contato. Necessário informar para localizar o Contato</param>
    /// <returns>Retorna o objeto do tipo Contato</returns>
    [HttpGet, Route("ddd/{ddd:int}")]
    public async Task<ActionResult<ResponseModel>> GetByDDD(int ddd)
    {
        var responseModel = new ResponseModel();
        
        var contatosDdd = await _baseService.FilterAsync(c => c.DDD == ddd);
        var contatosDddDto = _mapper.Map<List<ContatoDto>>(contatosDdd);
        return Ok(responseModel.Result(StatusCodes.Status200OK, "OK", contatosDddDto));
    }

    /// <summary>
    /// Endpoint utilizado para alterar um Contato existente
    /// </summary>
    /// <param name="id">Id do objeto Contato. Necessário informar para localizar o Contato que será alterado</param>
    /// <param name="contatoDto">Objeto Contato. Necessário informar para aplicar as alterações no Contato que será alterado</param>
    /// <returns>Retorna o objeto Contato informado com o Id preenchido</returns>
    [HttpPut("{id:int}")]
    public async Task<ActionResult<ResponseModel>> Update(int id, ContatoDto contatoDto)
    {
        if (id != contatoDto.Id)
        {
            var responseModel = new ResponseModel();
            return BadRequest(responseModel.Result(
                StatusCodes.Status400BadRequest, "Erro na Requisição", contatoDto));
        }

        var contatoExistente = await _baseService.GetByIdAsync(id);
        _mapper.Map(contatoDto, contatoExistente);
        await _baseService.UpdateAsync(contatoExistente);
        
        return NoContent();
    }

    /// <summary>
    /// Endpoint utilizado para excluir o Contato de acordo com o Id informado
    /// </summary>
    /// <param name="id">Id do objeto Contato. Necessário informar para localizar o Contato que será excluído</param>
    /// <returns>Retorna o objeto do tipo Contato</returns>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var contatoExistente = await _baseService.GetByIdAsync(id);
        await _baseService.DeleteAsync(contatoExistente.Id);
        
        return NoContent();
    }
}
