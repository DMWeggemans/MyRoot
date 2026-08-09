using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyRoot.Domain.Entities.Watchables;
using MyRoot.Domain.Entities.Abstracts;
using Microsoft.EntityFrameworkCore;
using MyRoot.Domain.Enums;

namespace MyRoot.Infrastructure.Persistence.Configurations
{
    public class WatchableConfiguration : IEntityTypeConfiguration<Watchable>
    {
        public void Configure(EntityTypeBuilder<Watchable> builder)
        {
            builder.Property(w => w.Title)
                .IsRequired();

            builder.HasDiscriminator(w => w.Type)
                .HasValue<WatchableMovie>(WatchableType.Movie)
                .HasValue<WatchableShow>(WatchableType.Show)
                .HasValue<WatchableSeason>(WatchableType.Season)
                .HasValue<WatchableEpisode>(WatchableType.Episode);
        }
    }
}
