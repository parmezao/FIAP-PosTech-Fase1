using Contatos.Web.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace Contatos.Web.Domain.Entities;

public class Contato : BaseEntity
{
    [Required(ErrorMessage ="Campo Nome é obrigatório!")]
    [MaxLength(100, ErrorMessage = "Limite máximo atingido! Máximo de 100 caracteres")]
    public string? Nome { get; set; }
    
    public Email Email { get; set; } = new Email(string.Empty);

    [Required(ErrorMessage = "Campo Telefone é obrigatório!")]
    [MaxLength(10, ErrorMessage = "Limite máximo atingido! Máximo de 10 caracteres")]
    [RegularExpression("^[0-9]*$", ErrorMessage = "Campo Telefone aceita somente números!")]
    public string? Telefone { get; set; }

    [Required(ErrorMessage = "Campo DDD é obrigatório!")]
    [Range(10, 99, ErrorMessage = "Mínimo e Máximo de 2 caracteres!")]
    [RegularExpression("^[0-9]*$", ErrorMessage = "Campo DDD aceita somente números!")]
    public int DDD { get; set; }

    public void ChangeData(Contato contato)
    {
        Nome = contato.Nome;
        Email = contato.Email;
        Telefone = contato.Telefone;
        DDD = contato.DDD;
    }
}


