namespace Contatos.Web.Tests.Application.Api.Abstractions;

public class BaseFunctionalTests(FunctionalTestWebAppFactory webAppFactory) : IClassFixture<FunctionalTestWebAppFactory>
{
    protected HttpClient HttpClient { get; init; } = webAppFactory.CreateClient();
}