using System;
using System.Net.Http;
using Deimdal.CheckVIES.Client.ApiClient;

namespace Deimdal.CheckVIES.Client;

/// <summary>
/// Factory for manual instantiation of CheckViesClient
/// </summary>
public static class CheckViesClientFactory
{
    /// <summary>
    /// Creates a new instance of CheckViesClient
    /// </summary>
    /// <param name="apiKey">The API key for CheckVIES service</param>
    /// <param name="baseUrl">Optional base URL for the API. Defaults to https://api.checkvies.com</param>
    /// <returns>A new instance of ICheckViesClient</returns>
    public static ICheckViesClient Create(string apiKey, string baseUrl = "https://api.checkvies.com")
    {
        if (string.IsNullOrEmpty(apiKey))
            throw new ArgumentException("API key must be provided.", nameof(apiKey));

        var httpClient = new HttpClient
        {
            BaseAddress = new Uri(baseUrl)
        };

        // The generated CheckViesClient requires HttpClient in constructor
        var client = new CheckViesClient(httpClient)
        {
            ApiKey = apiKey
        };

        return client;
    }
}
