using System.ComponentModel.DataAnnotations.Schema;
using Eventy.Service.Domain.Bases;
using Eventy.Service.Domain.Enums;

namespace Eventy.Service.Domain.Entities
{
    [Table("event")]
    public class EventEntityDomain : BaseEntityDomain
    {
        public EventEntityDomain(Guid createdBy, DateTime createdAt) : base(createdBy, createdAt){ }

        public EventEntityDomain(
            string name,
            string description,
            DateTime startDate,
            DateTime endDate,
            string location,
            string googleMapsUrl,
            Guid createdBy,
            DateTime createdAt,
            Guid? id = null,
            EStatus? status = null,
            Guid? updatedBy = null,
            DateTime? updatedAt = null,
            Guid? deletedBy = null,
            DateTime? deletedAt = null
        ) : base(createdBy, createdAt, id, status, updatedAt, updatedBy, deletedAt, deletedBy
        )
        {
            Name = name;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;
            Location = location;
            GoogleMapsUrl = googleMapsUrl;
        }


        [Column("name", TypeName = "varchar(200)")]
        public string Name { get; private set; }

        [Column("description", TypeName = "varchar(5000)")]
        public string Description { get; private set; }

        [Column("start_date", TypeName = "timestamp")]
        public DateTime StartDate { get; private set; }
        [Column("end_date", TypeName = "timestamp")]
        public DateTime EndDate { get; private set; }

        [Column("location", TypeName = "varchar(200)")]
        public string Location { get; private set; }

        [Column("googlemapsurl", TypeName = "text")]
        public string GoogleMapsUrl { get; private set; }

        public List<UserEventEntityDomain> UserEvents { get; set; } = new List<UserEventEntityDomain>();
    }
}
