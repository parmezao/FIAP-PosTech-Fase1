﻿using System.Net.Mime;
using System.Text.Json;
using AutoMapper;
using Contatos.Web.Application.Extensions;
using Contatos.Web.Application.Helpers;
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
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType<ContatoDto>(StatusCodes.Status201Created)]
    [ProducesResponseType<ContatoDto>(StatusCodes.Status422UnprocessableEntity)]
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
    [ProducesResponseType<List<ContatoDto>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<ResponseModel>> GetAll([FromQuery] PaginationParameters paginationParameters)
    {
        var responseModel = new ResponseModel();

        var contatos = await _baseService.GetAllAsync();
        var contatosDto = _mapper.Map<List<ContatoDto>>(contatos);
        
        var pagedResult = contatosDto
            .AsQueryable()
            .ToPagedResult(paginationParameters.PageNumber, paginationParameters.PageSize);
        Response.Headers.Append("X-Total-Count", pagedResult.TotalItems.ToString());
        Response.Headers.Append("X-Total-Pages", pagedResult.TotalPages.ToString());
        
        return Ok(responseModel.Result(StatusCodes.Status200OK, "OK", pagedResult));
    }

    /// <summary>
    /// Endpoint utilizado para localizar o Contato de acordo com o Id informado
    /// </summary>
    /// <param name="id">Id do objeto Contato. Necessário informar para localizar o Contato</param>
    /// <returns>Retorna o objeto do tipo Contato</returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType<ContatoDto>(StatusCodes.Status200OK)]
    [ProducesResponseType<ContatoDto>(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ResponseModel>> GetById(int id)
    {
        var responseModel = new ResponseModel();

        var contato = await _baseService.GetByIdAsync(id);
        if (contato is null)
            return NotFound(responseModel.Result(StatusCodes.Status404NotFound, "Not Found", default!));
        
        var contatoDto = _mapper.Map<ContatoDto>(contato);
        return Ok(responseModel.Result(StatusCodes.Status200OK, "OK", contatoDto));
    }

    /// <summary>
    /// Endpoint utilizado para filtrar os Contatos de acordo com o DDD informado
    /// </summary>
    /// <param name="ddd">DDD do objeto Contato. Necessário informar para localizar o Contato</param>
    /// <returns>Retorna o objeto do tipo Contato</returns>
    [HttpGet, Route("ddd/{ddd:int}")]
    [ProducesResponseType<List<ContatoDto>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<ResponseModel>> GetByDdd(int ddd, 
        [FromQuery] PaginationParameters paginationParameters)
    {
        var responseModel = new ResponseModel();
        
        var contatosDdd = await _baseService.FilterAsync(c => c.DDD == ddd);
        var contatosDddDto = _mapper.Map<List<ContatoDto>>(contatosDdd);
        
        var pagedResult = contatosDddDto
            .AsQueryable()
            .ToPagedResult(paginationParameters.PageNumber, paginationParameters.PageSize);
        Response.Headers.Append("X-Total-Count", pagedResult.TotalItems.ToString());
        Response.Headers.Append("X-Total-Pages", pagedResult.TotalPages.ToString());
        
        return Ok(responseModel.Result(StatusCodes.Status200OK, "OK", pagedResult));
    }

    /// <summary>
    /// Endpoint utilizado para alterar um Contato existente
    /// </summary>
    /// <param name="id">Id do objeto Contato. Necessário informar para localizar o Contato que será alterado</param>
    /// <param name="contatoDto">Objeto Contato. Necessário informar para aplicar as alterações no Contato que será alterado</param>
    /// <returns>Retorna o objeto Contato informado com o Id preenchido</returns>
    [HttpPut("{id:int}")]
    [ValidateModel]
    [ProducesResponseType<ContatoDto>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ContatoDto>(StatusCodes.Status204NoContent)]
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
    [ProducesResponseType<ContatoDto>(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(int id)
    {
        var contatoExistente = await _baseService.GetByIdAsync(id);
        await _baseService.DeleteAsync(contatoExistente.Id);
        
        return NoContent();
    }
}
