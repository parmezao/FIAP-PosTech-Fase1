
namespace Contatos.Web.Domain.ValueObjects;

public class Email(string endereco)
{
    public string Endereco { get; set; } = endereco;    
}
