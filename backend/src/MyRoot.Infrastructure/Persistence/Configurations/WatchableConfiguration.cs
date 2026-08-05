using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyRoot.Domain.Entities.Abstracts;
using Microsoft.EntityFrameworkCore;
using MyRoot.Domain.Entities;
using MyRoot.Domain.Enums;

namespace MyRoot.Infrastructure.Persistence.Configurations
{
    public class WatchableConfiguration : IEntityTypeConfiguration<Watchable>
    {
        public void Configure(EntityTypeBuilder<Watchable> builder)
        {
            builder.HasIndex(w => w.Title);

            builder.Property(w => w.Title)
                .IsRequired();

            builder.HasDiscriminator(w => w.Type)
                .HasValue<Movie>(WatchableType.Movie)
                .HasValue<Show>(WatchableType.Show);
        }
    }
}
