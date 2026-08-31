using Soenneker.Make.OpenApiClient;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Make.OpenApiClientUtil.Abstract;

/// <summary>
/// Provides cached Make API clients backed by the configured HTTP provider.
/// </summary>
public interface IMakeOpenApiClientUtil : IDisposable, IAsyncDisposable
{
    /// <summary>
    /// Gets a client using the configured API key and base URL.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>The configured Make client.</returns>
    ValueTask<MakeOpenApiClient> Get(CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a client for a specific API key using the configured base URL.
    /// </summary>
    /// <param name="apiKey">API key used to authenticate the request.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>The configured Make client.</returns>
    ValueTask<MakeOpenApiClient> Get(string apiKey, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a client for a specific Make connection.
    /// </summary>
    /// <param name="apiKey">API key used to authenticate the request.</param>
    /// <param name="baseUrl">Absolute Make API base URL to target.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>The configured Make client.</returns>
    ValueTask<MakeOpenApiClient> Get(string apiKey, string baseUrl, CancellationToken cancellationToken = default);
}
