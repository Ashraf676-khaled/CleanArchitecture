using Clean.Architecture.Api;
using Clean.Architecture.Api.Configurations;
using Clean.Architecture.Api.Extensions;
using Clean.Architecture.Api.Middlewares;
using Clean.Architecture.Application;
using Clean.Architecture.Infrastructure;
using Scalar.AspNetCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Serilog لازم يتسجل الأول قبل أي حاجة، عشان يلقط أي Exception حتى وقت الـ Startup
builder.AddLoggerConfigs();
builder.Services.AddOpenApi();
try
{
  Log.Information("Starting up Clean.Architecture.Api");

  // Clean Architecture layers
  builder.Services.AddApplication();
  builder.Services.AddInfrastructure(builder.Configuration);

  // Api layer (كل حاجة مجمعة دلوقتي في DependencyInjection.cs الجديد)
  builder.Services.AddApiServices(builder.Configuration);

  var app = builder.Build();


  app.UseExceptionHandler();
  app.MapOpenApi();
  app.MapScalarApiReference();
  app.UseSerilogRequestLogging();
  app.UseRequestLogContext();


  app.UseHttpsRedirection();
  app.UseAuthentication();
  app.UseAuthorization();

  app.MapEndpoints();

  app.Run();
}
catch (Exception ex)
{
  Log.Fatal(ex, "Clean.Architecture.Api terminated unexpectedly");
}
finally
{
  Log.CloseAndFlush();
}
