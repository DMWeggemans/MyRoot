using Microsoft.EntityFrameworkCore;
using MyRoot.Domain.Entities.Abstracts;
using MyRoot.Domain.Entities.Auth;

namespace MyRoot.Domain.Interfaces
{
    public interface IApplicationDBContext
    {
        DbSet<User> Users { get; }
        DbSet<RefreshToken> RefreshTokens { get; }

        DbSet<Watchable> Watchables { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
