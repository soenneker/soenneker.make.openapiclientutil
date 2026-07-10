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
    /// Gets the value.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task containing the result of the operation.</returns>
    ValueTask<MakeOpenApiClient> Get(CancellationToken cancellationToken = default);

    /// <summary>Gets a client for a specific API key using the configured base URL.</summary>
    ValueTask<MakeOpenApiClient> Get(string apiKey, CancellationToken cancellationToken = default);

    /// <summary>Gets a client for a specific Make connection.</summary>
    ValueTask<MakeOpenApiClient> Get(string apiKey, string baseUrl, CancellationToken cancellationToken = default);
}
