using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Soenneker.Extensions.Configuration;
using Soenneker.Extensions.ValueTask;
using Soenneker.Make.HttpClients.Abstract;
using Soenneker.Make.OpenApiClientUtil.Abstract;
using Soenneker.Make.OpenApiClient;
using Soenneker.Kiota.GenericAuthenticationProvider;
using Soenneker.Utils.AsyncSingleton;

namespace Soenneker.Make.OpenApiClientUtil;

///<inheritdoc cref="IMakeOpenApiClientUtil"/>
public sealed class MakeOpenApiClientUtil : IMakeOpenApiClientUtil
{
    private readonly AsyncSingleton<MakeOpenApiClient> _client;

    public MakeOpenApiClientUtil(IMakeOpenApiHttpClient httpClientUtil, IConfiguration configuration)
    {
        _client = new AsyncSingleton<MakeOpenApiClient>(async token =>
        {
            HttpClient httpClient = await httpClientUtil.Get(token).NoSync();

            var apiKey = configuration.GetValueStrict<string>("Make:ApiKey");
            string authHeaderValueTemplate = configuration["Make:AuthHeaderValueTemplate"] ?? "Bearer {token}";
            string authHeaderValue = authHeaderValueTemplate.Replace("{token}", apiKey, StringComparison.Ordinal);

            var requestAdapter = new HttpClientRequestAdapter(new GenericAuthenticationProvider(headerValue: authHeaderValue), httpClient: httpClient);

            return new MakeOpenApiClient(requestAdapter);
        });
    }

    public ValueTask<MakeOpenApiClient> Get(CancellationToken cancellationToken = default)
    {
        return _client.Get(cancellationToken);
    }

    public void Dispose()
    {
        _client.Dispose();
    }

    public ValueTask DisposeAsync()
    {
        return _client.DisposeAsync();
    }
}
