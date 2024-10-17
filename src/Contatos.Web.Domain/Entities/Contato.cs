namespace Contatos.Web.Domain.Entities;

public class Contato : BaseEntity
{
    public string? Nome { get; set; }
    public string? Email { get; set; }
    public string? Telefone { get; set; }
}
