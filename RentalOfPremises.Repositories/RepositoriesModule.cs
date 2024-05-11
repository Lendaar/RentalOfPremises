using RentalOfPremises.Common;
using RentalOfPremises.Shared;
using Microsoft.Extensions.DependencyInjection;

namespace RentalOfPremises.Repositories
{
    public class RepositoriesModule : Module
    {
        public override void CreateModule(IServiceCollection services)
        {
            services.AssemblyInterfaceAssignableTo<IRepositoriesAnchor>(ServiceLifetime.Scoped);
        }
    }
}
