using Microsoft.Extensions.DependencyInjection;

namespace Clean.Architecture.Api.Configurations;

public static class ServiceConfigs
{
  public static IServiceCollection AddServiceConfigs(this IServiceCollection services)
  {
    services.AddEndpointsApiExplorer();
    services.AddHttpContextAccessor();

    return services;
  }
}
