using Eventy.Service.Domain.Entities;

namespace Eventy.Service.Domain.Events.Commands
{
    public class UpdateEventCommand : BaseEventCommand
    {
        public UpdateEventCommand(Guid id,
                                  string name,
                                  string description,
                                  DateTime date,
                                  string location,
                                  string googleMapsUrl) : base(name, description, date, location, googleMapsUrl)
        {
            Id = id;
        }

        public Guid Id { get; set; }

        public EventEntityDomain Parse(EventEntityDomain record)
        {
             var entity = new EventEntityDomain(
                Name,
                Description,
                Date,
                Location,
                GoogleMapsUrl,
                record.CreatedBy,
                record.CreatedAt,
                Id,
                updatedBy: UserId,
                updatedAt: DateTime.UtcNow.AddHours(-3)
            );

            entity.UserEvents.Add(new UserEventEntityDomain(record.CreatedBy, entity.Id));
            Participants.ForEach(x => entity.UserEvents.Add(new UserEventEntityDomain(x, entity.Id)));

            return entity;
        }
    }
}
