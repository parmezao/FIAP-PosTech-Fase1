using Contatos.Web.Shared.DTO;

namespace Contatos.Web.Application.Interfaces;

public interface IAuthenticationUseCase
{
    public string GetToken(UserDto usuario);
}
