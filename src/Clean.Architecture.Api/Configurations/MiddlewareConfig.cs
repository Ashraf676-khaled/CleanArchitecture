using Clean.Architecture.Api.Middlewares;
using Microsoft.Extensions.DependencyInjection;

namespace Clean.Architecture.Api.Configurations;

public static class MiddlewareConfig
{
  public static IServiceCollection AddMiddlewareConfig(this IServiceCollection services)
  {
    return services;
  }
}
