using Eventy.Service.Domain.Events.Interfaces;
using Eventy.Service.Domain.Settings;
using Eventy.Service.Domain.User.Interfaces;
using Eventy.Service.Infra.Data.Context;
using Eventy.Service.Infra.Data.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Eventy.Service.Infra.Data.Dependencies.Extensions
{
    public static class InfraDependenciesExtensions
    {
        /// <summary>
        /// Register Data Layer Dependencies, including PostgreSQL and EFCore.
        /// </summary>
        /// <param name="services"></param>

        public static IServiceCollection RegisterDataLayerDependencies(this IServiceCollection services, AppSettings appSettings, IConfiguration configuration)
        {
            return services.RegisterPostgreSQL(appSettings);
        }

        public static IServiceCollection RegisterPostgreSQL(this IServiceCollection services, AppSettings appSettings)
        {
            services.AddDbContext<AppDbContext>( ServiceLifetime.Transient);

            //Repositories
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }



    }
}
