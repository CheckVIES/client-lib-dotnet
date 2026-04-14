using System.Net;
using System.Text.Json;

namespace Deimdal.CheckVIES.Client.Tests.Mocks;

public class MockHttpMessageHandler(Func<HttpRequestMessage, Task<HttpResponseMessage>> handler) : HttpMessageHandler
{
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        return handler(request);
    }

    public static HttpResponseMessage CreateJsonResponse(object body, HttpStatusCode statusCode = HttpStatusCode.OK, HttpRequestMessage? request = null)
    {
        return new HttpResponseMessage(statusCode)
        {
            Content = new StringContent(JsonSerializer.Serialize(body), System.Text.Encoding.UTF8, "application/json"),
            RequestMessage = request
        };
    }
}
