using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MyRoot.Domain.Entities;

namespace MyRoot.Infrastructure.Persistence.Configurations
{
    public class SeasonConfiguration : IEntityTypeConfiguration<Season>
    {
        public void Configure(EntityTypeBuilder<Season> builder)
        {
            builder.HasIndex(s => new { s.ShowId, s.SeasonNumber })
                .IsUnique();

            builder.HasOne(s => s.Show)
                .WithMany(s => s.Seasons)
                .HasForeignKey(s => s.ShowId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
