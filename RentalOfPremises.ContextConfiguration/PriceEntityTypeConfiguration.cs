using RentalOfPremises.Context.Contracts.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RentalOfPremises.ContextConfiguration
{
    public class PriceEntityTypeConfiguration : IEntityTypeConfiguration<Price>
    {
        public void Configure(EntityTypeBuilder<Price> builder)
        {
            builder.ToTable("Prices");
            builder.HasIdAsKey();
            builder.PropertyAuditConfiguration();
            builder.Property(x => x.Electricity).IsRequired();
            builder.Property(x => x.WaterPl).IsRequired();
            builder.Property(x => x.WaterMi).IsRequired();
            builder.Property(x => x.PassPerson).IsRequired();
            builder.Property(x => x.PassLegСar).IsRequired();
            builder.Property(x => x.PassGrСar).IsRequired();
            builder.HasMany(x => x.PaymentInvoice)
                .WithOne(x => x.Price)
                .HasForeignKey(x => x.PriceId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
