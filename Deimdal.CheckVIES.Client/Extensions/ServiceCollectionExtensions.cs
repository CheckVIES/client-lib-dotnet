using System;
using Microsoft.Extensions.DependencyInjection;
using Deimdal.CheckVIES.Client.ApiClient;
using JetBrains.Annotations;

namespace Deimdal.CheckVIES.Client.Extensions;

/// <summary>
/// Extension methods for registering CheckVIES API client
/// </summary>
[PublicAPI]
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds CheckVIES client to the service collection
    /// </summary>
    /// <param name="services">The service collection</param>
    /// <param name="apiKey">The API key for CheckVIES service</param>
    /// <param name="baseUrl">Optional base URL for the API. Defaults to https://api.checkvies.com</param>
    /// <returns>The service collection for chaining</returns>
    public static IServiceCollection AddCheckViesClient(this IServiceCollection services, string apiKey, string baseUrl = "https://api.checkvies.com")
    {
        if (string.IsNullOrEmpty(apiKey))
            throw new ArgumentException("API key must be provided.", nameof(apiKey));

        services.AddHttpClient<ICheckViesClient, CheckViesClient>(client => client.BaseAddress = new Uri(baseUrl));

        return services;
    }
}
