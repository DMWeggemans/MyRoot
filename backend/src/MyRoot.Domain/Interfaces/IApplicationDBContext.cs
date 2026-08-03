using Microsoft.EntityFrameworkCore;
using MyRoot.Domain.Entities;

namespace MyRoot.Domain.Interfaces
{
    public interface IApplicationDBContext
    {
        DbSet<User> Users { get; }
        DbSet<RefreshToken> RefreshTokens { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
