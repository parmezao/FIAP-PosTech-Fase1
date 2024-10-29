
using System.ComponentModel.DataAnnotations;

namespace Contatos.Web.Domain.ValueObjects;

public class Email(string endereco)
{
    [Required(ErrorMessage = "Campo Email é obrigatório!")]
    [EmailAddress(ErrorMessage = "Email inválido!")]
    [MaxLength(60, ErrorMessage ="Limite máximo atingido! Máximo de 60 caracteres")]
    public string Endereco { get; set; } = endereco;

    public bool ChangeEmail(string email)
    {
        Endereco = email;
        return Endereco.Equals(email);
    }
}
