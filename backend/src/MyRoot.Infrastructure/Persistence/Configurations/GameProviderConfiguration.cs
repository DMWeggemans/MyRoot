//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using Microsoft.EntityFrameworkCore;
//using MyRoot.Domain.Entities.Game;

//namespace MyRoot.Infrastructure.Persistence.Configurations
//{
//    public class GameProviderConfiguration : IEntityTypeConfiguration<GameProvider>
//    {
//        public void Configure(EntityTypeBuilder<GameProvider> builder)
//        {
//            builder.Property(gp => gp.Provider)
//                .IsRequired(true);

//            builder.Property(gp => gp.UserName)
//                .IsRequired(true);

//            builder.HasOne(gp => gp.Account)
//                .WithMany(a => a.GameProviders)
//                .HasForeignKey(gp => gp.AccountId)
//                .OnDelete(DeleteBehavior.Cascade);
//        }
//    }
//}
