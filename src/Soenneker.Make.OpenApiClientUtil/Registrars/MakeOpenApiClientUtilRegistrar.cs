using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Make.HttpClients.Registrars;
using Soenneker.Make.OpenApiClientUtil.Abstract;

namespace Soenneker.Make.OpenApiClientUtil.Registrars;

/// <summary>
/// Registers the OpenAPI client utility for dependency injection.
/// </summary>
public static class MakeOpenApiClientUtilRegistrar
{
    /// <summary>
    /// Adds <see cref="MakeOpenApiClientUtil"/> as a singleton service. <para/>
    /// </summary>
    public static IServiceCollection AddMakeOpenApiClientUtilAsSingleton(this IServiceCollection services)
    {
        services.AddMakeOpenApiHttpClientAsSingleton()
                .TryAddSingleton<IMakeOpenApiClientUtil, MakeOpenApiClientUtil>();

        return services;
    }

    /// <summary>
    /// Adds <see cref="MakeOpenApiClientUtil"/> as a scoped service. <para/>
    /// </summary>
    public static IServiceCollection AddMakeOpenApiClientUtilAsScoped(this IServiceCollection services)
    {
        services.AddMakeOpenApiHttpClientAsSingleton()
                .TryAddScoped<IMakeOpenApiClientUtil, MakeOpenApiClientUtil>();

        return services;
    }
}
