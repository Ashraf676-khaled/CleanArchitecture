using Clean.Architecture.Application.Common.Interfaces;
using Clean.Architecture.Appliction.Common.Interfaces;
using Clean.Architecture.Infrastructure.Data.Interceptors;
using Clean.Architecture.Infrastructure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Clean.Architecture.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
namespace Clean.Architecture.Infrastructure;   

public static class DependencyInjection
{
  public static IServiceCollection AddInfrastructure(
      this IServiceCollection services,
      IConfiguration configuration)
  {
    services.AddHttpContextAccessor();

    services.Configure<Jwt>(configuration.GetSection("Jwt"));

    services.AddScoped<ICurrentUserService, CurrentUserService>();
    services.AddScoped<ITokenProvider, TokenProvider>();
    services.AddScoped<IRefreshTokenService, RefreshTokenService>();

    services.AddScoped<AuditableEntityInterceptor>();
    services.AddScoped<DispatchDomainEventsInterceptor>();

    services.AddDbContext<AppDbContext>((sp, options) =>
    {
      options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")); options.AddInterceptors(
          sp.GetRequiredService<AuditableEntityInterceptor>(),
          sp.GetRequiredService<DispatchDomainEventsInterceptor>());
    });

    services.AddScoped<IApplicationDbContext>(sp =>
        sp.GetRequiredService<AppDbContext>());
    return services;
  }
}
