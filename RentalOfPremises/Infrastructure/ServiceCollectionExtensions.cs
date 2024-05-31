using DinkToPdf.Contracts;
using DinkToPdf;
using RentalOfPremises.Api.Infrastructure;
using RentalOfPremises.Api.Infrastructures.Validator;
using RentalOfPremises.Common;
using RentalOfPremises.Common.Entity.InterfaceDB;
using RentalOfPremises.Context;
using RentalOfPremises.Repositories;
using RentalOfPremises.Services;
using RentalOfPremises.Shared;

namespace RentalOfPremises.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDependences(this IServiceCollection services)
        {
            services.AddScoped<IIdentityProvider, ApiIdentityProvider>();

            services.AddTransient<IDateTimeProvider, DateTimeProvider>();
            services.AddTransient<IDbWriterContext, DbWriterContext>();
            services.AddTransient<IApiValidatorService, ApiValidatorService>();

            services.RegisterAutoMapperProfile<ApiAutoMapperProfile>();

            services.RegisterModule<ServiceModule>();
            services.RegisterModule<ContextModule>();
            services.RegisterModule<RepositoriesModule>();

            services.RegisterAutoMapper();

            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
        }
    }
}
