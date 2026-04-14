using System;
using System.Net.Http;
using JetBrains.Annotations;

namespace Deimdal.CheckVIES.Client.ApiClient;

/// <summary>
/// CheckVIES API client
/// </summary>
/// <remarks>Set <see cref="ApiKey"/> prior to calling methods</remarks>
[PublicAPI]
public partial class CheckViesClient
{
    /// <summary>
    /// API key HTTP header
    /// </summary>
    [PublicAPI] public const string ApiKeyHeader = "X-API-Key";

    /// <summary>
    /// Service API key
    /// </summary>
    /// <remarks>Can be obtained from the API keys area</remarks>
    public string ApiKey { get; set; }

    partial void PrepareRequest(HttpClient client, HttpRequestMessage request, string url)
    {
        var key = ApiKey ?? throw new Exception($"{nameof(ApiKey)} must be set prior to calling this method");
        request.Headers.Add(ApiKeyHeader, key);
    }
}
