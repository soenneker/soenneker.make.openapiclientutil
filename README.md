[![](https://img.shields.io/nuget/v/soenneker.make.openapiclientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.make.openapiclientutil/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.make.openapiclientutil/build-and-test.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.make.openapiclientutil/actions/workflows/build-and-test.yml)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.make.openapiclientutil/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.make.openapiclientutil/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.make.openapiclientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.make.openapiclientutil/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.make.openapiclientutil/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.make.openapiclientutil/actions/workflows/codeql.yml)

# Soenneker.Make.OpenApiClientUtil

Creates and caches authenticated `MakeOpenApiClient` instances for one or more Make connections.

## Install

```bash
dotnet add package Soenneker.Make.OpenApiClientUtil
```

## Configuration

```json
{
  "Make": {
    "ApiKey": "your-api-key",
    "ClientBaseUrl": "https://us1.make.com/api/v2"
  }
}
```

`ClientBaseUrl` is optional. Keep `ApiKey` in a secret provider rather than committed configuration.

## Usage

```csharp
using Microsoft.Extensions.DependencyInjection;
using Soenneker.Make.OpenApiClient;
using Soenneker.Make.OpenApiClientUtil.Abstract;
using Soenneker.Make.OpenApiClientUtil.Registrars;

services.AddMakeOpenApiClientUtilAsScoped();

IMakeOpenApiClientUtil clients =
    serviceProvider.GetRequiredService<IMakeOpenApiClientUtil>();

MakeOpenApiClient make = await clients.Get(cancellationToken);
var currentUser = await make.Users.Me.GetAsync(cancellationToken: cancellationToken);
```

Use `Get(apiKey)` for per-call credentials or `Get(apiKey, baseUrl)` for another Make region. Equivalent credentials and normalized base URLs reuse the same generated client within the util's lifetime.

## Client reuse

- Scoped registration intentionally uses a singleton HTTP transport beneath the scoped util. Disposing a scope releases its generated-client cache while allowing the shared transport to remain available.
- Singleton registration shares both generated clients and transport application-wide.
- The configured authentication header defaults to `Authorization: Bearer {token}`. `Make:AuthHeaderName` and `Make:AuthHeaderValueTemplate` support compatible gateways.
- Do not dispose the returned generated client or its underlying `HttpClient`; DI owns their lifetimes.
