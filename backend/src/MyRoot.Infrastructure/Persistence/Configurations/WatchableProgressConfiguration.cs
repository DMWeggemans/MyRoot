using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MyRoot.Domain.Entities;

namespace MyRoot.Infrastructure.Persistence.Configurations
{
    public class WatchableProgressConfiguration : IEntityTypeConfiguration<WatchableProgress>
    {
        public void Configure(EntityTypeBuilder<WatchableProgress> builder)
        {
            builder.HasIndex(wp => new { wp.UserId, wp.WatchableId, wp.EpisodeId })
                .IsUnique();

            builder.HasOne(wp => wp.Watchable)
                .WithMany(w => w.WatchableProgresses)
                .HasForeignKey(wp => wp.WatchableId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(wp => wp.Episode)
                .WithMany(e => e.WatchableProgresses)
                .HasForeignKey(wp => wp.EpisodeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
