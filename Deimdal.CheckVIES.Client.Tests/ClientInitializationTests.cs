using Microsoft.Extensions.DependencyInjection;
using Deimdal.CheckVIES.Client.ApiClient;
using Deimdal.CheckVIES.Client.Extensions;

namespace Deimdal.CheckVIES.Client.Tests;

public class ClientInitializationTests
{
    private const string ApiKey = "test-api-key";
    private const string BaseUrl = "https://api.test.com";

    [Fact]
    public void Factory_Create_ReturnsInstanceWithCorrectConfig()
    {
        // Act
        var client = CheckViesClientFactory.Create(ApiKey, BaseUrl);

        // Assert
        Assert.NotNull(client);
        var concreteClient = Assert.IsType<CheckViesClient>(client);
        Assert.Equal(ApiKey, concreteClient.ApiKey);
    }

    [Fact]
    public void DI_AddCheckViesClient_RegistersClient()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddCheckViesClient(ApiKey, BaseUrl);
        var provider = services.BuildServiceProvider();
        var client = provider.GetService<ICheckViesClient>();

        // Assert
        Assert.NotNull(client);
        Assert.IsType<CheckViesClient>(client);
    }
}
