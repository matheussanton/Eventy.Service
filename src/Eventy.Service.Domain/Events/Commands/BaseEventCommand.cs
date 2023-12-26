using Eventy.Service.Domain.Entities;
using MediatR;

namespace Eventy.Service.Domain.Events.Commands
{
    public class BaseEventCommand : IRequest
    {
        public BaseEventCommand(
            string name,
            string description,
            DateTime date,
            string location,
            string googleMapsUrl
        )
        {
            Name = name;
            Description = description;
            Date = date;
            Location = location;
            GoogleMapsUrl = googleMapsUrl;
        }

    
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public string GoogleMapsUrl { get; set; }

        public Guid UserId { get; set; } = Guid.Parse(Constants.ADMIN_ID);

        public List<Guid> Participants { get; set; } = new List<Guid>();

    }
}
