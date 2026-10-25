using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MyRoot.Domain.Entities;

namespace MyRoot.Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.UserName)
                .IsRequired();

            builder.Property(u => u.NormalizedUserName)
                .IsRequired();

            builder.Property(u => u.PasswordHash)
                .IsRequired();

            builder.HasIndex(u => u.NormalizedUserName)
                .IsUnique();

            builder.HasIndex(u => u.NormalizedEmail)
                .IsUnique();
        }
    }
}
