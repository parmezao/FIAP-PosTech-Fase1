using Contatos.Web.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;
using Contatos.Web.Domain.Attributes;

namespace Contatos.Web.Domain.Entities;

public class Contato : BaseEntity
{
    public Contato() {}

    [Required(ErrorMessage ="Campo Nome é obrigatório!")]
    public string? Nome { get; set; }

    [Required(ErrorMessage = "Campo Email é obrigatório!")]
    [EmailAddress(ErrorMessage ="Email inválido!")]
    public Email Email { get; set; }

    [Required(ErrorMessage = "Campo Telefone é obrigatório!")]
    [TelefoneValido(ErrorMessage ="Telefone inválido!")]
    public string? Telefone { get; set; }

    [Required(ErrorMessage = "Campo DDD é obrigatório!")]
    [DDDValido(ErrorMessage ="DDD Inválido!")]
    public int DDD { get; set; }

    public bool ChangeEmail(Email email)
    {
        Email = email;
        return Email.Equals(email);
    }

    public void ChangeData(Contato contato)
    {
        Nome = contato.Nome;
        Email = contato.Email;
        Telefone = contato.Telefone;
        DDD = contato.DDD;
    }
}


