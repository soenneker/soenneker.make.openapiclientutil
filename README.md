[![](https://img.shields.io/nuget/v/soenneker.make.openapiclientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.make.openapiclientutil/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.make.openapiclientutil/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.make.openapiclientutil/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.make.openapiclientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.make.openapiclientutil/)

# Soenneker.Make.OpenApiClientUtil

Exposes a cached OpenAPI client instance.

## Install

```bash
dotnet add package Soenneker.Make.OpenApiClientUtil
```

## Quick start

```csharp
using Soenneker.Make.OpenApiClientUtil.Registrars;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
var result = services.AddMakeOpenApiClientUtilAsSingleton();
```

Adds `MakeOpenApiClientUtil` as a singleton service.

## What you get

- `IMakeOpenApiClientUtil` — Exposes a cached OpenAPI client instance.
- `MakeOpenApiClientUtilRegistrar` — Registers the OpenAPI client utility for dependency injection.

## API at a glance

| API | What it does | Result / important behavior |
| --- | --- | --- |
| `IMakeOpenApiClientUtil.Get(apiKey, cancellationToken)` | Gets a client for a specific API key using the configured base URL. | A task whose result is the requested make Open API Client. |
| `IMakeOpenApiClientUtil.Get(apiKey, baseUrl, cancellationToken)` | Gets a client for a specific Make connection. | A task whose result is the requested make Open API Client. |
| `MakeOpenApiClientUtilRegistrar.AddMakeOpenApiClientUtilAsSingleton(services)` | Adds `MakeOpenApiClientUtil` as a singleton service. | The same service collection, so additional registrations can be chained. |
| `MakeOpenApiClientUtilRegistrar.AddMakeOpenApiClientUtilAsScoped(services)` | Adds `MakeOpenApiClientUtil` as a scoped service. | The same service collection, so additional registrations can be chained. |

## Practical notes

- Cancellation stops pending work; it does not undo work that has already completed.
- Reuse the registered client instead of constructing one per operation.
- Dispose instances you own when their scope ends so held resources can be released.
