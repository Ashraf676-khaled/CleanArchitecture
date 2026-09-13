using Clean.Architecture.Application.Common.Interfaces;
using Clean.Architecture.Appliction.Common.Interfaces;
using Clean.Architecture.Domain.Entities;
using Clean.Architecture.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Clean.Architecture.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options)
    : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>(options), IApplicationDbContext
{
  public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();


  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    base.OnModelCreating(modelBuilder);
  }
}
