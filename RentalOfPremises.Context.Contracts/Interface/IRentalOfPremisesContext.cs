using Microsoft.EntityFrameworkCore;
using RentalOfPremises.Context.Contracts.Models;

namespace RentalOfPremises.Context.Contracts.Interface
{
    public interface IRentalOfPremisesContext
    {
        /// <summary> Список <inheritdoc cref="Contract"/> </summary>
        DbSet<Contract> Contracts { get; }

        /// <summary> Список <inheritdoc cref="PaymentInvoice"/> </summary>
        DbSet<PaymentInvoice> PaymentInvoices { get; }

        /// <summary> Список <inheritdoc cref="Price"/> </summary>
        DbSet<Price> Prices { get; }

        /// <summary> Список <inheritdoc cref="Room"/> </summary>
        DbSet<Room> Rooms { get; }

        /// <summary> Список <inheritdoc cref="Tenant"/> </summary>
        DbSet<Tenant> Tenants { get; }

        /// <summary> Список <inheritdoc cref="User"/> </summary>
        DbSet<User> Users { get; }
    }
}
