using RentalOfPremises.Context.Contracts.Models;

namespace RentalOfPremises.Repositories.Contracts
{
    /// <summary>
    /// Репозиторий записи <see cref="Tenant"/>
    /// </summary>
    public interface ITenantWriteRepository : IRepositoryWriter<Tenant>
    {
    }
}
