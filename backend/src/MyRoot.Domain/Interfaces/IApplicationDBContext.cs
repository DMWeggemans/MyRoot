using MyRoot.Domain.Entities.Abstracts;
using Microsoft.EntityFrameworkCore;
using MyRoot.Domain.Entities;

namespace MyRoot.Domain.Interfaces
{
    public interface IApplicationDBContext
    {
        DbSet<User> Users { get; }
        DbSet<RefreshToken> RefreshTokens { get; }

        DbSet<Watchable> Watchables { get; }
        DbSet<Movie> Movies { get; }
        DbSet<Show> Shows { get; }
        DbSet<Season> Seasons { get; }
        DbSet<Episode> Episodes { get; }
        DbSet<WatchableProgress> WatchableProgresses { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
