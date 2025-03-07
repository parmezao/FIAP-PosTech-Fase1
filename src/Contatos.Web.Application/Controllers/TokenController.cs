using Contatos.Web.Application.Interfaces;
using Contatos.Web.Shared.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Contatos.Web.Application.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TokenController(IAuthenticationUseCase tokenService) : ControllerBase
{
    private readonly IAuthenticationUseCase _authenticationService = tokenService;

    /// <summary>
    /// Solicitação de token JwtBearer 
    /// </summary>
    /// <param name="usuario">Usuário do sistema</param>
    /// <returns>token</returns>
    /// <response code="200">Token fornecido com sucesso</response>
    /// <response code="401">Usuário/Senha inválido (não autorizado)</response>
    [HttpPost]
    public IActionResult Post([FromBody] UserDto usuario)
    {
        var token = _authenticationService.GetToken(usuario);

        if (!string.IsNullOrWhiteSpace(token))
        {
            return Ok(token);
        }

        return Unauthorized();
    }

}
