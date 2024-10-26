
using System.ComponentModel.DataAnnotations;

namespace Contatos.Web.Domain.ValueObjects;

public class Email(string endereco)
{
    [Required(ErrorMessage = "Campo Email é obrigatório!")]
    [EmailAddress(ErrorMessage = "Email inválido!")]
    public string Endereco { get; set; } = endereco;

    public bool ChangeEmail(Email email)
    {
        Endereco = email.Endereco;
        return Endereco.Equals(email.Endereco);
    }
}
