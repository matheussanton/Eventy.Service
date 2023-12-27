using Eventy.Service.Domain.Events.Commands.Handlers;
using Eventy.Service.Domain.Events.Queries;
using Eventy.Service.Domain.Hash;
using Eventy.Service.Domain.Hash.Interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Eventy.Service.Infra.Data.Dependencies.Extensions
{
    public static class DomainDependenciesExtensions
    {
        public static void RegisterDomainDependencies(this IServiceCollection services)
        {
            services.AddSingleton<IPasswordHasher, PasswordHasher>();

            services.AddScoped<EventsHandler>();
            services.AddScoped<EventsQueryHandler>();

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
        }
    }
}
