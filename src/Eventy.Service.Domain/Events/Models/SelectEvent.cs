using System.Text.Json.Serialization;
using Eventy.Service.Domain.Entities;
using Eventy.Service.Domain.User.Models;

namespace Eventy.Service.Domain.Events.Models
{
    public class SelectEvent
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Location { get; set; }
        public string GoogleMapsUrl { get; set; }
        [JsonIgnore]
        public DateTime CreatedAt { get; set; }
        [JsonIgnore]
        public Guid CreatedBy { get; set; }
        public bool IsOwner { get; set; } = false;

        public SelectUser Owner { get; set; }
        public List<SelectUser> Participants { get; set; } = new List<SelectUser>();

        public static explicit operator EventEntityDomain(SelectEvent evento){
            return new EventEntityDomain
            (
                evento.Name,
                evento.Description,
                evento.StartDate,
                evento.EndDate,
                evento.Location,
                evento.GoogleMapsUrl,
                evento.CreatedBy,
                evento.CreatedAt,
                evento.Id
            );
        }
    }
}
