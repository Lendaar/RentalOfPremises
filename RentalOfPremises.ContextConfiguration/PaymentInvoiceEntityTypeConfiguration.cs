using RentalOfPremises.Context.Contracts.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RentalOfPremises.ContextConfiguration
{
    public class PaymentInvoiceEntityTypeConfiguration : IEntityTypeConfiguration<PaymentInvoice>
    {
        public void Configure(EntityTypeBuilder<PaymentInvoice> builder)
        {
            builder.ToTable("PaymentInvoices");
            builder.HasIdAsKey();
            builder.PropertyAuditConfiguration();
            builder.Property(x => x.NumberContract).IsRequired();
            builder.Property(x => x.PeriodPayment).IsRequired();
            builder.Property(x => x.Electricity).IsRequired();
            builder.Property(x => x.WaterPl).IsRequired();
            builder.Property(x => x.WaterMi).IsRequired();
            builder.Property(x => x.PassPerson).IsRequired();
            builder.Property(x => x.PassLegСar).IsRequired();
            builder.Property(x => x.PassGrСar).IsRequired();
            builder.HasIndex(x => x.NumberContract)
                .HasFilter($"{nameof(PaymentInvoice.DeletedAt)} is null")
                .HasDatabaseName($"IX_{nameof(PaymentInvoice)}_{nameof(PaymentInvoice.NumberContract)}");
        }
    }
}
