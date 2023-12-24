using Eventy.Service.Domain.Entities;
using Eventy.Service.Domain.User.Enums;
using Microsoft.EntityFrameworkCore;

namespace Eventy.Service.Infra.Data.Context.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void ModelUser(this ModelBuilder modelBuilder){
            modelBuilder.Entity<UserEntityDomain>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Status).HasColumnName("status");
                entity.Property(e => e.Name).HasColumnName("name").HasMaxLength(100).IsRequired();
                entity.Property(e => e.Email).HasColumnName("email").HasMaxLength(100).IsRequired();
                entity.Property(e => e.Password).HasColumnName("password").HasMaxLength(100).IsRequired();
                entity.Property(e => e.Role).HasColumnName("role").IsRequired();
                entity.Property(e => e.CreatedAt).HasColumnName("created_at").IsRequired();
                entity.Property(e => e.CreatedBy).HasColumnName("created_by").IsRequired();
                entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
                entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
                entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");
                entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");
            });
        }

        public static void SeedDefaultUser(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntityDomain>().HasData(
                new UserEntityDomain(
                    name: "Administrator",
                    email: "admin@eventy.com",
                    password: "Pwd@123",
                    role: EUserRole.ADMINISTRATOR,
                    createdAt: DateTime.Now,
                    createdBy: Guid.NewGuid()
                )
            );
        }
    }
}
