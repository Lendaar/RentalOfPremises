using RentalOfPremises.Context.Contracts.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RentalOfPremises.ContextConfiguration
{
    public class ContractEntityTypeConfiguration : IEntityTypeConfiguration<Contract>
    {
        public void Configure(EntityTypeBuilder<Contract> builder)
        {
            builder.ToTable("Contracts");
            builder.HasIdAsKey();
            builder.PropertyAuditConfiguration();
            builder.Property(x => x.Number).IsRequired();
            builder.Property(x => x.Payment).IsRequired();
            builder.Property(x => x.DateStart).IsRequired();
            builder.Property(x => x.DateEnd).IsRequired();
            builder.Property(x => x.Archive).IsRequired();
            builder.HasIndex(x => x.Number)
                    .HasFilter($"{nameof(Contract.DeletedAt)} is null")
                    .HasDatabaseName($"IX_{nameof(Contract)}_{nameof(Contract.Number)}");
        }
    }
}
