using MyRoot.Domain.Entities.Abstracts;
using Microsoft.EntityFrameworkCore;
using MyRoot.Domain.Interfaces;
using MyRoot.Domain.Entities;

namespace MyRoot.Infrastructure.Persistence
{
    public class ApplicationDBContext : DbContext, IApplicationDBContext
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

        public DbSet<Watchable> Watchables => Set<Watchable>();
        public DbSet<Movie> Movies => Set<Movie>();
        public DbSet<Show> Shows => Set<Show>();
        public DbSet<Season> Seasons => Set<Season>();
        public DbSet<Episode> Episodes => Set<Episode>();
        public DbSet<WatchableProgress> WatchableProgresses => Set<WatchableProgress>();

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDBContext).Assembly);
        }
    }
}
