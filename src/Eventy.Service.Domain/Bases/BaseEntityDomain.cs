using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Eventy.Service.Domain.Enums;

namespace Eventy.Service.Domain.Bases
{
    public class BaseEntityDomain
    {
        public BaseEntityDomain(
            Guid createdBy,
            DateTime createdAt,
            Guid? id = null,
            EStatus? status = null,
            DateTime? updatedAt = null,
            Guid? updatedBy = null,
            DateTime? deletedAt = null,
            Guid? deletedBy = null
        )
        {
            CreatedBy = createdBy;
            CreatedAt = createdAt;
            Id = id ?? Guid.NewGuid();
            Status = status ?? EStatus.ACTIVE;
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
        [JsonIgnore]
        public DateTime CreatedAt { get; private set; }

        [Column("created_by", TypeName = "uuid")]
        [JsonIgnore]
        public Guid CreatedBy { get; private set; }


        [Column("updated_at", TypeName = "timestamp")]
        [JsonIgnore]
        public DateTime? UpdatedAt { get; private set; }

        [Column("updated_by", TypeName = "uuid")]
        [JsonIgnore]
        public Guid? UpdatedBy { get; private set; }


        [Column("deleted_at", TypeName = "timestamp")]
        [JsonIgnore]
        public DateTime? DeletedAt { get; private set; }

        [Column("deleted_by", TypeName = "uuid")]
        [JsonIgnore]
        public Guid? DeletedBy { get; private set; }
    }
}
