using Contatos.Web.Domain.Entities;
using Contatos.Web.Domain.ValueObjects;

namespace Contatos.Web.Tests.Domain.ValueObjects;

public class EmailTest
{
    [Fact(DisplayName = "Deve falhar quando email anterior for diferente do atual")]
    public void Deve_falhar_quando_email_anterior_for_diferente_do_atual()
    {
        const string novo_email = "novo@email.com";

        // Arrange
        string valorEsperado = novo_email;

        // Act
        Contato contato = new() {Email = new Email(string.Empty)};
        contato.Email.ChangeEmail("emailanterior@email.com");
        string valorAtual = contato.Email.Endereco;

        // Assert
        Assert.NotEqual(valorEsperado, valorAtual);
    }
}