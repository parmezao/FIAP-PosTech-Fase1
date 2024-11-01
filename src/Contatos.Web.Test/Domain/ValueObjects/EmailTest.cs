using Contatos.Web.Domain.Entities;
using Contatos.Web.Domain.ValueObjects;

namespace Contatos.Web.Tests.Domain.ValueObjects;

public class EmailTest
{
    [Fact(DisplayName = "Endereco Email Deve Falhar Quando Email Atual For Diferente Do Novo")]
    public void Endereco_Email_DeveFalhar_QuandoEmailAtualForDiferenteDoNovo()
    {
        const string novoEmail = "novo@email.com";

        // Arrange
        var valorEsperado = novoEmail;

        // Act
        Contato contato = new() {Email = new Email(string.Empty)};
        contato.Email.ChangeEmail("emailanterior@email.com");
        var valorAtual = contato.Email.Endereco;

        // Assert
        Assert.NotEqual(valorEsperado, valorAtual);
    }
}