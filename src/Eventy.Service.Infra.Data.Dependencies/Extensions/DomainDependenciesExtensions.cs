using Eventy.Service.Domain.Emailing.Interfaces;
using Eventy.Service.Domain.Emailing.Services;
using Eventy.Service.Domain.Events.Commands.Handlers;
using Eventy.Service.Domain.Events.Queries;
using Eventy.Service.Domain.Responses;
using Eventy.Service.Domain.User.Models;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Eventy.Service.Infra.Data.Dependencies.Extensions
{
    public static class DomainDependenciesExtensions
    {
        public static void RegisterDomainDependencies(this IServiceCollection services)
        {
            RegisterHandlers(services);
            RegisterResponses(services);

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
        }

        private static void RegisterHandlers(this IServiceCollection services)
        {
            services.AddScoped<EventsHandler>();
            services.AddScoped<EventsQueryHandler>();
            services.AddScoped<IEmailingService, EmailingService>();
        }

        private static void RegisterResponses(this IServiceCollection services)
        {
            services.AddScoped<Response>();
            services.AddScoped(typeof(Response<>));
            
            services.AddScoped<Response<UserDTO>>();
        }
    }
}
