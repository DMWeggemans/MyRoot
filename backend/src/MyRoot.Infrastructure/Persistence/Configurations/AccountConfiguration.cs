//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using Microsoft.EntityFrameworkCore;
//using MyRoot.Domain.Entities.User;

//namespace MyRoot.Infrastructure.Persistence.Configurations
//{
//    public class AccountConfiguration : IEntityTypeConfiguration<Account>
//    {
//        public void Configure(EntityTypeBuilder<Account> builder)
//        {
//            builder.HasOne(a => a.User)
//                .WithMany(u => u.Accounts)
//                .HasForeignKey(a => a.UserId)
//                .OnDelete(DeleteBehavior.Cascade);
//        }
//    }
//}
