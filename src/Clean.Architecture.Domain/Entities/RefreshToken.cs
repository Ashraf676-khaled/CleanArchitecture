// Domain/Entities/RefreshToken.cs
using Clean.Architecture.Domain.Common.Abstractions;

namespace Clean.Architecture.Domain.Entities;

public class RefreshToken : Entity
{
  public Guid UserId { get; set; }         // 👈 مجرد Guid، مش Navigation لـ User Entity
  public string Token { get; set; } = string.Empty;
  public DateTimeOffset ExpiresUtc { get; set; }
  public bool IsRevoked { get; set; }

  protected RefreshToken() { }

  public RefreshToken(Guid userId, string token, DateTimeOffset expiresUtc)
  {
    UserId = userId;
    Token = token;
    ExpiresUtc = expiresUtc;
  }

  public bool IsActive => !IsRevoked && ExpiresUtc > DateTimeOffset.UtcNow;
}
