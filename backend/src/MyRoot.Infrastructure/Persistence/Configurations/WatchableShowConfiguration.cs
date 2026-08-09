using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyRoot.Domain.Entities.Watchables;
using Microsoft.EntityFrameworkCore;

namespace MyRoot.Infrastructure.Persistence.Configurations
{
    public class WatchableShowConfiguration : IEntityTypeConfiguration<WatchableShow>
    {
        public void Configure(EntityTypeBuilder<WatchableShow> builder)
        {
            builder.HasOne(ws => ws.Collection)
                .WithMany("Watchables")
                .HasForeignKey(ws => ws.CollectionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .IsRequired(false);
        }
    }
}
