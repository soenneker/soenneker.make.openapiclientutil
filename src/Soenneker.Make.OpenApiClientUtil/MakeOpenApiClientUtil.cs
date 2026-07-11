using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Soenneker.Dictionaries.Singletons;
using Soenneker.Extensions.Configuration;
using Soenneker.Extensions.ValueTask;
using Soenneker.Make.HttpClients.Abstract;
using Soenneker.Make.OpenApiClientUtil.Abstract;
using Soenneker.Make.OpenApiClient;
using Soenneker.Kiota.GenericAuthenticationProvider;

namespace Soenneker.Make.OpenApiClientUtil;

///<inheritdoc cref="IMakeOpenApiClientUtil"/>
public sealed class MakeOpenApiClientUtil : IMakeOpenApiClientUtil
{
    private readonly SingletonDictionary<MakeOpenApiClient> _clients;
    private readonly IMakeOpenApiHttpClient _httpClientUtil;
    private readonly IConfiguration _configuration;
    private readonly string _baseUrl;
    private readonly string _authHeaderName;
    private readonly string _authHeaderValueTemplate;

    public MakeOpenApiClientUtil(IMakeOpenApiHttpClient httpClientUtil, IConfiguration configuration)
    {
        _httpClientUtil = httpClientUtil;
        _configuration = configuration;
        _baseUrl = configuration["Make:ClientBaseUrl"] ?? "https://us1.make.com/api/v2";
        _authHeaderName = configuration["Make:AuthHeaderName"] ?? "Authorization";
        _authHeaderValueTemplate = configuration["Make:AuthHeaderValueTemplate"] ?? "Bearer {token}";
        _clients = new SingletonDictionary<MakeOpenApiClient>(CreateClient);
    }

    private async ValueTask<MakeOpenApiClient> CreateClient(string connectionKey, CancellationToken token)
    {
        (string apiKey, string baseUrl) = ParseConnectionKey(connectionKey);
        HttpClient httpClient = await _httpClientUtil.Get(apiKey, baseUrl, token).NoSync();
        string authHeaderValue = _authHeaderValueTemplate.Replace("{token}", apiKey, StringComparison.Ordinal);

        var requestAdapter = new HttpClientRequestAdapter(
            new GenericAuthenticationProvider(headerName: _authHeaderName, headerValue: authHeaderValue), httpClient: httpClient)
        {
            BaseUrl = baseUrl
        };

        return new MakeOpenApiClient(requestAdapter);
    }

    public ValueTask<MakeOpenApiClient> Get(CancellationToken cancellationToken = default)
    {
        return Get(_configuration.GetValueStrict<string>("Make:ApiKey"), _baseUrl, cancellationToken);
    }

    public ValueTask<MakeOpenApiClient> Get(string apiKey, CancellationToken cancellationToken = default)
    {
        return Get(apiKey, _baseUrl, cancellationToken);
    }

    public ValueTask<MakeOpenApiClient> Get(string apiKey, string baseUrl, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(apiKey);
        ArgumentException.ThrowIfNullOrWhiteSpace(baseUrl);

        string normalizedBaseUrl = new Uri(baseUrl, UriKind.Absolute).AbsoluteUri.TrimEnd('/');

        return _clients.Get(CreateConnectionKey(apiKey, normalizedBaseUrl), cancellationToken);
    }

    private static string CreateConnectionKey(string apiKey, string baseUrl) => string.Concat(apiKey, "\0", baseUrl);

    private static (string ApiKey, string BaseUrl) ParseConnectionKey(string connectionKey)
    {
        int separatorIndex = connectionKey.IndexOf('\0');

        return (connectionKey[..separatorIndex], connectionKey[(separatorIndex + 1)..]);
    }

    /// <summary>
    /// Releases resources used by the current instance.
    /// </summary>
    public void Dispose()
    {
        _clients.Dispose();
    }

    /// <summary>
    /// Asynchronously releases resources used by the current instance.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public ValueTask DisposeAsync()
    {
        return _clients.DisposeAsync();
    }
}
