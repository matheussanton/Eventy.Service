using Eventy.Service.Infra.Data.Dependencies.Extensions;

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
            services.AddSwaggerGen();

            var appSettings = services.RegisterAppSettings(Configuration);

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

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();
        }


    }
}
