using RentalOfPremises.Context.Contracts.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RentalOfPremises.ContextConfiguration
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(x => x.Id);
            builder.PropertyAuditConfiguration();
            builder.Property(x => x.LoginUser).IsRequired();
            builder.Property(x => x.PasswordUser).IsRequired();
            builder.Property(x => x.RoleUser).IsRequired();
            builder.HasIndex(x => x.LoginUser)
                .HasFilter($"{nameof(User.DeletedAt)} is null")
                .HasDatabaseName($"IX_{nameof(User)}_{nameof(User.LoginUser)}");
        }
    }
}
