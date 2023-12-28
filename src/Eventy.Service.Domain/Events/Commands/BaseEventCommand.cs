using MediatR;

namespace Eventy.Service.Domain.Events.Commands
{
    public class BaseEventCommand : IRequest
    {
        public BaseEventCommand(
            string name,
            string description,
            DateTime startDate,
            DateTime endDate,
            string location,
            string googleMapsUrl
        )
        {
            Name = name;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;
            Location = location;
            GoogleMapsUrl = googleMapsUrl;
        }

    
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Location { get; set; }
        public string GoogleMapsUrl { get; set; }

        public Guid UserId { get; set; } = Guid.Parse(Constants.DEMO_USER_ID);

        public List<Guid> Participants { get; set; } = new List<Guid>();

    }
}
