using Eventy.Service.Infra.Data.Dependencies.Extensions;
using Microsoft.OpenApi.Models;

namespace Eventy.Service.Host
{
    public class Startup(IConfiguration configuration, IHostEnvironment env)
    {

        public IConfiguration Configuration { get; } = configuration;
        public IHostEnvironment CurrentEnvironment { get; } = env;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();

            var appSettings = services.RegisterAppSettings(Configuration);

            services.RegisterAuthentication(appSettings.JwtSecretKey);  
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Eventy Service", Description = "Service for Eventy - Event manager.", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert a JWT with Bearer",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            });

            services.RegisterDataLayerDependencies(appSettings, Configuration);
            services.RegisterDomainDependencies();

            services.AddLogging();
        }

        public void Configure(WebApplication app, IHostEnvironment env)
        {
            if (CurrentEnvironment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
        }


    }
}
