using Clean.Architecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Clean.Architecture.Appliction.Common.Interfaces;

public interface IApplicationDbContext
{
  DbSet<RefreshToken> RefreshTokens { get; }
  Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

}
