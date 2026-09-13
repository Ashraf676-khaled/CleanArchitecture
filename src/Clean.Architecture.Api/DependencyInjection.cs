using Clean.Architecture.Api.Configurations;
using Clean.Architecture.Api.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Clean.Architecture.Api;

public static class DependencyInjection
{
  public static IServiceCollection AddApiServices(
      this IServiceCollection services,
      IConfiguration configuration)
  {
    services.AddOptionConfigs(configuration);
    services.AddServiceConfigs();
    services.AddAuthenticationConfig(configuration);
    services.AddMediatorConfig();

    services.AddExceptionHandler<GlobalExceptionHandler>();
    services.AddProblemDetails();

    return services;
  }
}
