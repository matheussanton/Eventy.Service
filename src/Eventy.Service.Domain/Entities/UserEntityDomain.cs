using System.ComponentModel.DataAnnotations.Schema;
using Eventy.Service.Domain.Bases;
using Eventy.Service.Domain.Enums;
using Eventy.Service.Domain.User.Enums;

namespace Eventy.Service.Domain.Entities
{
    [Table("user", Schema = "identity")]
    public class UserEntityDomain : BaseEntityDomain
    {
        public UserEntityDomain() { }
        
        public UserEntityDomain(
            string name,
            string email,
            string password,
            EUserRole role,
            DateTime createdAt,
            Guid createdBy,
            Guid? id = null,
            EStatus? status = null,
            DateTime? updatedAt = null,
            Guid? updatedBy = null,
            DateTime? deletedAt = null,
            Guid? deletedBy = null
        ) : base(id, status, createdAt, createdBy, updatedAt, updatedBy, deletedAt, deletedBy)
        {
            Name = name;
            Email = email;
            Password = password;
            Role = role;
        }
        
        [Column("name", TypeName = "varchar(100)")]
        public string Name { get; private set; }

        [Column("email", TypeName = "varchar(100)")]
        public string Email { get; private set; }

        [Column("password", TypeName = "text")]
        public string Password { get; private set; }

        [Column("role", TypeName = "smallint")]
        public EUserRole Role { get; private set; }
    }
}
