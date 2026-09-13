using System.Reflection;
using Clean.Architecture.Application.Common.Behaviours;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Clean.Architecture.Application;

public static class DependencyInjection
{
  public static IServiceCollection AddApplication(this IServiceCollection services)
  {
    var assembly = Assembly.GetExecutingAssembly();

    services.AddAutoMapper(cfg => { }, assembly);

    services.AddValidatorsFromAssembly(assembly);

    services.AddMediatR(cfg =>
    {
      cfg.RegisterServicesFromAssembly(assembly);
      cfg.AddOpenBehavior(typeof(UnhandledExceptionBehaviour<,>));
      cfg.AddOpenBehavior(typeof(ValidationBehaviour<,>));
      cfg.AddOpenBehavior(typeof(PerformanceBehaviour<,>));
    });

    return services;
  }
}
