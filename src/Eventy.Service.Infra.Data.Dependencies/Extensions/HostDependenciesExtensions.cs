using Eventy.Service.Domain.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Eventy.Service.Infra.Data.Dependencies.Extensions
{
    public static class HostDependenciesExtensions
    {
        public static AppSettings RegisterAppSettings(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = configuration.GetSection("Settings");
            services.Configure<AppSettings>(settings);

            services.AddSingleton(new AppSettings
            {
                PostgreSQLConnectionString = configuration["Settings:PostgreSQLConnectionString"]!,
                DatabaseName = configuration["Settings:DatabaseName"]!,
                ApplicationName = configuration["Settings:ApplicationName"]!,
                JwtSecretKey = configuration["Settings:JwtSecretKey"]!
            });

            return settings.Get<AppSettings>()!;
        }
    }
}
