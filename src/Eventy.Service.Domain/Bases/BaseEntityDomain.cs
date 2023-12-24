using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eventy.Service.Domain.Enums;

namespace Eventy.Service.Domain.Bases
{
    public class BaseEntityDomain
    {
        public BaseEntityDomain(
            Guid? id = null,
            EStatus? status = null,
            DateTime? createdAt = null,
            Guid? createdBy = null,
            DateTime? updatedAt = null,
            Guid? updatedBy = null,
            DateTime? deletedAt = null,
            Guid? deletedBy = null
        )
        {
            Id = id ?? Guid.NewGuid();
            Status = status ?? EStatus.ACTIVE;
            CreatedAt = createdAt;
            CreatedBy = createdBy;
            UpdatedAt = updatedAt;
            UpdatedBy = updatedBy;
            DeletedAt = deletedAt;
            DeletedBy = deletedBy;
        }

        [Key]
        [Column("id", TypeName = "uuid")]
        public Guid Id { get; private set; }

        [Column("status", TypeName = "smallint")]
        public EStatus Status { get; private set; }

        [Column("created_at", TypeName = "timestamp")]
        public DateTime? CreatedAt { get; private set; }

        [Column("created_by", TypeName = "uuid")]
        public Guid? CreatedBy { get; private set; }


        [Column("updated_at", TypeName = "timestamp")]
        public DateTime? UpdatedAt { get; private set; }

        [Column("updated_by", TypeName = "uuid")]
        public Guid? UpdatedBy { get; private set; }


        [Column("deleted_at", TypeName = "timestamp")]
        public DateTime? DeletedAt { get; private set; }

        [Column("deleted_by", TypeName = "uuid")]
        public Guid? DeletedBy { get; private set; }
    }
}
