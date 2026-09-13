using System.Text;
using Clean.Architecture.Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Clean.Architecture.Api.Configurations;

public static class AuthenticationConfig
{
  public static IServiceCollection AddAuthenticationConfig(
      this IServiceCollection services,
      IConfiguration configuration)
  {
    var jwtSettings = configuration.GetSection("Jwt").Get<Jwt>()!;

    services.AddAuthentication(options =>
    {
      options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
      options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
      options.TokenValidationParameters = new TokenValidationParameters
      {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(
                  Encoding.UTF8.GetBytes(jwtSettings.Secret))
      };
    });

    services.AddAuthorization();

    return services;
  }
}
