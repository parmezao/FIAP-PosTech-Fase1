using Contatos.Web.Domain.Entities;
using Contatos.Web.Domain.ValueObjects;
using System.Text.RegularExpressions;

namespace Contatos.Web.Tests;

public class ContatoTest
{
    #region Propriedade "Nome"
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

    [Fact(DisplayName = "Deve falhar quando nome ultrapassar limite de caracteres")]
    public void Deve_falhar_quando_nome_ultrapassar_limite_de_caracteres()
    {
        const bool nome_maior_que_100 = true;

        // Arrange
        bool valorEsperado = nome_maior_que_100;

        // Act
        Contato contato = new();
        contato.Nome = "ndflhweifosndflsjdhnvchoehowijedfhoidhfoiwjhofoifhoifhwoihfeo" +
            "hasjfhjfgkjhrlpfjsdfjsdfsfsdfdfdhfcnohurhife";

        bool valorAtual = contato.Nome?.Length > 100;

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
    #endregion

    #region Propriedade "Email - campo Endereço"
    [Fact(DisplayName = "Deve falhar quando email ultrapassar limite de caracteres")]
    public void Deve_falhar_quando_email_ultrapassar_limite_de_caracteres()
    {
        const bool endereco_maior_que_100 = true;

        // Arrange
        bool valorEsperado = endereco_maior_que_100;

        // Act
        Contato contato = new();
        contato.Email.Endereco = "ndflhweifosndflsjdhnvchoehowijedfhoidhfoiwjhofoifhoifhwoihfeo" +
            "hasjfhjfgkjhrlpfjsdfjsdfsfsdfdfdhfcnohurhife";

        bool valorAtual = contato.Email.Endereco.Length > 100;

        // Assert
        Assert.Equal(valorEsperado, valorAtual);
    }

    #endregion

    #region Propriedade "Telefone"
    [Fact]
    public void Deve_falhar_se_telefone_tiver_mais_de_10_caracteres()
    {        
        const string TELEFONE_MAIOR_QUE_10_CARACTERES = "98888776600";

        // Arrange
        const int LIMITE_MAXIMO_CARACTERES = 10;

        // Act
        var contato = new Contato();
        contato.Telefone = TELEFONE_MAIOR_QUE_10_CARACTERES;
        int valorAtual = contato.Telefone.Length;

        // Assert
        Assert.True(valorAtual > LIMITE_MAXIMO_CARACTERES);
    }

    [Fact(DisplayName = "Deve falhar quando telefone for nulo")]
    public void Deve_falhar_quando_telefone_for_nulo()
    {
        // Arrange        
        string valorEsperado = null;

        // Act
        var contato = new Contato();
        string valorAtual = contato.Telefone;

        // Assert
        Assert.Equal(valorEsperado, valorAtual);
    }

    [Fact(DisplayName = "Deve falhar quando telefone estiver em branco")]
    public void Deve_falhar_quando_telefone_estiver_em_branco()
    {
        const string telefone_vazio = "";

        // Arrange
        string valorEsperado = telefone_vazio;

        // Act
        Contato contato = new() { Telefone = telefone_vazio };
        string valorAtual = contato.Telefone;

        // Assert
        Assert.Equal(valorEsperado, valorAtual);
    }

    [Fact(DisplayName = "Deve falhar quando telefone possuir algum caractere alfanumerico")]
    public void Deve_falhar_quando_telefone_possuir_algum_caractere_alfanumerico()
    {
        const string TELEFONE_COM_CARACTERE_ALFANUMERICO = "9888877X6";

        var contato = new Contato();
        contato.Telefone = TELEFONE_COM_CARACTERE_ALFANUMERICO;

        // Arrange
        bool valorEsperado = !Regex.IsMatch(contato.Telefone, "^[0-9]*$");

        // Act

        // Assert
        Assert.True(valorEsperado);
    }

    #endregion
}