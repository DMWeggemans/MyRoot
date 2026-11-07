using Microsoft.EntityFrameworkCore;
using MyRoot.Domain.Entities.Game;
using MyRoot.Domain.Entities.User;
using MyRoot.Domain.Interfaces;

namespace MyRoot.Infrastructure.Persistence
{
    public class ApplicationDBContext : DbContext, IApplicationDBContext
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
        //public DbSet<Account> Accounts => Set<Account>();

        //public DbSet<GameProvider> GameProviders => Set<GameProvider>();

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDBContext).Assembly);
        }
    }
}
