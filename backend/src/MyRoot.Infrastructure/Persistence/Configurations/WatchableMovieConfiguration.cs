using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyRoot.Domain.Entities.Watchables;
using Microsoft.EntityFrameworkCore;

namespace MyRoot.Infrastructure.Persistence.Configurations
{
    public class WatchableMovieConfiguration : IEntityTypeConfiguration<WatchableMovie>
    {
        public void Configure(EntityTypeBuilder<WatchableMovie> builder)
        {
            builder.HasOne(wm => wm.Collection)
                .WithMany("Watchables")
                .HasForeignKey(wm => wm.CollectionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .IsRequired(false);
        }
    }
}
