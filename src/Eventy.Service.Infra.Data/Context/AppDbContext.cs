using Eventy.Service.Domain.Entities;
using Eventy.Service.Infra.Data.Context.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Eventy.Service.Infra.Data.Context
{
    public class AppDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public AppDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(Configuration.GetSection("Settings")["PostgreSQLConnectionString"]);
        }
        public DbSet<UserEntityDomain> Users { get; set; }
        public DbSet<EventEntityDomain> Events { get; set; }
        public DbSet<UserEventEntityDomain> UserEvents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Seed default user
            modelBuilder.ModelUser();
            modelBuilder.SeedDefaultUser();

            //Events
            modelBuilder.ModelEvent();
            modelBuilder.SeedEvent();

            //User Events
            modelBuilder.ModelUserEvent();
            modelBuilder.SeedUserEvent();
        }

    }
}
