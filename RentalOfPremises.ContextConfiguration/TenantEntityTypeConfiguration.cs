using RentalOfPremises.Context.Contracts.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RentalOfPremises.ContextConfiguration
{
    public class TenantEntityTypeConfiguration : IEntityTypeConfiguration<Tenant>
    {
        public void Configure(EntityTypeBuilder<Tenant> builder)
        {
            builder.ToTable("Tenants");
            builder.HasIdAsKey();
            builder.PropertyAuditConfiguration();
            builder.Property(x => x.Title).IsRequired();
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Surname).IsRequired();
            builder.Property(x => x.Type).IsRequired();
            builder.Property(x => x.Inn).IsRequired();
            builder.Property(x => x.Address).IsRequired();
            builder.Property(x => x.Rs).IsRequired();
            builder.Property(x => x.Ks).IsRequired();
            builder.Property(x => x.Bik).IsRequired();
            builder.Property(x => x.Bank).IsRequired();
            builder.Property(x => x.Okpo).IsRequired();
            builder.Property(x => x.Ogrn).IsRequired();
            builder.Property(x => x.Telephone).IsRequired();
            builder.HasIndex(x => x.Inn)
                .HasFilter($"{nameof(Tenant.DeletedAt)} is null")
                .HasDatabaseName($"IX_{nameof(Tenant)}_{nameof(Tenant.Inn)}");
            builder.HasMany(x => x.Contract)
                .WithOne(x => x.Tenant)
                .HasForeignKey(x => x.TenantId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
