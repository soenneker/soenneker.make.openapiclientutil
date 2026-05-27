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
    ValueTask<MakeOpenApiClient> Get(CancellationToken cancellationToken = default);
}
