using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MyRoot.Domain.Entities;

namespace MyRoot.Infrastructure.Persistence.Configurations
{
    public class EpisodeConfiguration : IEntityTypeConfiguration<Episode>
    {
        public void Configure(EntityTypeBuilder<Episode> builder)
        {
            builder.HasIndex(e => new { e.SeasonId, e.EpisodeNumber })
                .IsUnique();

            builder.Property(e => e.Title)
                .IsRequired();

            builder.HasOne(e => e.Season)
                .WithMany(s => s.Episodes)
                .HasForeignKey(e => e.SeasonId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
