using Contatos.Web.Infrastructure.CrossCutting.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Contatos.Web.Domain.Entities;

public class Contato : BaseEntity
{
    [Required(ErrorMessage ="Campo Nome é obrigatório!")]
    public string? Nome { get; set; }

    [Required(ErrorMessage = "Campo Email é obrigatório!")]
    [EmailAddress(ErrorMessage ="Email inválido!")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Campo Telefone é obrigatório!")]
    [TelefoneValido(ErrorMessage ="Telefone inválido!")]
    public string? Telefone { get; set; }

    [Required(ErrorMessage = "Campo DDD é obrigatório!")]
    public int DDD { get; set; }
}


