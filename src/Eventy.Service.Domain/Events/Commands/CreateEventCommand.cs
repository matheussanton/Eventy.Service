using Eventy.Service.Domain.Entities;
using Eventy.Service.Domain.Enums;

namespace Eventy.Service.Domain.Events.Commands
{
    public class CreateEventCommand : BaseEventCommand
    {
        public CreateEventCommand(
            string name,
            string description,
            DateTime date,
            string location,
            string googleMapsUrl
        ) : base(name, description, date, location, googleMapsUrl)
        {
        }

        public EventEntityDomain Parse(){
            var entity = new EventEntityDomain(
                Name,
                Description,
                Date,
                Location,
                GoogleMapsUrl,
                UserId,
                DateTime.UtcNow.AddHours(-3)
            );

            entity.UserEvents.Add(new UserEventEntityDomain(UserId, entity.Id, EStatus.ACTIVE));
            Participants.ForEach(x => entity.UserEvents.Add(new UserEventEntityDomain(x, entity.Id)));

            return entity;
        }
    }
}
