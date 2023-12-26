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
            string googleMapsUrl,
            Guid? referenceId = null
        )
        {
            Name = name;
            Description = description;
            Date = date;
            Location = location;
            GoogleMapsUrl = googleMapsUrl;
            ReferenceId = referenceId;   
        }

    
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public string GoogleMapsUrl { get; set; }
        public Guid? ReferenceId { get; set; }

        public Guid UserId { get; set; } = Guid.Parse(Constants.ADMIN_ID);


        public EventEntityDomain Parse(){
            return new EventEntityDomain(
                Name,
                Description,
                Date,
                Location,
                GoogleMapsUrl,
                UserId,
                DateTime.UtcNow.AddHours(-3),
                ReferenceId
            );
        }

    }
}
