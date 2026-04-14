using System.Net;
using Deimdal.CheckVIES.Client.ApiClient;
using Deimdal.CheckVIES.Client.Tests.Mocks;

namespace Deimdal.CheckVIES.Client.Tests;

public class CheckViesApiTests
{
    private const string ApiKey = "test-api-key";
    private const string BaseUrl = "https://api.test.com";

    private static ICheckViesClient CreateClient(Func<HttpRequestMessage, Task<HttpResponseMessage>> handler)
    {
        var mockHandler = new MockHttpMessageHandler(handler);
        var httpClient = new HttpClient(mockHandler)
        {
            BaseAddress = new Uri(BaseUrl)
        };

        return new CheckViesClient(httpClient)
        {
            ApiKey = ApiKey
        };
    }

    [Fact]
    public async Task GetDetailsAsync_ReturnsExpectedResult()
    {
        // Arrange
        var requestId = Guid.NewGuid();
        var expectedResponse = new VatCheckRequestWithResultDto
        {
            RequestId = requestId,
            State = RequestStateDto.Success,
            Number = "12345678",
            IsoAlpha2 = "DE",
            Result = new VatCheckResultDetailsDto { Valid = true, Name = "Test Company" }
        };

        var client = CreateClient(req =>
        {
            Assert.Equal(HttpMethod.Get, req.Method);
            Assert.Equal($"{BaseUrl}/api/check/details/{requestId}", req.RequestUri?.ToString());
            return Task.FromResult(MockHttpMessageHandler.CreateJsonResponse(expectedResponse, HttpStatusCode.OK, req));
        });

        // Act
        var result = await client.DetailsAsync(requestId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(requestId, result.RequestId);
        Assert.Equal(RequestStateDto.Success, result.State);
        Assert.True(result.Result?.Valid);
        Assert.Equal("Test Company", result.Result?.Name);
    }

    [Fact]
    public async Task StartCheckAsync_SendsCorrectRequest()
    {
        // Arrange
        var request = new StartCheckRequestDto { Number = "12345678", IsoAlpha2 = "DE" };
        var expectedResponse = new CheckRequestCreatedDto { Id = Guid.NewGuid() };

        var client = CreateClient(req =>
        {
            Assert.Equal(HttpMethod.Post, req.Method);
            Assert.Equal($"{BaseUrl}/api/check/start", req.RequestUri?.ToString());
            return Task.FromResult(MockHttpMessageHandler.CreateJsonResponse(expectedResponse, HttpStatusCode.OK, req));
        });

        // Act
        var result = await client.StartAsync(request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedResponse.Id, result.Id);
    }

    [Fact]
    public async Task GetDetailsAsync_HandlesErrorResponse()
    {
        // Arrange
        var requestId = Guid.NewGuid();
        var errorResponse = new ErrorResponse { Error_code = "NOT_FOUND", Error_text = "Request not found" };

        var client = CreateClient(req => Task.FromResult(MockHttpMessageHandler.CreateJsonResponse(errorResponse, HttpStatusCode.UnprocessableEntity, req)));

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ApiException<ErrorResponse>>(() => client.DetailsAsync(requestId));
        Assert.Equal((int)HttpStatusCode.UnprocessableEntity, exception.StatusCode);

        // Refit allows accessing the content
        var content = exception.Result;
        Assert.NotNull(content);
        Assert.Equal("NOT_FOUND", content.Error_code);
    }
}
