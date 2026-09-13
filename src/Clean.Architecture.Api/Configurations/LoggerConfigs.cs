using Serilog;
using Serilog.Events;

namespace Clean.Architecture.Api.Configurations;

public static class LoggerConfigs
{
  public static void AddLoggerConfigs(this WebApplicationBuilder builder)
  {
    builder.Host.UseSerilog((context, services, configuration) =>
    {
      configuration
          .ReadFrom.Configuration(context.Configuration)
          .ReadFrom.Services(services)
          .Enrich.FromLogContext()
          .Enrich.WithMachineName()
          .Enrich.WithThreadId();
    });
  }
}
