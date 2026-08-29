using Soenneker.Make.OpenApiClient;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Make.OpenApiClientUtil.Abstract;

/// <summary>
/// Exposes a cached OpenAPI client instance.
/// </summary>
public interface IMakeOpenApiClientUtil: IDisposable, IAsyncDisposable
{
    /// <summary>
    /// Returns the configured make Open API Client used by the make open api client.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task whose result is the requested make Open API Client.</returns>
    ValueTask<MakeOpenApiClient> Get(CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a client for a specific API key using the configured base URL.
    /// </summary>
    /// <param name="apiKey">API key used to authenticate the request.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task whose result is the requested make Open API Client.</returns>
    ValueTask<MakeOpenApiClient> Get(string apiKey, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a client for a specific Make connection.
    /// </summary>
    /// <param name="apiKey">API key used to authenticate the request.</param>
    /// <param name="baseUrl">URL of the base to target.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task whose result is the requested make Open API Client.</returns>
    ValueTask<MakeOpenApiClient> Get(string apiKey, string baseUrl, CancellationToken cancellationToken = default);
}
