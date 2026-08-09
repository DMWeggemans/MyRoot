using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyRoot.Domain.Entities.Watchables;
using Microsoft.EntityFrameworkCore;

namespace MyRoot.Infrastructure.Persistence.Configurations
{
    public class WatchableSeasonConfiguration : IEntityTypeConfiguration<WatchableSeason>
    {
        public void Configure(EntityTypeBuilder<WatchableSeason> builder)
        {
            builder.HasOne(ws => ws.Show)
                .WithMany(ws => ws.Seasons)
                .HasForeignKey(ws => ws.ShowId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
