using Eventy.Service.Infra.Data.Context;
using Eventy.Service.Infra.Data.Dependencies;
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
            services.AddDbContext<AppDbContext>();

            // //Repositories
            // services.AddScoped<ICouponsRepository, CouponsRepository>();

            return services;
        }



    }
}
