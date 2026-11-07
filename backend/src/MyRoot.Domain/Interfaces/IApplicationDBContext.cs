using Microsoft.EntityFrameworkCore;
using MyRoot.Domain.Entities.Game;
using MyRoot.Domain.Entities.User;

namespace MyRoot.Domain.Interfaces
{
    public interface IApplicationDBContext
    {
        DbSet<User> Users { get; }
        DbSet<RefreshToken> RefreshTokens { get; }
        //DbSet<Account> Accounts { get; }

        //DbSet<GameProvider> GameProviders { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
