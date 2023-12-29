using System.Text;
using Eventy.Service.Domain.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

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

        public static IServiceCollection RegisterAuthentication(this IServiceCollection services, string secret)
		{
			var key = Encoding.ASCII.GetBytes(secret);

			return services.AddAuthentication(x =>
			{
				x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(x =>
			{
				x.RequireHttpsMetadata = false;
				x.SaveToken = true;
				x.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(key),
					ValidateIssuer = false,
					ValidateAudience = false
				};
			}).Services;
		}
    }
}
