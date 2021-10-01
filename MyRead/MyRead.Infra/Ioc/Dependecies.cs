using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyRead.Application.Abstractions;
using MyRead.Infra;
using MyRead.Infra.Entity;
using MyRead.Infra.Exceptions;
using System.Diagnostics;
using System.Linq;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class Dependecies
    {
        public static IServiceCollection InjectUow(this IServiceCollection services) =>
            services.AddScoped<IUnitOfWork, UnitOfWork>();

        public static IServiceCollection InjectNotifier(this IServiceCollection services) =>
            services.AddScoped<INotifier, Notification>();

        public static IServiceCollection InjectServices(this IServiceCollection services)
        {
            var serviceType = typeof(IService);
            var appServices = serviceType.Assembly.DefinedTypes.Where(w => !w.IsInterface && w.IsAssignableTo(serviceType));

            foreach (var appService in appServices)
                foreach (var appServiceInterface in appService.GetInterfaces())
                    if (appServiceInterface != serviceType)
                        services.AddScoped(appServiceInterface, appService);

            return services;
        }

        public static IServiceCollection InjectEF(this IServiceCollection services, IConfiguration config)
        {
            var dbProvider = config["Database:Provider"];
            if (dbProvider is null)
                throw new DbNotConfiguredException();

            switch (dbProvider)
            {
                case "InMemory":
                    services.AddDbContext<MyReadContext>(options =>
                        options.UseInMemoryDatabase("memDb")
                               .EnableDetailedErrors()
                               .LogTo(log => Debug.WriteLine(log)));
                    break;

                default:
                    throw new DbNotConfiguredException();
            }

            return services;
        }
    }
}