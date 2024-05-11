using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace RentalOfPremises.Shared
{
    public static class Register
    {
        public static void AssemblyInterfaceAssignableTo<TInterface>(this IServiceCollection services, ServiceLifetime lifetime)
        {
            var serviceType = typeof(TInterface);
            var types = serviceType.Assembly.GetTypes()
                .Where(x => serviceType.IsAssignableFrom(x) && !(x.IsAbstract || x.IsInterface));
            foreach (var type in types)
            {
                services.TryAdd(new ServiceDescriptor(type, type, lifetime));
                var interfaces = type.GetTypeInfo().ImplementedInterfaces
                .Where(i => i != typeof(IDisposable) && i.IsPublic && i != serviceType);
                foreach (var interfaceType in interfaces)
                {
                    services.TryAdd(new ServiceDescriptor(interfaceType,
                        provider => provider.GetRequiredService(type),
                        lifetime));
                }
            }
        }
    }
}
