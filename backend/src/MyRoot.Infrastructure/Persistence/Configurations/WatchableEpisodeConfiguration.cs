using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyRoot.Domain.Entities.Watchables;
using Microsoft.EntityFrameworkCore;

namespace MyRoot.Infrastructure.Persistence.Configurations
{
    public class WatchableEpisodeConfiguration : IEntityTypeConfiguration<WatchableEpisode>
    {
        public void Configure(EntityTypeBuilder<WatchableEpisode> builder)
        {
            builder.HasOne(we => we.Season)
                .WithMany(ws => ws.Episodes)
                .HasForeignKey(we => we.SeasonId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
