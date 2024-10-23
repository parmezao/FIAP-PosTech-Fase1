using Contatos.Web.Domain.Entities;
using Contatos.Web.Domain.ValueObjects;

namespace Contatos.Web.Tests;

public class ContatoTest
{
    [Fact(DisplayName = "Deve falhar quando nome for nulo")]
    public void Deve_falhar_quando_nome_for_nulo()
    {
        // Arrange        
        string valorEsperado = null;

        // Act
        var contato = new Contato();
        string valorAtual = contato.Nome;

        // Assert
        Assert.Equal(valorEsperado, valorAtual);
    }

    [Fact(DisplayName = "Deve falhar quando nome estiver em branco")]
    public void Deve_falhar_quando_nome_estiver_em_branco()
    {
        const string nome_vazio = "";

        // Arrange
        string valorEsperado = nome_vazio;

        // Act
        Contato contato = new() { Nome = nome_vazio };
        string valorAtual = contato.Nome;

        // Assert
        Assert.Equal(valorEsperado, valorAtual);
    }

    [Fact(DisplayName = "Deve passar quando nome estiver preenchido")]
    public void Deve_passar_quando_nome_estiver_preenchido()
    {
        const string nome_contato = "Nome_Contato";

        // Arrange
        string valorEsperado = nome_contato;

        // Act
        var contato = new Contato() { Nome = nome_contato };
        string valorAtual = contato.Nome;

        // Assert
        Assert.Equal(valorEsperado, valorAtual);
    }

    [Fact(DisplayName = "Deve passar se o email for alterado com sucesso")]
    public void Deve_passar_se_o_email_for_alterado_com_sucesso()
    {
        const string novo_endereco_email = "novo@email.com";

        // Arrange
        string valorEsperado = novo_endereco_email;

        // Act
        var contato = new Contato();
        contato.ChangeEmail(new Email(novo_endereco_email));
        string valorAtual = contato.Email.Endereco;

        // Assert
        Assert.Equal(valorEsperado, valorAtual);
    }

}