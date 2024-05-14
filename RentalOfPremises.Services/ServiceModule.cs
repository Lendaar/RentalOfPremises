using RentalOfPremises.Common;
using RentalOfPremises.Services.Anchors;
using RentalOfPremises.Services.Automappers;
using RentalOfPremises.Shared;
using Microsoft.Extensions.DependencyInjection;

namespace RentalOfPremises.Services
{
    public class ServiceModule : Module
    {
        public override void CreateModule(IServiceCollection service)
        {
            service.AssemblyInterfaceAssignableTo<IServiceAnchor>(ServiceLifetime.Scoped);
            service.RegisterAutoMapperProfile<ServiceProfile>();
        }
    }
}
