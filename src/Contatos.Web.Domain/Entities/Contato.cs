using System.ComponentModel.DataAnnotations;

namespace Contatos.Web.Domain.Entities;

public class Contato : BaseEntity
{
    [Required(ErrorMessage ="Campo Nome é obrigatório!")]
    public string Nome { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public string Telefone { get; set; }

    public int DDD { get; set; }
}
