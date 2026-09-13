using Clean.Architecture.Application.Common.Interfaces;
using Clean.Architecture.Appliction.Common.Interfaces;
using Clean.Architecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace Clean.Architecture.Infrastructure.Identity;

public class RefreshTokenService : IRefreshTokenService
{
  private readonly IApplicationDbContext _context;

  public RefreshTokenService(IApplicationDbContext context) => _context = context;

  public async Task SaveRefreshTokenAsync(Guid userId, string token, CancellationToken ct = default)
  {
    var refreshToken = new RefreshToken(userId, token, DateTimeOffset.UtcNow.AddDays(7));
    _context.RefreshTokens.Add(refreshToken);
    await _context.SaveChangesAsync(ct);
  }

  public async Task<bool> ValidateRefreshTokenAsync(Guid userId, string token, CancellationToken ct = default) =>
      await _context.RefreshTokens.AnyAsync(
          r => r.UserId == userId && r.Token == token && r.IsActive, ct);

  public async Task RevokeRefreshTokenAsync(Guid userId, string token, CancellationToken ct = default)
  {
    var entity = await _context.RefreshTokens
        .FirstOrDefaultAsync(r => r.UserId == userId && r.Token == token, ct);

    if (entity is not null)
    {
      entity.Revoke(); // استدعاء دالة الـ Domain مباشرة من الكيان
      await _context.SaveChangesAsync(ct);
    }
  }
}
