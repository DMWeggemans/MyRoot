using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyRoot.Domain.Entities.Watchables;
using Microsoft.EntityFrameworkCore;

namespace MyRoot.Infrastructure.Persistence.Configurations
{
    public class WatchableCollectionConfiguration : IEntityTypeConfiguration<WatchableCollection>
    {
        public void Configure(EntityTypeBuilder<WatchableCollection> builder)
        {
            builder.HasOne(wc => wc.Collection)
                .WithMany("Watchables")
                .HasForeignKey(wc => wc.CollectionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .IsRequired(false);
        }
    }
}
