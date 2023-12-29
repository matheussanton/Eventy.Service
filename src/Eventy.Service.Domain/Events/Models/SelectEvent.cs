using Eventy.Service.Domain.User.Models;

namespace Eventy.Service.Domain.Events.Models
{
    public class SelectEvent
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Location { get; set; }
        public string GoogleMapsUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid CreatedBy { get; set; }
        public bool IsOwner { get; set; } = false;

        public SelectUser Owner { get; set; }
        public List<SelectUser> Participants { get; set; } = new List<SelectUser>();
    }
}
