using Eventy.Service.Domain;
using Eventy.Service.Domain.Entities;
using Eventy.Service.Domain.Enums;
using Eventy.Service.Domain.User.Enums;
using Microsoft.EntityFrameworkCore;

namespace Eventy.Service.Infra.Data.Context.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void ModelUser(this ModelBuilder modelBuilder){
            modelBuilder.Entity<UserEntityDomain>()
                        .HasKey(x => x.Id);
        }

        public static void SeedDefaultUser(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntityDomain>().HasData(
                new UserEntityDomain(
                    name: "Administrator",
                    email: "admin@eventy.com",
                    password: "Pwd@123",
                    role: EUserRole.ADMINISTRATOR,
                    createdAt: DateTime.UtcNow.AddHours(-3),
                    createdBy: Guid.Parse(Constants.ADMIN_ID),
                    id: Guid.Parse(Constants.ADMIN_ID)
                )
            );
        }
        
        public static void ModelEvent(this ModelBuilder modelBuilder){
            modelBuilder.Entity<EventEntityDomain>()
                        .Ignore(x => x.UserEvents);
        }

        public static void SeedEvent(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventEntityDomain>().HasData(
                new EventEntityDomain(
                    name: "Eventy",
                    description: "Eventy is a event management system",
                    date: DateTime.UtcNow.AddHours(-3),
                    location: "Eventy's office",
                    googleMapsUrl: "https://g.co/kgs/mxYNbz",
                    createdBy: Guid.Parse(Constants.ADMIN_ID),
                    createdAt: DateTime.UtcNow.AddHours(-3),
                    id: Guid.Parse(Constants.DEFAULT_EVENT_ID)
                )
            );
        }

        public static void ModelUserEvent(this ModelBuilder modelBuilder){
            modelBuilder.Entity<UserEventEntityDomain>()
                        .HasKey(x => new {x.UserId, x.EventId});
        }

        public static void SeedUserEvent(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEventEntityDomain>().HasData(
                new UserEventEntityDomain(
                    userId: Guid.Parse(Constants.ADMIN_ID),
                    eventId: Guid.Parse(Constants.DEFAULT_EVENT_ID),
                    status: EStatus.ACTIVE
                )
            );
        }
    }
}
