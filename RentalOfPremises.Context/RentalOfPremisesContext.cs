using RentalOfPremises.Common.Entity.InterfaceDB;
using RentalOfPremises.Context.Contracts.Interface;
using RentalOfPremises.Context.Contracts.Models;
using RentalOfPremises.ContextConfiguration;
using Microsoft.EntityFrameworkCore;

namespace RentalOfPremises.Context
{
    public class RentalOfPremisesContext : DbContext,
        IRentalOfPremisesContext,
        IDbRead,
        IDbWriter,
        IUnitOfWork
    {
        /// <summary>
        /// Контекст работы с БД
        /// </summary>
        /// <remarks>
        /// 1) dotnet tool install --global dotnet-ef
        /// 2) dotnet tool update --global dotnet-ef
        /// 3) dotnet ef migrations add [name] --project TimeTable203.Context\TimeTable203.Context.csproj
        /// 4) dotnet ef database update --project TimeTable203.Context\TimeTable203.Context.csproj
        /// 5) dotnet ef database update [targetMigrationName] --TimeTable203.Context\TimeTable203.Context.csproj
        /// </remarks>
        public RentalOfPremisesContext(DbContextOptions<RentalOfPremisesContext> options) : base(options) { }

        public DbSet<Contract> Contracts { get; set; }

        public DbSet<PaymentInvoice> PaymentInvoices { get; set; }

        public DbSet<Price> Prices { get; set; }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<Tenant> Tenants { get; set; }

        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(IContextConfigurationAnchor).Assembly);
        }

        IQueryable<TEntity> IDbRead.Read<TEntity>()
            => base.Set<TEntity>()
                .AsNoTracking()
                .AsQueryable();

        void IDbWriter.Add<TEntities>(TEntities entity)
            => base.Entry(entity).State = EntityState.Added;

        void IDbWriter.Update<TEntities>(TEntities entity)
              => base.Entry(entity).State = EntityState.Modified;

        void IDbWriter.Delete<TEntities>(TEntities entity)
              => base.Entry(entity).State = EntityState.Deleted;


        async Task<int> IUnitOfWork.SaveChangesAsync(CancellationToken cancellationToken)
        {
            var count = await base.SaveChangesAsync(cancellationToken);
            foreach (var entry in base.ChangeTracker.Entries().ToArray())
            {
                entry.State = EntityState.Detached;
            }
            return count;
        }
    }
}
