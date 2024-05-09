using RentalOfPremises.Context.Contracts.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RentalOfPremises.ContextConfiguration
{
    public class RoomEntityTypeConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.ToTable("Rooms");
            builder.HasIdAsKey();
            builder.PropertyAuditConfiguration();
            builder.Property(x => x.Liter).IsRequired();
            builder.Property(x => x.NumberRoom).IsRequired();
            builder.Property(x => x.SquareRoom).IsRequired();
            builder.Property(x => x.Occupied).IsRequired();
            builder.Property(x => x.TypeRoom).IsRequired();
            builder.HasIndex(x => x.Liter)
                .HasFilter($"{nameof(Room.DeletedAt)} is null")
                .HasDatabaseName($"IX_{nameof(Room)}_{nameof(Room.Liter)}");
            builder.HasMany(x => x.Contract)
                .WithOne(x => x.Room)
                .HasForeignKey(x => x.RoomId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
