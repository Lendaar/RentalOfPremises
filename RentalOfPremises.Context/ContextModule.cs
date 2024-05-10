using RentalOfPremises.Common;
using RentalOfPremises.Common.Entity.InterfaceDB;
using RentalOfPremises.Context.Contracts.Interface;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace RentalOfPremises.Context
{
    public class ContextModule : Module
    {
        public override void CreateModule(IServiceCollection services)
        {
            services.AddScoped<IRentalOfPremisesContext, RentalOfPremisesContext>();
            services.TryAddScoped<IRentalOfPremisesContext>(provider => provider.GetRequiredService<RentalOfPremisesContext>());
            services.TryAddScoped<IDbRead>(provider => provider.GetRequiredService<RentalOfPremisesContext>());
            services.TryAddScoped<IDbWriter>(provider => provider.GetRequiredService<RentalOfPremisesContext>());
            services.TryAddScoped<IUnitOfWork>(provider => provider.GetRequiredService<RentalOfPremisesContext>());
        }
    }
}
